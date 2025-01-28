SELECT [d2].[Id], [d2].[AssigneeUserId], [d2].[CreatedDate], [d2].[MatchScore], [d2].[MatchType], [d2].[ModifiedDate], [d2].[Note], [d2].[Status], [d2].[Type], 
       [s].[Id], [s].[DuplicateGroupId], [s].[ItemStatus], [s].[ProductId], [s].[Id0], [s].[BrandId], [s].[BrandName], [s].[BrandProductId], [s].[Catalog], 
       [s].[CatalogType], [s].[Currency], [s].[Description], [s].[Gender], [s].[MainColourId], [s].[MainColourName], [s].[Market], [s].[MerchantCode], 
       [s].[Name], [s].[PlatformCategoryId], [s].[Price], [s].[ProductStatusId], [s].[ProductionType], [s].[Relevance], [s].[ScoreGBFC], [s].[ScrapeDate], 
       [s].[SizeScaleId], [s].[SizeScaleName], [s].[SlotStatus], [s].[Stock], [s].[WebCategoryId], [s].[Id1], [s].[Order], [s].[ProductId0], [s].[Url]
FROM (
    SELECT [d].[Id], [d].[AssigneeUserId], [d].[CreatedDate], [d].[MatchScore], [d].[MatchType], [d].[ModifiedDate], [d].[Note], [d].[Status], [d].[Type]
    FROM [DuplicateGroups] AS [d]
    WHERE [d].[Type] = 1 
      AND [d].[AssigneeUserId] = '7f9ad521-2e5c-4fd9-b6d8-1fd4507775a5'
      AND [d].[Type] = 1
    ORDER BY [d].[ModifiedDate]
    OFFSET 0 ROWS FETCH NEXT 30 ROWS ONLY
) AS [d2]
LEFT JOIN (
    SELECT [d0].[Id], [d0].[DuplicateGroupId], [d0].[ItemStatus], [d0].[ProductId], [p].[Id] AS [Id0], [p].[BrandId], [p].[BrandName], [p].[BrandProductId], 
           [p].[Catalog], [p].[CatalogType], [p].[Currency], [p].[Description], [p].[Gender], [p].[MainColourId], [p].[MainColourName], [p].[Market], 
           [p].[MerchantCode], [p].[Name], [p].[PlatformCategoryId], [p].[Price], [p].[ProductStatusId], [p].[ProductionType], [p].[Relevance], 
           [p].[ScoreGBFC], [p].[ScrapeDate], [p].[SizeScaleId], [p].[SizeScaleName], [p].[SlotStatus], [p].[Stock], [p].[WebCategoryId], [d1].[Id] AS [Id1], 
           [d1].[Order], [d1].[ProductId] AS [ProductId0], [d1].[Url]
    FROM [DuplicateGroupItems] AS [d0]
    INNER JOIN [Products] AS [p] ON [d0].[ProductId] = [p].[Id]
    LEFT JOIN [DigitalAssets] AS [d1] ON [p].[Id] = [d1].[ProductId]
) AS [s] ON [d2].[Id] = [s].[DuplicateGroupId]
ORDER BY [d2].[ModifiedDate], [d2].[Id], [s].[Id], [s].[Id0]
