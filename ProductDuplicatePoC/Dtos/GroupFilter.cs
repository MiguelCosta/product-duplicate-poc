namespace ProductDuplicatePoC.Dtos;

using System;
using System.Collections.Generic;

public class GroupFilter
{
    public Guid? AssignedTo { get; set; }
    public List<Guid>? BrandIds { get; set; }
    public List<string>? BrandProductIds { get; set; }
    public GroupType? CatalogType { get; set; }
    public List<string>? Catalogues { get; set; }
    public bool? ChildHasDigitalAsset { get; set; }
    public bool? ChildHasStock { get; set; }
    public List<int>? ChildMerchantIds { get; set; }
    public DateTime? CreatedDateStart { get; set; }
    public DateTime? CreatedDateEnd { get; set; }
    public List<string>? Genders { get; set; }
    public List<int>? GroupIds { get; set; }
    public List<GroupStatus>? Status { get; set; }
    public bool? HasNotes { get; set; }
    public List<string>? ItemIds { get; set; }
    public List<ItemStatus>? ItemStatus { get; set; }
    public List<string>? Markets { get; set; }
    public int? MatchScoreMin { get; set; }
    public int? MatchScoreMax { get; set; }
    public MatchType? MatchType { get; set; }
    public DateTime? MergeDateStart { get; set; }
    public DateTime? MergeDateEnd { get; set; }
    public DateTime? ModifiedDateStart { get; set; }
    public DateTime? ModifiedDateEnd { get; set; }
    public DateTime? MatchDateStart { get; set; }
    public DateTime? MatchDateEnd { get; set; }
    public List<string>? MerchantCodes { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
    public bool? ParentHasStock { get; set; }
    public List<Guid?>? PlatformCategoryIds { get; set; }
    public List<ProductionType>? ProductionTypes { get; set; }
    public DateTime? ScrapeDateStart { get; set; }
    public DateTime? ScrapeDateEnd { get; set; }
    public List<SlotStatus>? SlotStatus { get; set; }
    public List<Guid?>? ProductStatus { get; set; }

    public SortOptions? Sort { get; set; }
}

public enum SortOptions
{
    RelevanceDesc,
    CreatedDateAsc,
    ModifiedDateAsc,
    MatchDateAsc,
    MatchScoreDesc
}
