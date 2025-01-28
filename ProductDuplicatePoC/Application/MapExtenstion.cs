namespace ProductDuplicatePoC.Application;

using System.Collections.Generic;
using System.Linq;
using NetTopologySuite.Index.HPRtree;
using ProductDuplicatePoC.Dtos;

public static class MapExtenstion
{
    public static List<Models.Group> ToModel(this List<Dtos.Group> groups) => groups.Select(x => x.ToModel()).ToList();

    public static Models.Group ToModel(this Dtos.Group group)
    {
        return new Models.Group
        {
            Id = group.Id,
            Type = (Models.GroupType)group.Type,
            MatchType = (Models.MatchType)group.MatchType,
            Note = group.Note,
            Status = (Models.GroupStatus)group.Status,
            CreatedDate = group.CreatedDate,
            MatchScore = group.MatchScore,
            ModifiedDate = group.ModifiedDate,
            AssigneeUserId = group.AssigneeUserId,
            Items = group.Items
                .Select(x => new Models.Product
                {
                    Id = x.Id,
                    Catalog = x.Catalog,
                    CatalogType = (Models.CatalogType)x.CatalogType,
                    Name = x.Name,
                    Compositions = x.Compositions,
                    Price = x.Price,
                    Description = x.Description,
                    BrandName = x.BrandName,
                    Measurements = x.Measurements,
                    Relevance = x.Relevance,
                    ProductionType = (Models.ProductionType?)x.ProductionType,
                    ProductStatusId = x.ProductStatusId,
                    ScrapeDate = x.ScrapeDate,
                    Currency = x.Currency,
                    BrandProductId = x.BrandProductId,
                    MainColourId = x.MainColourId,
                    MainColourName = x.MainColourName,
                    SizeScaleId = x.SizeScaleId,
                    SizeScaleName = x.SizeScaleName,
                    ScoreGBFC = x.ScoreGBFC,
                    BrandId = x.BrandId,
                    Gender = x.Gender,
                    Stock = x.Stock,
                    SlotStatus = (Models.SlotStatus?)x.SlotStatus,
                    MerchantCodes = x.MerchantCodes,
                    Status = (Models.Status)x.Status,
                    Market = x.Market,
                    DigitalAssets = x.DigitalAssets?
                        .Select(d => new Models.DigitalAsset
                        {
                            Id = d.Id,
                            Url = d.Url,
                            Order = d.Order
                        })
                        .ToList(),
                    PlatformCategories = x.PlatformCategories?
                        .Select(pc => new Models.Category
                        {
                            Id = pc.Id,
                            Name = pc.Name,
                            Order = pc.Order
                        })
                        .ToList(),
                    WebCategories = x.WebCategories?
                        .Select(wc => new Models.Category
                        {
                            Id = wc.Id,
                            Name = wc.Name,
                            Order = wc.Order
                        })
                        .ToList()
                })
                .ToList()
        };
    }

    public static List<Dtos.Group> ToDto(this List<Models.Group> groups) => groups.Select(x => x.ToDto()).ToList();

    public static Dtos.Group ToDto(this Models.Group model) => new Group
    {
        Id = model.Id,
        MatchScore = model.MatchScore,
        ModifiedDate = model.ModifiedDate,
        AssigneeUserId = model.AssigneeUserId,
        MatchType = (Dtos.MatchType)model.MatchType,
        Note = model.Note,
        Status = (Dtos.GroupStatus)model.Status,
        CreatedDate = model.CreatedDate,
        Type = (Dtos.GroupType)model.Type,
        Items = model.Items.Select(x => new Dtos.Product
        {
            Id = x.Id,
            Catalog = x.Catalog,
            CatalogType = (Dtos.CatalogType)x.CatalogType,
            Name = x.Name,
            Compositions = x.Compositions,
            Price = x.Price,
            Description = x.Description,
            BrandName = x.BrandName,
            Measurements = x.Measurements,
            Relevance = x.Relevance,
            ProductionType = (Dtos.ProductionType?)x.ProductionType,
            ProductStatusId = x.ProductStatusId,
            ScrapeDate = x.ScrapeDate,
            Currency = x.Currency,
            BrandProductId = x.BrandProductId,
            MainColourId = x.MainColourId,
            MainColourName = x.MainColourName,
            SizeScaleId = x.SizeScaleId,
            SizeScaleName = x.SizeScaleName,
            ScoreGBFC = x.ScoreGBFC,
            BrandId = x.BrandId,
            Gender = x.Gender,
            Stock = x.Stock,
            DigitalAssets = x.DigitalAssets?
                .Select(d => new Dtos.DigitalAsset
                {
                    Id = d.Id,
                    Url = d.Url,
                    Order = d.Order
                })
                .ToList(),
            PlatformCategories = x.PlatformCategories?
                .Select(pc => new Dtos.Category
                {
                    Id = pc.Id,
                    Name = pc.Name,
                    Order = pc.Order
                })
                .ToList(),
            WebCategories = x.WebCategories?
                .Select(wc => new Dtos.Category
                {
                    Id = wc.Id,
                    Name = wc.Name,
                    Order = wc.Order
                })
                .ToList()
        }).ToList()
    };

