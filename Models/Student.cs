namespace tryno2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            Takes = new HashSet<Take>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(15)]
        public string Fname { get; set; }

        [Required]
        [StringLength(15)]
        public string Lname { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public int? Age { get; set; }

        [StringLength(30)]
        public string Address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Take> Takes { get; set; }
    }
}
