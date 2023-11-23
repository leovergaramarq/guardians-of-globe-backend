using System;
using System.Collections.Generic;

namespace GuardiansOfGlobe.DataAccess.Models
{
    public partial class Relationship
    {
        public decimal RelationshipId { get; set; }
        public decimal Person1Id { get; set; }
        public decimal Person2Id { get; set; }
        public string? RelationshipType { get; set; }

        public virtual Person Person1 { get; set; } = null!;
        public virtual Person Person2 { get; set; } = null!;
    }
}
