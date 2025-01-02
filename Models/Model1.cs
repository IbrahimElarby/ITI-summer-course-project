namespace tryno2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Take> Takes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(e => e.Takes)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.Course_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Courses)
                .WithOptional(e => e.Department)
                .HasForeignKey(e => e.Dept_id);

            modelBuilder.Entity<Student>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Takes)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.St_id)
                .WillCascadeOnDelete(false);
        }
    }
}
