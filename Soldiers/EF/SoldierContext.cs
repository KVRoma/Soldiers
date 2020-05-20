using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soldiers.EF
{
    public class SoldierContext : DbContext
    {
        public SoldierContext() : base("ConStr") { }

        public DbSet<Soldier> Soldiers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<SoldierContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
