namespace JournalWebAppplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pulpit")]
    public partial class Pulpit
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string PulpitName { get; set; }

        public int FacultyId { get; set; }

        public virtual Faculty Faculty { get; set; }

        public virtual Groups Groups { get; set; }
    }
}
