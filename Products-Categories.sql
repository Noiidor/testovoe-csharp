CREATE TABLE Products (
  ProductID INTEGER PRIMARY KEY AUTOINCREMENT,
  ProductName VARCHAR(50),
  CategoryID integer
  );
  
INSERT INTO Products
VALUES
  (1, "Product1", 2),
  (2, "Product2", 1),
  (3, "Product3", 3),
  (4, "Product4", 2),
  (5, "Product5", NULL),
  (6, "Product6", 1),
  (7, "Product7", 2),
  (8, "Product8", NULL),
  (9, "Product9", 4),
  (10, "Product10", 3);
  
CREATE TABLE Categories (
  CategoryID INTEGER PRIMARY KEY AUTOINCREMENT,
  CategoryName VARCHAR(50)
  );
  
INSERT INTO Categories
VALUES
  (1, "Category1"),
  (2, "Category2"),
  (3, "Category3"),
  (4, "Category4");
  
SELECT ProductName, CategoryName FROM Products P
LEFT JOIN Categories C ON P.CategoryID = C.CategoryID