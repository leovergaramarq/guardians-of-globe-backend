using System;
using System.Collections.Generic;

namespace GuardiansOfGlobe.DataAccess.Models
{
    public partial class Person
    {
        public Person()
        {
            AlterEgos = new HashSet<AlterEgo>();
            RelationshipPerson1s = new HashSet<Relationship>();
            RelationshipPerson2s = new HashSet<Relationship>();
            ScheduleEvents = new HashSet<ScheduleEvent>();
        }

        public decimal PersonId { get; set; }
        public string? PersonName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Sex { get; set; }
        public string? Occupation { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<AlterEgo> AlterEgos { get; set; }
        public virtual ICollection<Relationship> RelationshipPerson1s { get; set; }
        public virtual ICollection<Relationship> RelationshipPerson2s { get; set; }
        public virtual ICollection<ScheduleEvent> ScheduleEvents { get; set; }
    }
}
