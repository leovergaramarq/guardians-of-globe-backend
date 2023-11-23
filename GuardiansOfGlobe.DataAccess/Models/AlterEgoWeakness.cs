using System;
using System.Collections.Generic;

namespace GuardiansOfGlobe.DataAccess.Models
{
    public partial class AlterEgoWeakness
    {
        public decimal AlterEgoWeaknessId { get; set; }
        public decimal AlterEgoId { get; set; }
        public string? WeaknessName { get; set; }

        public virtual AlterEgo AlterEgo { get; set; } = null!;
    }
}
