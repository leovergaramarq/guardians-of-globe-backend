using System;
using System.Collections.Generic;

namespace GuardiansOfGlobe.DataAccess.Models
{
    public partial class Fight
    {
        public Fight()
        {
            AlterEgoFights = new HashSet<AlterEgoFight>();
        }

        public decimal FightId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Location { get; set; } = null!;
        public string FightTitle { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<AlterEgoFight> AlterEgoFights { get; set; }
    }
}
