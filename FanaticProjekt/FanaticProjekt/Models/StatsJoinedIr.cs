namespace FanaticProjekt
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StatsJoinedIr
    {
        [Key]
        [Column(Order = 0)]
        public DateTime DATE { get; set; }

        [StringLength(100)]
        public string TYPE { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IR_TYPE { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int USER_NAME { get; set; }
    }
}
