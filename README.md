# fiap.tech-challenge.fast-food.api

## Introdução
Repositório para o projeto DDD da Pós Graduação FIAP - Software Architecture.
O Tech Challenge Grupo 19 é composto por:
- Danilo Queiroz da Silva
- Elton Douglas Souza
- Leandro da Silva Câmara
- Marcelo Patricio da Silva
- Melchior Felix de Moraes


## Tecnologias
* [.NET 8.0](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
* [ Entity Framework Core 8.0 ](https://devblogs.microsoft.com/dotnet/announcing-ef8/)
* [ OpenAPI - Swashbuckle ](https://learn.microsoft.com/pt-br/aspnet/core/tutorials/getting-started-with-swashbuckle)
* [ ReDoc ](https://github.com/Redocly/redoc)
* [ FluentMigrator ](https://fluentmigrator.github.io/)
* [ Postgresql ](https://www.postgresql.org/)

## Arquitetura
A arquitetura da aplicação é a Clean Architecture. Para a camada Entities, foi adotado o Domain Driven Design.
Segue a estrutura da aplicação:

    .
    ├── Drivers                     # Frameworks & Drivers
        ├── API                     # Web API (.NET 8)
            ├── HealthChecks
            └── Routers
        └── External                # External Interfaces & DB
            ├── Clients
            └── Persistence
                ├── Migrations
                └── Repositories
    ├── Adapters                    # Interface Adapters
        ├── Controllers
        └── Gateways
    └── Core                        # Business Rules
        ├── Application
            └── UseCases
        └── Entities (Domain)
            ├── BoundedContext
                └── Model           # Aggregates - entities and value objects
            └── SeedWork            # Reusable classes/interfaces for the domain (by Martin Fowler)

## Execução
O projeto pode ser executado utilizando o Docker.

### Docker Compose

Para iniciar o banco de dados (_Postgres_) e a aplicação, utilize o comando abaixo abaixo no seu prompt shell (windows ou linux):
```shell
docker compose up --build
```

Remover os _containers_:
```shell
docker compose down
```

Remover o _volume_ de dados do postgresql:
```shell
docker volume rm fastfood_data
```

Após o build do banco e das aplicações, você pode acessar as informações OpenAPI de duas formas:
Swagger UI da API: `http://localhost:8080/swagger` (***Recomendado para testar as chamadas aos endpoints***)
ReDoc UI da API: `http://localhost:8080/api-docs` (***Recomendado para visualização amigável***)

### CLI
Para iniciar a aplicação e banco de dados separadamente, siga os passos abaixo.

Para subir o BD local, o recomendado é utilizar o Docker e executar o seguinte comando:
```shell
docker run --rm --name pg-docker -e POSTGRES_PASSWORD=docker -d -p 5432:5432 postgres
```

Obs.: A *connection string* já está configurada corretamente no arquivo *launchSettings.json*

Inicie a Aplicação (API):
```shell
dotnet run --project .\src\Drivers\API\API.csproj
```

Caso seja necessário derrubar o BD, basta executar:

```shell
docker container kill pg-docker
```


# Como testar 

## Schema de Banco de dados
A aplicação conta com uma biblioteca de migrations configurada portanto, ao iniciar a aplicação, o banco de dados será atualizado com as tabelas e dados necessários.

## Autenticação
Na primeira versão da aplicação não há autenticação, portanto basta usar a própria UI do Swagger para fazer as chamadas aos endpoints.

## Endpoints
O detalhe de cada request, como seus parâmetros e tipos estão detalhadas nas interfaces de UI do Swagger e do ReDoc. Nas próximas sessões estão resumidas as operações existentes.

## Produtos
O sistema já possui uma carga de produtos pré cadastrados para serem utilizados nos testes, você pode acessar a lista de produtos por categoria através do método:
`[GET] api/products?category={categoryId}`.

Também é possível criar produtos ou editar, excluir e recuperar produtos pelo seu **id**. Os endpoints são os seguintes:
- `[GET] api/products/{id}`: Deve ser fornecido o id do produto a ser retornado.
- `[POST] api/products`: Criar um novo produto.
- `[PUT] api/products`: Atualizar um produto existente da base de dados.
- `[DELETE] api/products/{id}`: Excluir um produto existente da base de dados.

### Códigos das Categorias de Produtos
- `0`: Lanches
- `1`: Acompanhamentos
- `2`: Bebidas
- `3`: Sobremesas


## Clientes
O sistema dispõe de endpoints para cadastrar clientes para relacioná-los aos pedidos, embora não seja requerido para criação de um pedido.
Não há carga prévia de dados para clientes.

Há dois endpoints para manipulação do cadastro de clientes:
- `[POST] api/customers`: Criar um novo cliente caso o CPF já não se encontre na base de dados.
- `[GET] api/customers?cpf={cpf}`: Retornar um cliente por seu CPF.


## Pedidos
Na versão inicial do sistema, é possível criar um pedido, retornar a lista de pedidos existentes e retornar os detalhes de um pedido por seu **id**.
- `[POST] api/orders`: Criar um novo pedido com o status Pay com os itens e relacionado ao cliente (quando informado).
- `[GET] api/orders/ongoing`: Retorna a lista dos pedidos em andamento, ordenada dos mais antigos para os mais novos.
- `[POST] api/orders/{id}/status`: Atualiza o status de um pedido específico.
- `[GET] api/orders/{id}`: Retorna o detalhe de um pedido informado pelo **{id}**.

### Status dos Pedidos
- `0`: Pagamento Pendente 
- `1`: Pagamento Recusado
- `2`: Pedido Recebido na Cozinha
- `3`: Cozinha Preparando Pedido
- `4`: Pedido Pronto
- `5`: Pedido Concluído


## Webhook para atualização de pagamentos
O sistema, em sua versão 2, terá integração com o Mercado Pago (QrCode dinâmico).
Na versão atual existe um endpoint que simula uma chamada do Mercado Pago para o sistema.
Podendo a chamada ser de aceite ou recusa do pagamento.
- `[POST] api/webhook/orders/payment`: Endpoint para representar a chamada do mercado pago informando pagamento aceito ou recusado.

## Fluxo de testes recomendado
Para testar a aplicação recomendamos primeiro:
1. Incluir um produto (podendo também testar atualização e exclusão dos produtos).
2. Incluir um novo cliente (guardando a informação do campo id retornado na inclusão).
3. Buscar lista de produtos por categoria, guardando alguns ids para utilizá-los na criação do pedido.
4. Criar um novo pedido (podendo ou não informar o cliente).
5. Atualizar o status de pagamento de um pedido, simulando a chamada realizada pelo mercado pago ao webhook.
6. Listar todos os pedidos em andamento
7. Atualizar o status do pedido (com pagamento confirmado)
8. Obter informações de um pedido

Todo o fluxo está presente na [Collection do Postman](./docs/postman/FIAP.FastFood.postman_collection.json).

