SELECT [d4].[Id], [d4].[AssigneeUserId], [d4].[CreatedDate], [d4].[MatchScore], [d4].[MatchType], [d4].[ModifiedDate], [d4].[Note], [d4].[Status], [d4].[Type], 
       [s].[Id], [s].[DuplicateGroupId], [s].[ItemStatus], [s].[ProductId], [s].[Id0], [s].[BrandId], [s].[BrandName], [s].[BrandProductId], [s].[Catalog], 
       [s].[CatalogType], [s].[Currency], [s].[Description], [s].[Gender], [s].[MainColourId], [s].[MainColourName], [s].[Market], [s].[MerchantCode], [s].[Name], 
       [s].[PlatformCategoryId], [s].[Price], [s].[ProductStatusId], [s].[ProductionType], [s].[Relevance], [s].[ScoreGBFC], [s].[ScrapeDate], [s].[SizeScaleId], 
       [s].[SizeScaleName], [s].[SlotStatus], [s].[Stock], [s].[WebCategoryId], [s].[Id1], [s].[Order], [s].[ProductId0], [s].[Url]
FROM (
    SELECT [d].[Id], [d].[AssigneeUserId], [d].[CreatedDate], [d].[MatchScore], [d].[MatchType], [d].[ModifiedDate], [d].[Note], [d].[Status], [d].[Type]
    FROM [DuplicateGroups] AS [d]
    WHERE [d].[Status] IN (1, 2) 
    AND [d].[Type] = '0' 
    AND EXISTS (
        SELECT 1
        FROM [DuplicateGroupItems] AS [d0]
        INNER JOIN [Products] AS [p] ON [d0].[ProductId] = [p].[Id]
        WHERE [d].[Id] = [d0].[DuplicateGroupId] 
        AND [p].[ProductionType] IS NOT NULL 
        AND [p].[ProductionType] IN (0)
    ) 
    AND EXISTS (
        SELECT 1
        FROM [DuplicateGroupItems] AS [d1]
        INNER JOIN [Products] AS [p0] ON [d1].[ProductId] = [p0].[Id]
        WHERE [d].[Id] = [d1].[DuplicateGroupId] 
        AND [p0].[ProductStatusId] IS NOT NULL 
        AND [p0].[ProductStatusId] IN ('e605d9bb-2236-461c-a817-61564c0a9ad6', '92696dad-2d31-44bd-bdab-1865d885d6f1', '2699e640-7497-4a24-a2d4-003fd26cf28d')
    )
    ORDER BY [d].[CreatedDate]
    OFFSET 0 ROWS FETCH NEXT 30 ROWS ONLY
) AS [d4]
LEFT JOIN (
    SELECT [d2].[Id], [d2].[DuplicateGroupId], [d2].[ItemStatus], [d2].[ProductId], 
           [p1].[Id] AS [Id0], [p1].[BrandId], [p1].[BrandName], [p1].[BrandProductId], [p1].[Catalog], 
           [p1].[CatalogType], [p1].[Currency], [p1].[Description], [p1].[Gender], [p1].[MainColourId], 
           [p1].[MainColourName], [p1].[Market], [p1].[MerchantCode], [p1].[Name], [p1].[PlatformCategoryId], 
           [p1].[Price], [p1].[ProductStatusId], [p1].[ProductionType], [p1].[Relevance], [p1].[ScoreGBFC], 
           [p1].[ScrapeDate], [p1].[SizeScaleId], [p1].[SizeScaleName], [p1].[SlotStatus], [p1].[Stock], 
           [p1].[WebCategoryId], [d3].[Id] AS [Id1], [d3].[Order], [d3].[ProductId] AS [ProductId0], [d3].[Url]
    FROM [DuplicateGroupItems] AS [d2]
    INNER JOIN [Products] AS [p1] ON [d2].[ProductId] = [p1].[Id]
    LEFT JOIN [DigitalAssets] AS [d3] ON [p1].[Id] = [d3].[ProductId]
) AS [s] ON [d4].[Id] = [s].[DuplicateGroupId]
ORDER BY [d4].[CreatedDate], [d4].[Id], [s].[Id], [s].[Id0]