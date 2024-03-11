CREATE
EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE "customers"
(
    "Id"    UUID PRIMARY KEY UNIQUE DEFAULT uuid_generate_v4(),
    "Cpf"   VARCHAR(11)  NOT NULL,
    "Name"  VARCHAR(100) NOT NULL,
    "Email" VARCHAR(100) NOT NULL
);

CREATE TABLE "products"
(
    "Id"          UUID PRIMARY KEY UNIQUE DEFAULT uuid_generate_v4(),
    "Name"        VARCHAR(100)   NOT NULL,
    "Category"    SMALLINT       NOT NULL,
    "Price"       NUMERIC(10, 2) NOT NULL,
    "Description" VARCHAR(200)   NOT NULL,
    "Images"      JSONB
);

CREATE TABLE "orders"
(
    "Id"         UUID PRIMARY KEY UNIQUE DEFAULT uuid_generate_v4(),
    "CustomerId" UUID NULL,
    "OrderNumber" INT NOT NULL,
    "Status"     SMALLINT    NOT NULL,
    "CreatedAt"  TIMESTAMPTZ NOT NULL    DEFAULT NOW(),
    "TotalPrice" DECIMAL,
    "QrCodePayment" VARCHAR(1000) NULL,
    "PaymentStatusDate" TIMESTAMPTZ NULL,
    FOREIGN KEY ("CustomerId") REFERENCES "customers" ("Id")
);

CREATE TABLE "orderItems"
(
    "Id"         UUID PRIMARY KEY UNIQUE DEFAULT uuid_generate_v4(),
    "OrderId"    UUID     NOT NULL,
    "ProductId"  UUID     NOT NULL,
    "Quantity"   SMALLINT NOT NULL,
    "TotalPrice" DECIMAL  NOT NULL,
    FOREIGN KEY ("OrderId") REFERENCES "orders" ("Id"),
    FOREIGN KEY ("ProductId") REFERENCES "products" ("Id")
);

-- Lanches Clássicos
INSERT INTO "products" ("Name", "Category", "Price", "Description", "Images")
VALUES ('Burger Clássico', 0, 4.99, 'Hambúrguer, queijo, alface, cebola, picles, ketchup e mostarda no pão de hambúrguer.', '[{"url": "url_da_imagem"}]');

INSERT INTO "products" ("Name", "Category", "Price", "Description", "Images")
VALUES ('Chicken', 0, 5.49, 'Filé de frango empanado, alface, maionese no pão de hambúrguer.', '[{"url": "url_da_imagem"}]');

-- Lanches Premium
INSERT INTO "products" ("Name", "Category", "Price", "Description", "Images")
VALUES ('Bacon Deluxe', 0, 6.99, 'Hambúrguer, queijo, bacon, alface, tomate, cebola caramelizada e maionese no pão de brioche.', '[{"url": "url_da_imagem"}]');

INSERT INTO "products" ("Name", "Category", "Price", "Description", "Images")
VALUES ('Chicken Ranch', 0, 6.79, 'Filé de frango grelhado, queijo suíço, bacon, alface, tomate e molho ranch no pão de brioche.', '[{"url": "url_da_imagem"}]');

-- Lanches Alternativos
INSERT INTO "products" ("Name", "Category", "Price", "Description", "Images")
VALUES ('Wrap de Frango Caesar', 0, 5.99, 'Tortilla de trigo, tiras de frango grelhado, alface, queijo parmesão e molho Caesar.', '[{"url": "url_da_imagem"}]');

INSERT INTO "products" ("Name", "Category", "Price", "Description", "Images")
VALUES ('Salada de Grilled Chicken', 0, 7.49, 'Mix de folhas verdes, frango grelhado, tomate, queijo, croutons e molho à escolha.', '[{"url": "url_da_imagem"}]');

-- Acompanhamentos e Sobremesas
INSERT INTO "products" ("Name", "Category", "Price", "Description", "Images")
VALUES ('Nuggets', 1, 3.99, 'Porções de pedaços de frango empanado.', '[{"url": "url_da_imagem"}]');

INSERT INTO "products" ("Name", "Category", "Price", "Description", "Images")
VALUES ('Fries', 1, 2.49, 'Batatas fritas crocantes.', '[{"url": "url_da_imagem"}]');

-- Bebidas
INSERT INTO "products" ("Name", "Category", "Price", "Description", "Images")
VALUES ('Cola', 2, 1.99, 'Refrigerante cola de sua escolha.', '[{"url": "url_da_imagem"}]');

INSERT INTO "products" ("Name", "Category", "Price", "Description", "Images")
VALUES ('Guaraná', 2, 1.99, 'Refrigerante guaraná de sua escolha.', '[{"url": "url_da_imagem"}]');

INSERT INTO "products" ("Name", "Category", "Price", "Description", "Images")
VALUES ('Café Frappé', 2, 4.29, 'Café gelado batido com gelo e chantilly.', '[{"url": "url_da_imagem"}]');

-- Sobremesas
INSERT INTO "products" ("Name", "Category", "Price", "Description", "Images")
VALUES ('Flurry', 3, 3.79, 'Sorvete misturado com pedaços de chocolate, biscoito ou outras opções.', '[{"url": "url_da_imagem"}]');

INSERT INTO "products" ("Name", "Category", "Price", "Description", "Images")
VALUES ('Apple Pie', 3, 2.99, 'Torta de maçã deliciosa.', '[{"url": "url_da_imagem"}]');