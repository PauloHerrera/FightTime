using System.Data.Entity;
using FightTime.Model;

namespace FightTime.Data.Config
{
    public class EntityContext : DbContext
    {
        public EntityContext() : base("Default")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EntityContext, FightTime.Data.Migrations.Configuration>("Default"));

            
            //Database.SetInitializer<EntityContext>(null);
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure domain classes using Fluent API here
           
            modelBuilder.Entity<Lutador>() 
                .HasMany(t => t.Habilidades)
                .WithMany(t => t.Lutadores).Map(m =>
                {
                    m.ToTable("LutadorHabilidades");
                    m.MapLeftKey("LutadorId");
                    m.MapRightKey("HabilidadeId");
                });

            modelBuilder.Entity<Usuario>()
                .HasOptional(t => t.Lutador);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Lutador> Lutadores { get; set; }
        public DbSet<Habilidade> Habilidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
