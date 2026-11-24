using Microsoft.Extensions.Logging;
using OtusWebAuction.BL.Interfaces.Lot;
using OtusWebAuction.BL.Lots.Dto;
using OtusWebAuction.BL.Lots.Request;
using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.BL.Lots
{
    public sealed class LotHandler(ILotRepository lotRepository, ILogger<LotHandler> logger) : ILotHandler
    {
        public async Task<LotResultDto> GetLot(long id, CancellationToken ct)
        {
            LotDto? lot = await lotRepository.GetByIdAsync(id, ct);
            if (lot is null)
            {
                return NotFound(id);
            }

            return new LotResultDto
            {
                Status = LotResultStatus.Found,
                Response = lot
            };
        }

        public async Task<LotResultDto> CreateLot(CreateLotRequest request, Guid sellerId, CancellationToken ct)
        {
            string? error = ValidatePrices(request.StartPrice, request.BuyoutPrice, request.MinStep);
            if (error is not null)
            {
                logger.LogWarning("CreateLot отклонён: некорректные данные от пользователя {userId}: {error}", sellerId, error);
                return new LotResultDto
                {
                    Status = LotResultStatus.InvalidData,
                    Message = error
                };
            }

            //TODO разкомитить когда появятся категории
            //if (!await lotRepository.CategoryExistsAsync(request.CategoryId, ct))
            //{
            //    return new LotResultDto
            //    {
            //        Status = LotResultStatus.InvalidData,
            //        Message = $"Категория с id {request.CategoryId} не найдена."
            //    };
            //}

            LotDto lot = await lotRepository.CreateLotAsync(request, sellerId, ct);
            logger.LogInformation("Лот {lotId} создан пользователем {userId}", lot.Id, sellerId);
            return new LotResultDto
            {
                Status = LotResultStatus.Created,
                Response = lot
            };
        }

        public async Task<LotResultDto> UpdateLot(long id, UpdateLotRequest request, Guid sellerId, CancellationToken ct)
        {
            LotDto? lot = await lotRepository.GetByIdAsync(id, ct);
            if (lot is null)
            {
                logger.LogWarning("UpdateLot отклонён: лот {lotId} не найден", id);
                return NotFound(id);
            }

            //TODO добавить возможность редактирования админом
            if (lot.SellerId != sellerId)
            {
                logger.LogWarning("UpdateLot отклонён: пользователь {userId} не является владельцем лота {lotId}", sellerId, id);
                return new LotResultDto
                {
                    Status = LotResultStatus.NotOwner,
                    Message = "Редактировать лот может только его продавец."
                };
            }

            string? error = ValidatePrices(request.StartPrice, request.BuyoutPrice, request.MinStep);
            if (error is not null)
            {
                logger.LogWarning("UpdateLot отклонён: некорректные данные от пользователя {userId}: {error}", sellerId, error);
                return new LotResultDto
                {
                    Status = LotResultStatus.InvalidData,
                    Message = error
                };
            }

            if (request.CategoryId is not null
                && !await lotRepository.CategoryExistsAsync(request.CategoryId.Value, ct))
            {
                logger.LogWarning("UpdateLot отклонён: категория с id {categoryId} не найдена", request.CategoryId.Value);
                return new LotResultDto
                {
                    Status = LotResultStatus.InvalidData,
                    Message = $"Категория с id {request.CategoryId} не найдена."
                };
            }

            LotDto? updated = await lotRepository.UpdateLotAsync(id, request, ct);
            if (updated is null)
            {
                logger.LogWarning("UpdateLot отклонён: лот {lotId} не найден при обновлении", id);
                return NotFound(id);
            }

            logger.LogInformation("Лот {lotId} обновлён пользователем {userId}", id, sellerId);
            return new LotResultDto
            {
                Status = LotResultStatus.Updated,
                Response = updated
            };
        }

        public async Task<LotResultDto> DeleteLot(long id, Guid sellerId, CancellationToken ct)
        {
            LotDto? lot = await lotRepository.GetByIdAsync(id, ct);
            if (lot is null)
            {
                logger.LogWarning("DeleteLot отклонён: лот {lotId} не найден", id);
                return NotFound(id);
            }

            //TODO Добавить возможность удаления админом
            if (lot.SellerId != sellerId)
            {
                logger.LogWarning("DeleteLot отклонён: пользователь {userId} не является владельцем лота {lotId}", sellerId, id);
                return new LotResultDto
                {
                    Status = LotResultStatus.NotOwner,
                    Message = "Удалить лот может только его продавец."
                };
            }

            await lotRepository.DeleteLotAsync(id, ct);
            logger.LogInformation("Лот {lotId} удалён пользователем {userId}", id, sellerId);
            return new LotResultDto
            {
                Status = LotResultStatus.Deleted
            };
        }

        // TODO Определять роль пользователя - это должен делать только продавец
        public async Task<LotResultDto> SendToModerate(long id, Guid sellerId, CancellationToken ct)
        {
            LotDto? lot = await lotRepository.GetByIdAsync(id, ct);
            if (lot is null)
            {
                logger.LogWarning("SendToModerate отклонён: лот {lotId} не найден", id);
                return NotFound(id);
            }

            if (lot.SellerId != sellerId)
            {
                logger.LogWarning("SendToModerate отклонён: пользователь {userId} не является владельцем лота {lotId}",
                    sellerId, id);
                return new LotResultDto
                {
                    Status = LotResultStatus.NotOwner,
                    Message = "Отправить лот на модерацию может только его продавец."
                };
            }

            if (lot.ModerationStatus is ModerationStatus.Moderating or ModerationStatus.Approved)
            {
                logger.LogWarning("SendToModerate отклонён: лот {lotId} уже отправлен на модерацию или прошёл её",
                    id);
                return new LotResultDto
                {
                    Status = LotResultStatus.InvalidData,
                    Message = "Лот уже отправлен на модерацию или прошёл её."
                };
            }

            LotDto? updated = await lotRepository.SendToModerateAsync(id, ct);
            if (updated is null)
            {
                logger.LogWarning("SendToModerate отклонён: лот {lotId} не найден при обновлении", id);
                return NotFound(id);
            }

            logger.LogInformation("Лот {lotId} отправлен на модерацию пользователем {userId}", id, sellerId);
            return new LotResultDto
            {
                Status = LotResultStatus.SentToModerate,
                Response = updated
            };
        }

        // TODO Тут можно прикрутить отправку сообщения в кафку, чтобы оповестить продавца о прохождении лотом модерации
        // TODO Определять роль пользователя - это должен делать только админ или модератор 
        public async Task<LotResultDto> ApproveModerate(long id, CancellationToken ct)
        {
            LotDto? lot = await lotRepository.GetByIdAsync(id, ct);
            if (lot is null)
            {
                logger.LogWarning("ApproveModerate отклонён: лот {lotId} не найден", id);
                return NotFound(id);
            }

            if (lot.ModerationStatus != ModerationStatus.Moderating)
            {
                logger.LogWarning("ApproveModerate отклонён: лот {lotId} не находится на модерации", id);
                return new LotResultDto
                {
                    Status = LotResultStatus.InvalidData,
                    Message = "Одобрить можно только лот, находящийся на модерации."
                };
            }

            LotDto? updated = await lotRepository.ApproveModerate(id, ct);
            if (updated is null)
            {
                logger.LogWarning("ApproveModerate отклонён: лот {lotId} не найден при обновлении", id);
                return NotFound(id);
            }

            logger.LogInformation("Лот {lotId} одобрен", id);
            return new LotResultDto
            {
                Status = LotResultStatus.Approved,
                Response = updated
            };
        }

        // TODO Тут можно прикрутить отправку сообщения в кафку, чтобы оповестить продавца об отклонении лота
        // TODO Определять роль пользователя - это должен делать только админ или модератор
        public async Task<LotResultDto> RejectLot(long id, CancellationToken ct)
        {
            LotDto? lot = await lotRepository.GetByIdAsync(id, ct);
            if (lot is null)
            {
                logger.LogWarning("RejectLot отклонён: лот {lotId} не найден", id);
                return NotFound(id);
            }

            if (lot.ModerationStatus != ModerationStatus.Moderating)
            {
                logger.LogWarning("RejectLot отклонён: лот {lotId} не находится на модерации", id);
                return new LotResultDto
                {
                    Status = LotResultStatus.InvalidData,
                    Message = "Отклонить можно только лот, находящийся на модерации."
                };
            }

            LotDto? updated = await lotRepository.RejectLotAsync(id, ct);
            if (updated is null)
            {
                logger.LogWarning("RejectLot отклонён: лот {lotId} не найден при обновлении", id);
                return NotFound(id);
            }

            logger.LogInformation("Лот {lotId} отклонён", id);
            return new LotResultDto
            {
                Status = LotResultStatus.Rejected,
                Response = updated
            };
        }

        private static LotResultDto NotFound(long id) => new()
        {
            Status = LotResultStatus.NotFound,
            Message = $"Лот с id {id} не найден."
        };

        private static string? ValidatePrices(decimal? startPrice, decimal? buyoutPrice, decimal? minStep)
        {
            if (startPrice is not null && startPrice <= 0)
            {
                return "Стартовая цена должна быть больше нуля.";
            }

            if (minStep is not null && minStep <= 0)
            {
                return "Минимальное повышение ставки должно быть больше нуля.";
            }

            if (startPrice is not null
                && buyoutPrice is not null
                && buyoutPrice < startPrice)
            {
                return "Цена выкупа не может быть меньше стартовой цены.";
            }

            return null;
        }
    }
}
