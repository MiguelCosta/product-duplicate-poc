
USE master;
GO

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MyDatabase')
BEGIN
    CREATE DATABASE MyDatabase;
END
GO

USE MyDatabase;
GO

CREATE TABLE Merchant (
    Code INT PRIMARY KEY,           -- 'Code' is the primary key, of type INT
    Name NVARCHAR(255) NOT NULL     -- 'Name' is a string, not nullable
);

-- Create Products table
CREATE TABLE Products (
    Id NVARCHAR(255) PRIMARY KEY, -- Unique identifier for the product
    Catalog NVARCHAR(255) NOT NULL, -- Catalog information
    CatalogType INT NOT NULL, -- Enum for CatalogType (stored as INT)
    Name NVARCHAR(MAX), -- Product name (nullable)
    Description NVARCHAR(MAX), -- Product description (nullable)
    Price DECIMAL(18, 2), -- Product price (nullable)
    Currency NVARCHAR(10), -- Currency code (nullable)
    BrandProductId NVARCHAR(255), -- Brand product ID (nullable)
    MainColourId UNIQUEIDENTIFIER, -- Main colour ID (nullable, GUID)
    MainColourName NVARCHAR(255), -- Main colour name (nullable)
    BrandId UNIQUEIDENTIFIER, -- Brand ID (nullable, GUID)
    BrandName NVARCHAR(255), -- Brand name (nullable)
    Gender NVARCHAR(50), -- Gender information (nullable)
    MerchantId UNIQUEIDENTIFIER, -- Merchant ID
    MerchantCode INT NULL, -- Merchant code
    MerchantName NVARCHAR(255), -- Merchant name
    PlatformCategoryId UNIQUEIDENTIFIER, -- Platform category ID (nullable, GUID)
    WebCategoryId UNIQUEIDENTIFIER, -- Web category ID (nullable, GUID)
    SizeScaleId UNIQUEIDENTIFIER, -- Size scale ID (nullable, GUID)
    SizeScaleName NVARCHAR(255), -- Size scale name (nullable)
    ProductStatusId UNIQUEIDENTIFIER, -- Product status ID (nullable, GUID)
    ProductionType INT, -- Enum for ProductionType (stored as INT, nullable)
    Stock INT, -- Stock quantity (nullable)
    ScrapeDate DATETIME, -- Scrape date (nullable)
    ScoreGBFC INT, -- Score GBFC (nullable)
    Relevance INT, -- Relevance score (nullable)
    Market NVARCHAR(255) NOT NULL, -- Market information
    SlotStatus INT, -- Enum for SlotStatus (stored as INT, nullable)

   FOREIGN KEY (MerchantCode) REFERENCES Merchant(Code)
);

-- Create DuplicateGroups table
CREATE TABLE DuplicateGroups (
    Id INT PRIMARY KEY IDENTITY(1,1), -- Auto-incrementing primary key
    Type INT NOT NULL, -- Enum for GroupType (stored as INT)
    MatchType INT NOT NULL, -- Enum for MatchType (stored as INT)
    MatchScore DECIMAL(5, 2), -- Match score (nullable, e.g., 95.50)
    CreatedDate DATETIME NOT NULL, -- Date when the group was created
    ModifiedDate DATETIME, -- Date when the group was last modified (nullable)
    AssigneeUserId UNIQUEIDENTIFIER, -- Assignee user ID (nullable, GUID)
    Status INT NOT NULL, -- Enum for GroupStatus (stored as INT)
    Note NVARCHAR(MAX) -- Optional note (nullable)
);

-- Create DuplicateGroupItems table
CREATE TABLE DuplicateGroupItems (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProductId NVARCHAR(255) NOT NULL,
    DuplicateGroupId INT NOT NULL,
	ItemStatus INT NOT NULL, -- Enum for ItemStatus (stored as INT)
    CONSTRAINT FK_DuplicateGroupItem_Product FOREIGN KEY (ProductId) REFERENCES Products(Id),
    CONSTRAINT FK_DuplicateGroupItem_DuplicateGroup FOREIGN KEY (DuplicateGroupId) REFERENCES DuplicateGroups(Id)
);


CREATE TABLE DigitalAssets (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Url NVARCHAR(500) NOT NULL,
    [Order] INT NOT NULL,
    ProductId NVARCHAR(255) NOT NULL,

    CONSTRAINT FK_DigitalAssets_Products FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE
);



-- Index for fast lookup of digital assets by ProductId
CREATE INDEX IX_DigitalAssets_ProductId ON DigitalAssets(ProductId);

-- Indexes for performance optimization
CREATE INDEX IX_GroupItems_GroupId ON DuplicateGroupItems(DuplicateGroupId);
CREATE INDEX IX_GroupItems_ProductId ON DuplicateGroupItems(ProductId);

SET IDENTITY_INSERT DuplicateGroups ON;

