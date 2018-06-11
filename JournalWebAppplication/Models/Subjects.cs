namespace JournalWebAppplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Subjects
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subjects()
        {
            Hours = new HashSet<Hours>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string SubjectName { get; set; }

        public int Teacher_Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hours> Hours { get; set; }

        public virtual Teachers Teachers { get; set; }
    }
}
