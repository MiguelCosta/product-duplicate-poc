namespace ProductDuplicatePoC.Dtos;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Group
{
    public int Id { get; set; }

    public GroupType Type { get; set; }

    public MatchType MatchType { get; set; }

    public decimal? MatchScore { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? AssigneeUserId { get; set; }

    public GroupStatus Status { get; set; }

    public string? Note { get; set; }

    public List<Product> Items { get; set; }
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

    public CatalogType CatalogType { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public ItemStatus ItemStatus { get; set; }

    public decimal? Price { get; set; }

    public string? Currency { get; set; }

    public string? BrandProductId { get; set; }

    public Guid? MainColourId { get; set; }

    public string? MainColourName { get; set; }

    public Guid? BrandId { get; set; }

    public string? BrandName { get; set; }

    public string? Gender { get; set; }

    public List<Category>? PlatformCategories { get; set; }

    public List<Category>? WebCategories { get; set; }

    public List<string>? Compositions { get; set; }

    public string? Measurements { get; set; }

    public Guid? SizeScaleId { get; set; }

    public string? SizeScaleName { get; set; }

    public Guid? ProductStatusId { get; set; }

    public ProductionType? ProductionType { get; set; }

    public SlotStatus? SlotStatus { get; set; }

    public int? Stock { get; set; }

    public List<DigitalAsset>? DigitalAssets { get; set; }

    public DateTime? ScrapeDate { get; set; }

    public int? ScoreGBFC { get; set; }

    public int? Relevance { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ItemStatus
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

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProductionType
{
    Regular = 0,
    VPI = 1
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProductStatus
{
    Online = 0,
    Offline = 1
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SlotStatus
{
    None = 0,
    Open = 1,
    ReadyToSend = 2,
    Allocated = 3,
    InProduction = 4,
    Dispatched = 5
}

public class Category
{
    public Guid? Id { get; set; }

    public string Name { get; set; }

    public int Order { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CatalogType
{
    Farfetch = 0,
    Competitor = 1
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MatchType
{
    Manual = 0,
    Automatic = 1,
    Outsource = 2
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GroupStatus
{
    None = 0,
    Created = 1,
    Matched = 2,
    Rejected = 3,
    Merged = 4,
    Unmerged = 5
}
