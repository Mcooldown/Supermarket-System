CREATE DATABASE marketDB

CREATE TABLE Product (
     ProductID INT PRIMARY KEY,
     ProductName VARCHAR(255),
     Quantity INT,
     Price INT
)

CREATE TABLE HeaderTransaction(
	TransactionID INT,
	PaymentMethod VARCHAR(255),
	PRIMARY KEY(TransactionID)
)

CREATE TABLE DetailTransaction(
	TransactionID INT,
	ProductID INT,
	Quantity INT,
	PRIMARY KEY(TransactionID, ProductID),
	FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
)

INSERT INTO Product VALUES
	('1','Kacang Merah','100','1000'),
	('2','Kacang Kulit','100','2000'),
	('3','Sabun Batang','2000','10000'),
	('4','Sabun Cair','1000','50000'),
	('5','Indomie Kuah','800','3000'),
	('6','Indomie Goreng','900','3000')