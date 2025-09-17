# ScanPlant API

API backend em C# para o aplicativo ScanPlant, desenvolvida com ASP.NET Core Web API.

## Tecnologias Utilizadas

- ASP.NET Core 6.0
- Entity Framework Core
- SQL Server
- Identity para autenticação e autorização
- JWT para tokens de autenticação
- Azure Blob Storage para armazenamento de imagens
- Swagger para documentação da API

## Estrutura do Projeto

- **Controllers**: Controladores da API para gerenciar requisições HTTP
- **Data**: Contexto do banco de dados e migrações
- **Models**: Modelos de dados e DTOs
- **Services**: Serviços para lógica de negócios

## Funcionalidades

- Autenticação e autorização de usuários
- Operações CRUD para plantas
- Upload e gerenciamento de imagens
- Busca de plantas por nome
- Busca de plantas por localização geográfica

## Configuração

1. Certifique-se de ter o .NET 6.0 SDK instalado
2. Configure a string de conexão do banco de dados em `appsettings.json`
3. Configure a string de conexão do Azure Blob Storage em `appsettings.json`
4. Execute as migrações do banco de dados:

```bash
dotnet ef database update
```

## Executando o Projeto

```bash
dotnet run
```

A API estará disponível em:
- http://localhost:5000
- https://localhost:5001

A documentação Swagger estará disponível em:
- http://localhost:5000/swagger
- https://localhost:5001/swagger

## Endpoints da API

### Autenticação

- `POST /api/auth/register` - Registrar um novo usuário
- `POST /api/auth/login` - Fazer login
- `POST /api/auth/reset-password` - Solicitar redefinição de senha
- `GET /api/auth/me` - Obter dados do usuário atual

### Plantas

- `GET /api/plants` - Obter todas as plantas
- `GET /api/plants/{id}` - Obter uma planta pelo ID
- `GET /api/plants/my-plants` - Obter plantas do usuário atual
- `GET /api/plants/nearby` - Obter plantas próximas a uma localização
- `GET /api/plants/search` - Buscar plantas por nome
- `POST /api/plants` - Criar uma nova planta
- `PUT /api/plants/{id}` - Atualizar uma planta existente
- `DELETE /api/plants/{id}` - Excluir uma planta

## Integração com o Frontend

Para integrar esta API com o aplicativo React Native existente, atualize as chamadas de API no frontend para apontar para os endpoints desta API em vez do Supabase.# Projeto-ScanPlant
# Projeto-ScanPlant
