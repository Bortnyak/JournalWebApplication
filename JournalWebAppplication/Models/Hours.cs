namespace JournalWebAppplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Hours
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hours()
        {
            Ratings = new HashSet<Ratings>();
        }

        public int Id { get; set; }

        public int GroupId { get; set; }

        public int SubjectId { get; set; }

        public int? LK { get; set; }

        public int? PZ { get; set; }

        public int? LZ { get; set; }

        public virtual Subjects Subjects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ratings> Ratings { get; set; }
    }
}
