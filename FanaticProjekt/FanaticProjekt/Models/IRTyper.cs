namespace FanaticProjekt
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IRTyper")]
    public partial class IRTyper
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IRTyper()
        {
            UserStatistics = new HashSet<UserStatistic>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string TYPE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserStatistic> UserStatistics { get; set; }
    }
}
