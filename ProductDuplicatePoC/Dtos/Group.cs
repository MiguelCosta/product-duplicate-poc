namespace ProductDuplicatePoC.Dtos;

using System;
using System.Collections.Generic;

public class Group
{
    public int Id { get; set; }

    public MatchType MatchType { get; set; }

    public decimal? MatchScore { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid? AssigneeUserId { get; set; }

    public GroupStatus Status { get; set; }

    public string? Note { get; set; }

    public List<Product> Products { get; set; }
}

public class Product
{
    public int Id { get; set; }

    public string Catalog { get; set; }

    public CatalogType CatalogType { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public string? Currency { get; set; }

    public string? BrandProductId { get; set; }

    public Guid? MainColourId { get; set; }

    public string? MainColourName { get; set; }

    public Guid? BrandId { get; set; }

    public string? BrandName { get; set; }

    public string? Gender { get; set; }

    public List<Category> PlatformCategories { get; set; }

    public List<Category> WebCategories { get; set; }

    public List<string> Compositions { get; set; }

    public string? Measurements { get; set; }

    public Guid? SizeScaleId { get; set; }

    public string? SizeScaleName { get; set; }

    public ProductStatus? ProductStatus { get; set; }

    public ProductionType? ProductionType { get; set; }

    public int? Stock { get; set; }

    public List<DigitalAsset> DigitalAssets { get; set; }

    public DateTime? ScrapeDate { get; set; }

    public int? ScoreGBFC { get; set; }

    public int? Relevance { get; set; }
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

public enum ProductStatus
{
    Online = 0,
    Offline = 1
}

public class Category
{
    public Guid Id { get; set; }

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
    Automatic = 1,
    Outsource = 2
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