    public static Models.GroupFilter ToModel(this Dtos.GroupFilter filter) => new Models.GroupFilter
    {
        AssignedTo = filter.AssignedTo,
        BrandIds = filter.BrandIds?.Select(x => (System.Guid?)x).ToList(),
        BrandProductIds = filter.BrandProductIds,
        Type = filter.CatalogType.HasValue ? (Models.GroupType)filter.CatalogType : null,
        Catalogues = filter.Catalogues,
        ChildHasDigitalAsset = filter.ChildHasDigitalAsset,
        ChildHasStock = filter.ChildHasStock,
        ChildMerchantIds = filter.ChildMerchantIds,
        CreatedDateStart = filter.CreatedDateStart,
        CreatedDateEnd = filter.CreatedDateEnd,
        Genders = filter.Genders,
        GroupIds = filter.GroupIds,
        GroupStatus = filter.Status?.Select(x => (Models.GroupStatus)x).ToList(),
        HasNotes = filter.HasNotes,
        ItemIds = filter.ItemIds,
        ItemStatus = filter.ItemStatus?.Select(x => (Models.Status)x).ToList(),
        Markets = filter.Markets,
        MatchScoreMin = filter.MatchScoreMin,
        MatchScoreMax = filter.MatchScoreMax,
        MatchType = filter.MatchType.HasValue ? (Models.MatchType)filter.MatchType : null,
        MergeDateStart = filter.MergeDateStart,
        MergeDateEnd = filter.MergeDateEnd,
        ModifiedDateStart = filter.ModifiedDateStart,
        ModifiedDateEnd = filter.ModifiedDateEnd,
        MatchDateStart = filter.MatchDateStart,
        MatchDateEnd = filter.MatchDateEnd,
        ParentHasStock = filter.ParentHasStock,
        PlatformCategoryIds = filter.PlatformCategoryIds,
        ProductStatus = filter.ProductStatus,
        ProductionTypes = filter.ProductionTypes?.Select(x => (Models.ProductionType)x).ToList(),
        ScrapeDateStart = filter.ScrapeDateStart,
        ScrapeDateEnd = filter.ScrapeDateEnd,
        SlotStatus = filter.SlotStatus?.Select(x => (Models.SlotStatus)x).ToList(),
        Sort = (Models.SortOptions?)filter.Sort,
        Page = filter.Page ?? 1,
        PageSize = filter.PageSize ?? 180
    };

    public static List<Dtos.Group> ToDto(this List<Models.Sql.DuplicateGroup> groups) => groups.Select(x => x.ToDto()).ToList();

