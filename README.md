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

## БД
Для локальной разработки можно завести базу данных в docker командой
docker run -d --name postgres-sql --restart unless-stopped -e POSTGRES_USER={userName} -e POSTGRES_PASSWORD='{pass}' -e POSTGRES_DB=sql -p 5432:5432 -v postgres_sql_data:/var/lib/postgresql postgres:18

userName и pass прописать в секрете OtusWebAuction в секции Postgres

## Схема БД
![Схема базы](docs/Auction.svg)

## Диаграмма приложения
![Схема приложения](https://www.plantuml.com/plantuml/png/VL7DJiCm3BxdANW1tW315WONAZJeRbKFKUf5rjAwSX8aDF7kM2AXAequ9V7tOt_EBaJ3COrUpSPUZ8ERPwOfmZJiaxsgJkCG6lC9tAkk8FdzfNurxq_jQJ_ZZ-HHM4pU0sVbRQURh2atgHrqnGS636BHa7F0M_n88Je6QDyQOHtq2dmnvukHyf5fI1QantrteXbjh_8f1HakfF6FxIj5FACr4MbgWJ_GmiDLHE31iHarnLu9lnPDBCkmwu3_8cjh2RBHL5T-9uJD34LVSDbA-RRikMn4BVdhLZlfbKR_3G00)