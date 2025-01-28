namespace ProductDuplicatePoC.Models.Sql
{
    using ProductDuplicatePoC.Dtos;

    public class DuplicateGroupItem
    {
        public int Id { get; set; }

        public int DuplicateGroupId { get;set;}

        public DuplicateGroup Group { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }

        public ItemStatus ItemStatus { get; set; }
    }
}