    public static Dtos.Group ToDto(this Models.Sql.DuplicateGroup model) => new Group
    {
        Id = model.Id,
        MatchScore = model.MatchScore,
        ModifiedDate = model.ModifiedDate,
        AssigneeUserId = model.AssigneeUserId,
        MatchType = (Dtos.MatchType)model.MatchType,
        Note = model.Note,
        Status = (Dtos.GroupStatus)model.Status,
        CreatedDate = model.CreatedDate,
        Type = (Dtos.GroupType)model.Type,
        Items = model.GroupItems.Select(x => new Dtos.Product
        {
            Id = x.ProductId,
            Catalog = x.Product.Catalog,
            CatalogType = (Dtos.CatalogType)x.Product.CatalogType,
            Name = x.Product.Name,
            Price = x.Product.Price,
            Description = x.Product.Description,
            BrandName = x.Product.BrandName,
            Relevance = x.Product.Relevance,
            ProductionType = (Dtos.ProductionType?)x.Product.ProductionType,
            ProductStatusId = x.Product.ProductStatusId,
            ScrapeDate = x.Product.ScrapeDate,
            Currency = x.Product.Currency,
            BrandProductId = x.Product.BrandProductId,
            MainColourId = x.Product.MainColourId,
            MainColourName = x.Product.MainColourName,
            SizeScaleId = x.Product.SizeScaleId,
            SizeScaleName = x.Product.SizeScaleName,
            ScoreGBFC = x.Product.ScoreGBFC,
            BrandId = x.Product.BrandId,
            Gender = x.Product.Gender,
            Stock = x.Product.Stock,
            DigitalAssets = x.Product.DigitalAssets.Select(y => new DigitalAsset
            {
                Id = y.Id,
                Order = y.Order,
                Url = y.Url
            }).ToList(),
            Market = x.Product.Market,
            MerchantCodes = new List<int> { x.Product.MerchantCode.GetValueOrDefault() },
            SlotStatus = (SlotStatus?)x.Product.SlotStatus,
            PlatformCategories = new List<Category> { },
            Measurements = string.Empty,
            Compositions = new List<string> { },
            WebCategories = new List<Category> { },
            Status = (Status)x.ItemStatus
        }).ToList()
    };

    public static Models.Sql.DuplicateGroup ToModelSql(this Dtos.Group group)
    {
        return new Models.Sql.DuplicateGroup
        {
            Id = group.Id,
            CreatedDate = group.CreatedDate,
            AssigneeUserId = group.AssigneeUserId,
            MatchScore = group.MatchScore,
            MatchType = (Models.MatchType)group.MatchType,
            Note = group.Note,
            ModifiedDate = group.ModifiedDate,
            Type = (Models.GroupType)group.Type,
            Status = (Models.GroupStatus)group.Status,
            GroupItems = group.Items.Select(item => new Models.Sql.DuplicateGroupItem
            {
                DuplicateGroupId = group.Id,
                ItemStatus = (ItemStatus)item.Status,
                ProductId = item.Id
            }).ToList()
        };
    }

    public static List<Models.Sql.Product> ToModelProductSql(this Dtos.Group group)
    {
        return group.Items.Select(item => new Models.Sql.Product
        {
            Id = item.Id,
            BrandId = item.BrandId,
            BrandName = item.BrandName,
            BrandProductId = item.BrandProductId,
            Catalog = item.Catalog,
            CatalogType = (Models.CatalogType)item.CatalogType,
            Currency = item.Currency,
            Description = item.Description,
            DigitalAssets = item.DigitalAssets?.Select(d => new Models.Sql.DigitalAsset
            {
                ProductId = item.Id,
                Order = d.Order,
                Url = d.Url,
            })?.ToList(),
            Gender = item.Gender,
            MainColourId = item.MainColourId,
            MainColourName = item.MainColourName,
            Market = item.Market,
            MerchantCode = item.MerchantCodes?.FirstOrDefault() ?? null,
            PlatformCategoryId = item.PlatformCategories?.FirstOrDefault()?.Id,
            Name = item.Name,
            Price = item.Price,
            ProductionType = (Models.ProductionType?)item.ProductionType,
            ProductStatusId = item.ProductStatusId,
            Relevance = item.Relevance,
            ScoreGBFC = item.ScoreGBFC,
            ScrapeDate = item.ScrapeDate,
            SizeScaleId = item.SizeScaleId,
            SizeScaleName = item.SizeScaleName,
            SlotStatus = (Models.SlotStatus?)item.SlotStatus,
            WebCategoryId = item.WebCategories?.FirstOrDefault()?.Id,
            Stock = item.Stock,
        }).ToList();
    }

    public static List<Models.Sql.DuplicateGroup> ToSqlModel(this List<Dtos.Group> groups) => groups.Select(x => x.ToModelSql()).ToList();

}
