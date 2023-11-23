using System;
using System.Collections.Generic;

namespace GuardiansOfGlobe.DataAccess.Models
{
    public partial class AlterEgoAbility
    {
        public decimal AlterEgoAbilityId { get; set; }
        public decimal AlterEgoId { get; set; }
        public string? AbilityName { get; set; }

        public virtual AlterEgo AlterEgo { get; set; } = null!;
    }
}
