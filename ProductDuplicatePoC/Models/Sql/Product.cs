namespace ProductDuplicatePoC.Models.Sql
{
    using System;
    using System.Collections.Generic;

    public class Product
    {
        public string Id { get; set; }

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

        public int? MerchantCode { get; set; }

        public Guid? PlatformCategoryId { get; set; }

        public Guid? WebCategoryId { get; set; }

        public Guid? SizeScaleId { get; set; }

        public string? SizeScaleName { get; set; }

        public Guid? ProductStatusId { get; set; }

        public ProductionType? ProductionType { get; set; }

        public int? Stock { get; set; }

        public DateTime? ScrapeDate { get; set; }

        public int? ScoreGBFC { get; set; }

        public int? Relevance { get; set; }

        public string Market { get; set; }

        public SlotStatus? SlotStatus { get; set; }

        public ICollection<DigitalAsset> DigitalAssets { get; set; }
    }
}
