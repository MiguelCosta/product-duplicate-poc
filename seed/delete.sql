DELETE FROM DuplicateGroupItems;
DELETE FROM DuplicateGroups;
DELETE FROM DigitalAssets;
DELETE FROM Products;
DELETE FROM Merchant;
DBCC CHECKIDENT ('DuplicateGroups', RESEED, 0);
DBCC CHECKIDENT ('DuplicateGroupItems', RESEED, 0);
DBCC CHECKIDENT ('DigitalAssets', RESEED, 0);
