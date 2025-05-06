CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProductId NVARCHAR(50) NOT NULL UNIQUE,
    ProductName NVARCHAR(100) NOT NULL,
    Category NVARCHAR(100) NOT NULL
);

CREATE TABLE Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId NVARCHAR(50) NOT NULL UNIQUE,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Address NVARCHAR(200) NOT NULL
);

CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId NVARCHAR(50) NOT NULL UNIQUE,
    ProductId INT NOT NULL,
    CustomerId INT NOT NULL,
    Region NVARCHAR(100) NOT NULL,
    DateOfSale DATETIME NOT NULL,
    QuantitySold INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    Discount DECIMAL(5,2) NOT NULL,
    ShippingCost DECIMAL(18,2) NOT NULL,
    PaymentMethod NVARCHAR(50) NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Products(Id),
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
);



-- Insert Products
INSERT INTO Products (ProductId, ProductName, Category)
VALUES 
('P123', 'UltraBoost Running Shoes', 'Shoes'),
('P456', 'iPhone 15 Pro', 'Electronics'),
('P789', 'Levi''s 501 Jeans', 'Clothing'),
('P234', 'Sony WH-1000XM5 Headphones', 'Electronics');

-- Insert Customers
INSERT INTO Customers (CustomerId, Name, Email, Address)
VALUES 
('C456', 'John Smith', 'johnsmith@email.com', '123 Main St, Anytown, CA 12345'),
('C789', 'Emily Davis', 'emilydavis@email.com', '456 Elm St, Otherville, NY 54321'),
('C101', 'Sarah Johnson', 'sarahjohnson@email.com', '789 Oak St, New City, TX 75024');

-- Insert Orders
INSERT INTO Orders (OrderId, ProductId, CustomerId, Region, DateOfSale, QuantitySold, UnitPrice, Discount, ShippingCost, PaymentMethod)
VALUES 
('1001',
 (SELECT Id FROM Products WHERE ProductId = 'P123'),
 (SELECT Id FROM Customers WHERE CustomerId = 'C456'),
 'North America', '2023-12-15', 2, 180.00, 0.10, 10.00, 'Credit Card'),

('1002',
 (SELECT Id FROM Products WHERE ProductId = 'P456'),
 (SELECT Id FROM Customers WHERE CustomerId = 'C789'),
 'Europe', '2024-01-03', 1, 1299.00, 0.00, 15.00, 'PayPal'),

('1003',
 (SELECT Id FROM Products WHERE ProductId = 'P789'),
 (SELECT Id FROM Customers WHERE CustomerId = 'C456'),
 'Asia', '2024-02-28', 3, 59.99, 0.20, 5.00, 'Debit Card'),

('1004',
 (SELECT Id FROM Products WHERE ProductId = 'P123'),
 (SELECT Id FROM Customers WHERE CustomerId = 'C101'),
 'South America', '2024-03-10', 1, 180.00, 0.00, 8.00, 'Credit Card'),

('1005',
 (SELECT Id FROM Products WHERE ProductId = 'P234'),
 (SELECT Id FROM Customers WHERE CustomerId = 'C789'),
 'North America', '2024-04-22', 1, 349.99, 0.15, 12.00, 'PayPal'),

('1006',
 (SELECT Id FROM Products WHERE ProductId = 'P456'),
 (SELECT Id FROM Customers WHERE CustomerId = 'C101'),
 'Asia', '2024-05-18', 2, 1299.00, 0.05, 20.00, 'Debit Card');






select * from Product
select * from Customer
select * from Orders





