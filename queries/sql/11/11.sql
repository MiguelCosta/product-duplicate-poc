DECLARE @__groupFilter_GroupStatus_0 NVARCHAR(MAX) = '[1,2]';
DECLARE @__groupFilter_SlotStatus_1 NVARCHAR(MAX) = '[3]';
DECLARE @__groupFilter_Type_2 INT = 0;
DECLARE @__groupFilter_ProductionTypes_3 NVARCHAR(MAX) = '[1]';
DECLARE @__groupFilter_AssignedTo_4 UNIQUEIDENTIFIER = '7f9ad521-2e5c-4fd9-b6d8-1fd4507775a5';
DECLARE @__p_5 INT = 0;
DECLARE @__p_6 INT = 30;

SELECT 
    [d4].[Id], 
    [d4].[AssigneeUserId], 
    [d4].[CreatedDate], 
    [d4].[MatchScore], 
    [d4].[MatchType], 
    [d4].[ModifiedDate], 
    [d4].[Note], 
    [d4].[Status], 
    [d4].[Type], 
    [s].[Id], 
    [s].[DuplicateGroupId], 
    [s].[ItemStatus], 
    [s].[ProductId], 
    [s].[Id0], 
    [s].[BrandId], 
    [s].[BrandName], 
    [s].[BrandProductId], 
    [s].[Catalog], 
    [s].[CatalogType], 
    [s].[Currency], 
    [s].[Description], 
    [s].[Gender], 
    [s].[MainColourId], 
    [s].[MainColourName], 
    [s].[Market], 
    [s].[MerchantCode], 
    [s].[Name], 
    [s].[PlatformCategoryId], 
    [s].[Price], 
    [s].[ProductStatusId], 
    [s].[ProductionType], 
    [s].[Relevance], 
    [s].[ScoreGBFC], 
    [s].[ScrapeDate], 
    [s].[SizeScaleId], 
    [s].[SizeScaleName], 
    [s].[SlotStatus], 
    [s].[Stock], 
    [s].[WebCategoryId], 
    [s].[Id1], 
    [s].[Order], 
    [s].[ProductId0], 
    [s].[Url]
FROM (
    SELECT 
        [d].[Id], 
        [d].[AssigneeUserId], 
        [d].[CreatedDate], 
        [d].[MatchScore], 
        [d].[MatchType], 
        [d].[ModifiedDate], 
        [d].[Note], 
        [d].[Status], 
        [d].[Type]
    FROM [DuplicateGroups] AS [d]
    WHERE [d].[Status] IN (
        SELECT [g].[value]
        FROM OPENJSON(@__groupFilter_GroupStatus_0) WITH ([value] int '$') AS [g]
    ) 
    AND EXISTS (
        SELECT 1
        FROM [DuplicateGroupItems] AS [d0]
        INNER JOIN [Products] AS [p] ON [d0].[ProductId] = [p].[Id]
        WHERE [d].[Id] = [d0].[DuplicateGroupId] 
        AND [p].[SlotStatus] IS NOT NULL 
        AND [p].[SlotStatus] IN (
            SELECT [g0].[value]
            FROM OPENJSON(@__groupFilter_SlotStatus_1) WITH ([value] int '$') AS [g0]
        )
    ) 
    AND [d].[Type] = @__groupFilter_Type_2 
    AND EXISTS (
        SELECT 1
        FROM [DuplicateGroupItems] AS [d1]
        INNER JOIN [Products] AS [p0] ON [d1].[ProductId] = [p0].[Id]
        WHERE [d].[Id] = [d1].[DuplicateGroupId] 
        AND [p0].[ProductionType] IS NOT NULL 
        AND [p0].[ProductionType] IN (
            SELECT [g1].[value]
            FROM OPENJSON(@__groupFilter_ProductionTypes_3) WITH ([value] int '$') AS [g1]
        )
    ) 
    AND [d].[AssigneeUserId] = @__groupFilter_AssignedTo_4 
    AND [d].[Type] = @__groupFilter_Type_2
    ORDER BY [d].[CreatedDate]
    OFFSET @__p_5 ROWS 
    FETCH NEXT @__p_6 ROWS ONLY
) AS [d4]
LEFT JOIN (
    SELECT 
        [d2].[Id], 
        [d2].[DuplicateGroupId], 
        [d2].[ItemStatus], 
        [d2].[ProductId], 
        [p1].[Id] AS [Id0], 
        [p1].[BrandId], 
        [p1].[BrandName], 
        [p1].[BrandProductId], 
        [p1].[Catalog], 
        [p1].[CatalogType], 
        [p1].[Currency], 
        [p1].[Description], 
        [p1].[Gender], 
        [p1].[MainColourId], 
        [p1].[MainColourName], 
        [p1].[Market], 
        [p1].[MerchantCode], 
        [p1].[Name], 
        [p1].[PlatformCategoryId], 
        [p1].[Price], 
        [p1].[ProductStatusId], 
        [p1].[ProductionType], 
        [p1].[Relevance], 
        [p1].[ScoreGBFC], 
        [p1].[ScrapeDate], 
        [p1].[SizeScaleId], 
        [p1].[SizeScaleName], 
        [p1].[SlotStatus], 
        [p1].[Stock], 
        [p1].[WebCategoryId], 
        [d3].[Id] AS [Id1], 
        [d3].[Order], 
        [d3].[ProductId] AS [ProductId0], 
        [d3].[Url]
    FROM [DuplicateGroupItems] AS [d2]
    INNER JOIN [Products] AS [p1] ON [d2].[ProductId] = [p1].[Id]
    LEFT JOIN [DigitalAssets] AS [d3] ON [p1].[Id] = [d3].[ProductId]
) AS [s] ON [d4].[Id] = [s].[DuplicateGroupId]
ORDER BY [d4].[CreatedDate], [d4].[Id], [s].[Id], [s].[Id0];