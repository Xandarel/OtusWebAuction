# OtusWebAuction
Учебный проект для Otus

## Авторы:
- Бордюг Александр (@Xandarel)
- Тулендинов Данияр (@danimsu)
- Ильдар (@dev-itsec)

## Структура
- OtusWebAuction - REST API бэкенд аукциона.
- OtusWebAuction.BL - Слой бизнесс логики.
- OtusWebAuction.DB - Слой работы с базой данных (PostgreSQL)
- OtusWebAuction.Domain - Слой для общих сущностей

## Docker
Для локальной разработки в паке docker собрар docker-compose.yml. В нем заведены 3 сервиса
- PostgreSQL база данных
- GrafanaLoki для хранения логов
- Grafana для мониторинга логов

userName и pass прописать в секрете OtusWebAuction в секции Postgres

## Схема БД
![Схема базы](docs/Auction.svg)

## Диаграмма приложения
![Схема приложения](www.plantuml.com/plantuml/png/TL1DJuGm4BttLpHxtp_WGNVnmqqa1eKRyJ0f0pOBBPcs9cRZVxS3AYL4BipCUszUvnsS1_9XwCK8wWmTojq3MUFHDCUWTl8YwamtBeTFf3TH_s9pBwNu4d_29sZF4Fa5sIOeXlUohkoeLHJEVvxSMkF24E5K-tfVLiTHHyleAEF7D4-sxv5-p5cRvNFD5EuMv2xZUhsqiCvtX5Y-v1V1bBKbDjhn_0JkVIj-jbwtMe7NrfH87rhX3RChxB4Z68RC-fNWaQ050zFxwzns7TAoDKrHxlVNSabf6-g4R--_agUgAgQen9BYFsp1YIHsV3m7bepIIpPW4ie1JHE6Rm00)