[![Build Status](https://julianopestili.visualstudio.com/CtrlBox/_apis/build/status/JulianoMuteki.ctrlbox?branchName=master)](https://julianopestili.visualstudio.com/CtrlBox/_build/latest?definitionId=2&branchName=master)

# Ctrl.Box

# .NET core 3.0

ASP.NET MVC Core + EF Core
Este projeto é usado como base de estudo para aperfeiçoar minhas habilidades técnicas. Estou modelando conforme tutoriais, vídeos e demos. Ainda muitos bugs mas faz parte. Estou aberto a sugestões, dicas 😁

Futuro:
    -Azure DevOps 
    -API ASP.NET Core
    -ASP.NET MVC Core
    -Fluent Validation
    -Entity Framework Core
    -Swagger
    -RabbitMQ 📝
    -SQL Server
    -MongoDB/Postgres
    -Docker
    -Azure ☁️
    -Tests

O que eu quero aprender: DDD + CQRS + SOLID


Docker
SQL Server

sudo service docker start

sudo docker pull mcr.microsoft.com/mssql/server:2019-CU5-ubuntu-18.04

sudo docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourStrong!Passw0rd' --network=mssql-network -e 'MSSQL_PID=Developer' -p 14333:1433 -d mcr.microsoft.com/mssql/server:2019-CU5-ubuntu-18.04

ip addr show | grep inet
PostgresSQL

CtrlMoney\src\docker-compose.yaml
