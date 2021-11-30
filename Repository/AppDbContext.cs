using FacebookApiTest.Repository.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace FacebookApiTest.Repository
{
    public class AppDbContext : IdentityDbContext<User, Role, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base (options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Poste> Postes { get; set; }

        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Like>()
                .HasKey(x => new { x.Id });
            modelBuilder.Entity<Like>()
                .HasOne(x => x.User)
                .WithMany(x => x.Likes);

            modelBuilder.Entity<Like>()
                .HasOne(x => x.Poste)
                .WithMany(x => x.Likes);

            modelBuilder.Entity<Like>()
                .HasOne(x => x.Comment)
                .WithMany(x => x.Likes);

            modelBuilder.Entity<Poste>()
            .Property<bool>("IsDeleted");
            modelBuilder.Entity<Poste>()
            .HasQueryFilter(poste => !EF.Property<bool>(poste, "IsDeleted") );

            modelBuilder.Entity<User>()
           .Property<bool>("IsDeleted");
            modelBuilder.Entity<User>()
            .HasQueryFilter(user => !EF.Property<bool>(user, "IsDeleted"));

            modelBuilder.Entity<Comment>()
           .HasQueryFilter(comment => !EF.Property<bool>(comment, "IsDeleted") );
        }

        
    }
    
}
