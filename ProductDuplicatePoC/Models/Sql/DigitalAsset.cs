namespace ProductDuplicatePoC.Models.Sql
{
    public class DigitalAsset
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public int Order { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }
    }
}
