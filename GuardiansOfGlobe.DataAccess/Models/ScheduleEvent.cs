using System;
using System.Collections.Generic;

namespace GuardiansOfGlobe.DataAccess.Models
{
    public partial class ScheduleEvent
    {
        public decimal ScheduleEventId { get; set; }
        public decimal PersonId { get; set; }
        public string? EventName { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public virtual Person Person { get; set; } = null!;
    }
}
