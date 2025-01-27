namespace ProductDuplicatePoC.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Group
{
    [BsonId] public int Id { get; set; }

    [BsonRepresentation(BsonType.String)] public GroupType Type { get; set; }

    [BsonRepresentation(BsonType.String)] public MatchType MatchType { get; set; }

    public decimal? MatchScore { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? MatchDate { get; set; }

    public DateTime? MergeDate { get; set; }

    [BsonRepresentation(BsonType.String)] public Guid? AssigneeUserId { get; set; }

    [BsonRepresentation(BsonType.String)] public GroupStatus Status { get; set; }

    public string? Note { get; set; }

    public List<Product> Items { get; set; }

    // To use in the queries
    public bool HasNotes
    {
        get { return !string.IsNullOrWhiteSpace(this.Note); }
        set { }
    }

    public bool HasChildDigitalAsset
    {
        get { return this.Items.Any(x => x is { Status: Models.Status.MarkedAsChild, DigitalAssets.Count: > 0 }); }
        set { }
    }

    public bool HasChildStock
    {
        get { return this.Items.Any(x => x is { Status: Models.Status.MarkedAsChild, Stock: > 0 }); }
        set { }
    }

    public bool HasParentStock
    {
        get { return this.Items.Any(x => x is { Status: Models.Status.MarkedAsParent, Stock: > 0 }); }
        set { }
    }

    public List<int> ChildMerchantsIds
    {
        get
        {
            return this.Items
                .Where(x => x is { Status: Models.Status.MarkedAsChild, MerchantCodes.Count: > 0 })
                .SelectMany(x => x.MerchantCodes)
                .Distinct()
                .ToList();
        }
        set { }
    }

    public List<SlotStatus> ChildSlotStatus
    {
        get
        {
            return this.Items
                .Where(x => x is { Status: Models.Status.MarkedAsChild, SlotStatus: not null })
                .Select(x => x.SlotStatus!.Value)
                .ToList();
        }
        set { }
    }

    public List<ProductionType> ChildProductionTypeStatus
    {
        get
        {
            return this.Items
                .Where(x => x is { Status: Models.Status.MarkedAsChild, ProductionType: not null })
                .Select(x => x.ProductionType!.Value)
                .ToList();
        }
        set { }
    }

    public int Relevance
    {
        get
        {
            return this.Items
                .Select(x => x.Relevance ?? 0)
                .Max();
        }
        set { }
    }
}

public enum GroupType
{
    Farfetch,
    FarfetchCompetitor
}

public class Product
{
    public string Id { get; set; }

    public string Catalog { get; set; }

    [BsonRepresentation(BsonType.String)] public CatalogType CatalogType { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    [BsonRepresentation(BsonType.String)] public Status Status { get; set; }

    public decimal? Price { get; set; }

    public string? Currency { get; set; }

    public string? BrandProductId { get; set; }

    [BsonRepresentation(BsonType.String)] public Guid? MainColourId { get; set; }

    public string? MainColourName { get; set; }

    [BsonRepresentation(BsonType.String)] public Guid? BrandId { get; set; }

    public string? BrandName { get; set; }

    public string? Gender { get; set; }

    public List<Category> PlatformCategories { get; set; }

    public List<Category> WebCategories { get; set; }

    public List<string> Compositions { get; set; }

    public string? Measurements { get; set; }

    [BsonRepresentation(BsonType.String)] public Guid? SizeScaleId { get; set; }

    public string? SizeScaleName { get; set; }

    [BsonRepresentation(BsonType.String)]
    public Guid? ProductStatusId { get; set; }

    public ProductionType? ProductionType { get; set; }

    public int? Stock { get; set; }

    public List<DigitalAsset> DigitalAssets { get; set; }

    public DateTime? ScrapeDate { get; set; }

    public int? ScoreGBFC { get; set; }

    public int? Relevance { get; set; }

    public List<int>? MerchantCodes { get; set; }

    public string Market { get; set; }

    public SlotStatus? SlotStatus { get; set; }
}

public enum Status
{
    None = 0,
    MarkedAsChild = 1,
    MarkedAsParent = 2,
    Rejected = 3
}

public class DigitalAsset
{
    public int? Id { get; set; }

    public string Url { get; set; }

    public int Order { get; set; }
}

public enum ProductionType
{
    Regular = 0,
    VPI = 1
}

public class Category
{
    [BsonRepresentation(BsonType.String)] public Guid? Id { get; set; }

    public string Name { get; set; }

    public int Order { get; set; }
}

public enum CatalogType
{
    Farfetch = 0,
    Competitor = 1
}

public enum MatchType
{
    Manual = 0,
    Automatic = 1
}

public enum GroupStatus
{
    None = 0,
    Created = 1,
    Matched = 2,
    Rejected = 3,
    Merged = 4,
    Unmerged = 5
}

public enum SlotStatus
{
    None = 0,
    Open = 1,
    ReadyToSend = 2,
    Allocated = 3,
    InProduction = 4,
    Dispatched = 5
}
