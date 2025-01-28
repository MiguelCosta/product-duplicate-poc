namespace ProductDuplicatePoC.Models.Sql
{
    using System;
    using System.Collections.Generic;

    public class DuplicateGroup
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

        public ICollection<DuplicateGroupItem> GroupItems { get; set; } = new List<DuplicateGroupItem>();
    }
}
