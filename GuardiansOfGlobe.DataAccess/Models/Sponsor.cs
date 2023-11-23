using System;
using System.Collections.Generic;

namespace GuardiansOfGlobe.DataAccess.Models
{
    public partial class Sponsor
    {
        public Sponsor()
        {
            Sponsorships = new HashSet<Sponsorship>();
        }

        public decimal SponsorId { get; set; }
        public decimal? SponsorSourceId { get; set; }
        public string? SponsorName { get; set; }

        public virtual SponsorSource? SponsorSource { get; set; }
        public virtual ICollection<Sponsorship> Sponsorships { get; set; }
    }
}
