using System;
using System.Collections.Generic;

namespace GuardiansOfGlobe.DataAccess.Models
{
    public partial class SponsorSource
    {
        public SponsorSource()
        {
            Sponsors = new HashSet<Sponsor>();
        }

        public decimal SponsorSourceId { get; set; }
        public string? SponsorSourceName { get; set; }
        public string? Reliability { get; set; }

        public virtual ICollection<Sponsor> Sponsors { get; set; }
    }
}
