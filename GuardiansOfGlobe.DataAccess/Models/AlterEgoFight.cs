using System;
using System.Collections.Generic;

namespace GuardiansOfGlobe.DataAccess.Models
{
    public partial class AlterEgoFight
    {
        public decimal AlterEgoFightId { get; set; }
        public decimal AlterEgoId { get; set; }
        public decimal FightId { get; set; }
        public bool? Victory { get; set; }
        public bool? Side { get; set; }

        public virtual AlterEgo AlterEgo { get; set; } = null!;
        public virtual Fight Fight { get; set; } = null!;
    }
}
