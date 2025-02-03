namespace ProductDuplicatePoC.Models.Sql
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Merchant
    {
        [Key] // ✅ Mark it as the primary key
        public int Code { get; set; }

        public string Name { get; set; }
    }
}
