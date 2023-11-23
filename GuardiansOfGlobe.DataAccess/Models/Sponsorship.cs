using System;
using System.Collections.Generic;

namespace GuardiansOfGlobe.DataAccess.Models
{
    public partial class Sponsorship
    {
        public decimal SponsorshipId { get; set; }
        public decimal SponsorId { get; set; }
        public decimal AlterEgoId { get; set; }

        public virtual AlterEgo AlterEgo { get; set; } = null!;
        public virtual Sponsor Sponsor { get; set; } = null!;
    }
}
