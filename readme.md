# Configuração do Projeto Base Dotnet API

Este documento descreve o processo de configuração do projeto Base Dotnet API, permitindo que você inicie sua API.

## Pré-requisitos

Antes de iniciar a configuração, você precisa ter o seguinte instalado:

- [.NET SDK](https://dotnet.microsoft.com/download/dotnet) (versão 6.0 ou superior)
- Um banco de dados (SQL Server, MySQL, PostgreSQL, etc.)

## Configuração do Projeto

Siga os passos abaixo para configurar o seu projeto:

### 1. Clonando o Repositório

Clone o repositório em sua máquina local:

```bash
git clone https://github.com/Glide-Tecnologia/Base-Dotnet-Api.git
```

Navegue até o diretório do projeto:

```bash
cd Base-Dotnet-Api
```

### 2. Instalação do Entity Framework Core

Para instalar o Entity Framework Core, execute os seguintes comandos:

```ps
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
```

Se você estiver usando um banco de dados específico, adicione o provedor correspondente. Por exemplo, para SQL Server:

```ps
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

Para MySQL:

```ps
dotnet add package Pomelo.EntityFrameworkCore.MySql
```

### 3. Configuração do DbContext

Crie um novo arquivo para o contexto de dados. Exemplo: `MyDbContext.cs`:

```Csharp
using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    public DbSet<MyEntity> MyEntities { get; set; }
}
```

### 4. Configuração do Banco de Dados

No arquivo `appsettings.json`, adicione a string de conexão para o seu banco de dados:

```Json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=seu_servidor;Database=seu_banco;User Id=seu_usuario;Password=sua_senha;"
  }
}
```

### 5. Configuração do Startup

No arquivo Startup.cs, configure o seu DbContext para usar a string de conexão:

```Csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<MyDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
}
```

### 6. Executando Migrations

Para criar e aplicar migrations, use os seguintes comandos no terminal:

1. Crie uma migration:

```bash
dotnet ef migrations add NomeDaMigration
```
2. Aplique a migration ao banco de dados:
```bash
dotnet ef database update
```
## Executando a Aplicação

Após a configuração, você pode executar sua API com o comando:
```bash
dotnet run
```

A aplicação estará disponível em `http://localhost:5000` (ou conforme configurado).
