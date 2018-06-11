namespace JournalWebAppplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ratings
    {
        public int Id { get; set; }

        public int Student_Id { get; set; }

        [StringLength(50)]
        public string Rating { get; set; }

        public int? RatingType_Id { get; set; }

        [StringLength(500)]
        public string TopicOfLesson { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public int? HoursId { get; set; }

        public virtual Hours Hours { get; set; }

        public virtual RatingTypes RatingTypes { get; set; }

        public virtual Students Students { get; set; }
    }
}
