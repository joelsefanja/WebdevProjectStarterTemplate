CREATE DATABASE IF NOT EXISTS nhlstendencafe;

USE nhlstendencafe;

DROP TABLE IF EXISTS order_products, Product, Category, OrderLine, Users, Orders;

CREATE TABLE Category
(
    CategoryId  INTEGER AUTO_INCREMENT PRIMARY KEY,
    Name        VARCHAR(128) NOT NULL
);

CREATE TABLE Product
(
    ProductId   INTEGER         AUTO_INCREMENT PRIMARY KEY,
    Name        NVARCHAR(128)   NOT NULL UNIQUE,
    CategoryId  INTEGER         NOT NULL,
    Price       DECIMAL(10,2)   NOT NULL CHECK (Price > 0),

    CONSTRAINT FK_ProductCategory FOREIGN KEY (CategoryId) REFERENCES Category (CategoryId) ON DELETE CASCADE
);


CREATE TABLE Users (
                       Id INT AUTO_INCREMENT PRIMARY KEY,
                       Email VARCHAR(255) NOT NULL,
                       PasswordHash VARCHAR(255) NOT NULL,
                       FirstName VARCHAR(255),
                       LastName VARCHAR(255),
                       CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

create index ProductId
    on Product (ProductId);

CREATE TABLE Orders
(
    OrderId         INT PRIMARY KEY AUTO_INCREMENT,
    UserId          INT NOT NULL,
    TableNumber     INT NOT NULL,
    OrderDate       DATETIME DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT FK_OrdersUser FOREIGN KEY (UserId) REFERENCES Users (Id) ON DELETE CASCADE
);

CREATE TABLE OrderLine
(
    OrderId         INT PRIMARY KEY NOT NULL,
    ProductId       INT NOT NULL,
    Quantity        INT NOT NULL,
    AmountPaid      INT NOT NULL,

    CONSTRAINT FK_OrderLineProduct FOREIGN KEY (ProductId) REFERENCES Product (ProductId),
    CONSTRAINT FK_OrderLineOrder FOREIGN KEY (OrderId) REFERENCES Orders (OrderId) ON DELETE CASCADE
);

create index OrderId
    on OrderLine (OrderId);

INSERT INTO Category (Name) VALUES ('Frisdranken');
INSERT INTO Category (Name) VALUES ('Bier');
INSERT INTO Category (Name) VALUES ('Wijnen en aperitieven');
INSERT INTO Category (Name) VALUES ('Warme dranken');
INSERT INTO Category (Name) VALUES ('Speciaal bier');

INSERT INTO Product (Name, CategoryId, Price) VALUES ('Dommelsch 0.22' , (SELECT CategoryId FROM Category WHERE Name = 'Bier'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Dommelsch 0.25' , (SELECT CategoryId FROM Category WHERE Name = 'Bier'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Dommelsch 0.50' , (SELECT CategoryId FROM Category WHERE Name = 'Bier'), 4.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Jupiler N/A 0.0%' , (SELECT CategoryId FROM Category WHERE Name = 'Bier'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Palm' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 3.40);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Hoegaarden witbier' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 3.40);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Hoegaarden Radler 0.0%' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 3.40);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Hoegaarden Radler 2.0%' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 3.40);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Leffe dubbel' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 3.75);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Leffe blond' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 3.75);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Leffe trippel' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 4.25);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Hoegaarden rosé' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 3.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Liefmans fruitesse' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 3.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Oud bruin' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Biestheuvel blond 6%' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 4.00);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Biestheuvel IPA 7%' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 4.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Biestheuvel Tripel 9%' , (SELECT CategoryId FROM Category WHERE Name = 'Speciaal bier'), 4.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Coca-cola regular' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Coca-cola light' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Coca-cola zero' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Sprite' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Fanta orange' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Bitter Lemon' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Tonic' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Fanta Cassis' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Chaudfontainte still' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Chaudfontainte sparkling' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Lipton-ice tea regular' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Lipton-ice green' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Appelsap' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Jus d’orange' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Rivella' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Tomatensap' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Chocomel' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Fristi' , (SELECT CategoryId FROM Category WHERE Name = 'Frisdranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Huiswijnen Rood' , (SELECT CategoryId FROM Category WHERE Name = 'Wijnen en aperitieven'), 3.75);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Huiswijnen Wit' , (SELECT CategoryId FROM Category WHERE Name = 'Wijnen en aperitieven'), 3.75);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Huiswijnen Rose' , (SELECT CategoryId FROM Category WHERE Name = 'Wijnen en aperitieven'), 3.75);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Port' , (SELECT CategoryId FROM Category WHERE Name = 'Wijnen en aperitieven'), 3.75);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Sherry' , (SELECT CategoryId FROM Category WHERE Name = 'Wijnen en aperitieven'), 3.75);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Vermouth' , (SELECT CategoryId FROM Category WHERE Name = 'Wijnen en aperitieven'), 3.75);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Kop koffie' , (SELECT CategoryId FROM Category WHERE Name = 'Warme dranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Thee (Lipton)' , (SELECT CategoryId FROM Category WHERE Name = 'Warme dranken'), 2.30);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Cappuccino' , (SELECT CategoryId FROM Category WHERE Name = 'Warme dranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Latte Macchiato' , (SELECT CategoryId FROM Category WHERE Name = 'Warme dranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Koffie verkeerd' , (SELECT CategoryId FROM Category WHERE Name = 'Warme dranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Espresso' , (SELECT CategoryId FROM Category WHERE Name = 'Warme dranken'), 2.50);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Warme chocomel' , (SELECT CategoryId FROM Category WHERE Name = 'Warme dranken'), 3.00);
INSERT INTO Product (Name, CategoryId, Price) VALUES ('Warme chocomel met slagroom' , (SELECT CategoryId FROM Category WHERE Name = 'Warme dranken'), 3.50);

SELECT * 
FROM Product as p
    JOIN Category c on p.CategoryId = c.CategoryId
ORDER BY c.Name, p.Name, p.Price