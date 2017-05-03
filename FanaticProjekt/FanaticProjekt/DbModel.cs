namespace FanaticProjekt
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbModel : DbContext
    {
        public DbModel()
            : base("name=DbModel")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<IRTyper> IRTypers { get; set; }
        public virtual DbSet<UsersTable> UsersTables { get; set; }
        public virtual DbSet<UserStatistic> UserStatistics { get; set; }
        public virtual DbSet<StatsJoinedIr> StatsJoinedIrs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IRTyper>()
                .HasMany(e => e.UserStatistics)
                .WithRequired(e => e.IRTyper)
                .HasForeignKey(e => e.IR_TYPE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UsersTable>()
                .HasMany(e => e.UserStatistics)
                .WithRequired(e => e.UsersTable)
                .HasForeignKey(e => e.USER_NAME)
                .WillCascadeOnDelete(false);
        }
    }
}
