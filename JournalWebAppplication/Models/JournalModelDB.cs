namespace JournalWebAppplication.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class JournalModelDB : DbContext
    {
        public JournalModelDB()
            : base("name=JournalModelDB")
        {
        }

        public virtual DbSet<Faculty> Faculty { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Hours> Hours { get; set; }
        public virtual DbSet<Pulpit> Pulpit { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<RatingTypes> RatingTypes { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Teachers> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Faculty>()
                .HasMany(e => e.Pulpit)
                .WithRequired(e => e.Faculty)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Groups>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.Groups)
                .HasForeignKey(e => e.GroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pulpit>()
                .HasOptional(e => e.Groups)
                .WithRequired(e => e.Pulpit);

            modelBuilder.Entity<RatingTypes>()
                .HasMany(e => e.Ratings)
                .WithOptional(e => e.RatingTypes)
                .HasForeignKey(e => e.RatingType_Id);

            modelBuilder.Entity<Students>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.Students)
                .HasForeignKey(e => e.Student_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subjects>()
                .HasMany(e => e.Hours)
                .WithRequired(e => e.Subjects)
                .HasForeignKey(e => e.SubjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teachers>()
                .HasMany(e => e.Subjects)
                .WithRequired(e => e.Teachers)
                .HasForeignKey(e => e.Teacher_Id)
                .WillCascadeOnDelete(false);
        }
    }
}
