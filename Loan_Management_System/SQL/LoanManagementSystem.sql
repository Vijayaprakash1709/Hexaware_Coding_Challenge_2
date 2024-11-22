CREATE DATABASE LoanManagementSystem;

USE LoanManagementSystem;

-- Customer Table
CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    EmailAddress NVARCHAR(100) UNIQUE NOT NULL,
    PhoneNumber NVARCHAR(15) UNIQUE NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    CreditScore INT NOT NULL);

-- Loan Table
CREATE TABLE Loan (

    LoanID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT NOT NULL,
    PrincipalAmount DECIMAL(15, 2) NOT NULL,
    InterestRate FLOAT NOT NULL,
    LoanTerm INT NOT NULL,
    LoanType NVARCHAR(50) NOT NULL,
    LoanStatus NVARCHAR(50) DEFAULT 'Pending',
    CONSTRAINT CHK_LoanType CHECK (LoanType IN ('CarLoan', 'HomeLoan')),
    CONSTRAINT CHK_LoanStatus CHECK (LoanStatus IN ('Pending', 'Approved')),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID));

-- HomeLoan Table
CREATE TABLE HomeLoan (
	HomeLoanID INT PRIMARY KEY identity,
    LoanID int ,
    PropertyAddress NVARCHAR(255) NOT NULL,
    PropertyValue DECIMAL(15, 2) NOT NULL,
    FOREIGN KEY (LoanID) REFERENCES Loan(LoanID));

-- CarLoan Table
CREATE TABLE CarLoan (
	CarLoanID INT PRIMARY KEY identity,
    LoanID INT ,
    CarModel NVARCHAR(100) NOT NULL,
    CarValue DECIMAL(15, 2) NOT NULL,
    FOREIGN KEY (LoanID) REFERENCES Loan(LoanID));

	INSERT INTO Customer (Name, EmailAddress, PhoneNumber, Address, CreditScore)
VALUES
('vijay', 'vijay@gmail.com', '9876543210', 'Salem', 720),
('Prakash', 'prakash@gmail.com', '9476383762', 'Attur', 680),
('Jayes', 'jayes@gmail.com', '9597996275', 'Chennai', 750);

INSERT INTO Loan (CustomerID, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus)
VALUES
(1, 25000.00, 5.5, 60, 'HomeLoan', 'Pending'),
(2, 15000.00, 7.0, 48, 'CarLoan', 'Approved'),
(3, 30000.00, 4.8, 120, 'HomeLoan', 'Approved');

INSERT INTO HomeLoan (LoanID, PropertyAddress, PropertyValue)
VALUES
(1, 'Salem', 350000.00),
(3, 'Attur', 400000.00);

INSERT INTO CarLoan (LoanID, CarModel, CarValue)
VALUES
(2, 'BMW M4', 25000.00);

SELECT * FROM CarLoan
SELECT * FROM Customer
SELECT * FROM HomeLoan
SELECT * FROM Loan

