using System.Data.Entity.Migrations.Model;

namespace FanaticProjekt
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UsersTable")]
    public partial class UsersTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UsersTable()
        {
            UserStatistics = new HashSet<UserStatistic>();
        }

        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(60)]
        public string user_login { get; set; }

        [Required]
        [StringLength(255)]
        public string user_pass { get; set; }

        [Required]
        [StringLength(50)]
        public string user_nicename { get; set; }

        [Required]
        [StringLength(100)]
        public string user_email { get; set; }

        [Required]
        [StringLength(100)]
        public string user_url { get; set; }

        public DateTime user_registered { get; set; }

        [Required]
        [StringLength(255)]
        public string user_activation_key { get; set; }

        public int user_status { get; set; }

        [Required]
        [StringLength(250)]
        public string display_name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserStatistic> UserStatistics { get; set; }
    }
}
