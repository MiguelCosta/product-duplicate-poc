namespace ProductDuplicatePoC.Models;

using System;
using System.Collections.Generic;

public class GroupFilter
{
    public Guid? AssignedTo { get; set; }
    public List<Guid?>? BrandIds { get; set; }
    public List<string>? BrandProductIds { get; set; }
    public GroupType? Type { get; set; }
    public List<string>? Catalogues { get; set; }
    public bool? ChildHasDigitalAsset { get; set; }
    public bool? ChildHasStock { get; set; }
    public List<int>? ChildMerchantIds { get; set; }
    public DateTime? CreatedDateStart { get; set; }
    public DateTime? CreatedDateEnd { get; set; }
    public List<string>? Genders { get; set; }
    public List<int>? GroupIds { get; set; }
    public List<GroupStatus>? GroupStatus { get; set; }
    public bool? HasNotes { get; set; }
    public List<string>? ItemIds { get; set; }
    public List<Status>? ItemStatus { get; set; }
    public List<string>? Markets { get; set; }
    public decimal? MatchScoreMin { get; set; }
    public decimal? MatchScoreMax { get; set; }
    public MatchType? MatchType { get; set; }
    public DateTime? MergeDateStart { get; set; }
    public DateTime? MergeDateEnd { get; set; }
    public DateTime? ModifiedDateStart { get; set; }
    public DateTime? ModifiedDateEnd { get; set; }
    public DateTime? MatchDateStart { get; set; }
    public DateTime? MatchDateEnd { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 180;
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
