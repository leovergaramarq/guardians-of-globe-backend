using System;
using System.Collections.Generic;

namespace GuardiansOfGlobe.DataAccess.Models
{
    public partial class AlterEgo
    {
        public AlterEgo()
        {
            AlterEgoAbilities = new HashSet<AlterEgoAbility>();
            AlterEgoFights = new HashSet<AlterEgoFight>();
            AlterEgoWeaknesses = new HashSet<AlterEgoWeakness>();
            Sponsorships = new HashSet<Sponsorship>();
        }

        public decimal AlterEgoId { get; set; }
        public decimal PersonId { get; set; }
        public string? Origin { get; set; }
        public bool? IsHero { get; set; }
        public string? Alias { get; set; }

        public virtual Person Person { get; set; } = null!;
        public virtual ICollection<AlterEgoAbility> AlterEgoAbilities { get; set; }
        public virtual ICollection<AlterEgoFight> AlterEgoFights { get; set; }
        public virtual ICollection<AlterEgoWeakness> AlterEgoWeaknesses { get; set; }
        public virtual ICollection<Sponsorship> Sponsorships { get; set; }
    }
}
