namespace FanaticProjekt
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserStatistic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime DATE { get; set; }

        public int USER_NAME { get; set; }

        public int IR_TYPE { get; set; }

        public virtual IRTyper IRTyper { get; set; }

        public virtual UsersTable UsersTable { get; set; }
    }
}
