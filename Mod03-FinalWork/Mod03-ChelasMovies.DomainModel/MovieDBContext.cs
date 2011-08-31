using System.Data.Entity;
using Mod03_ChelasMovies.DomainModel.Entities;

namespace Mod03_ChelasMovies.DomainModel
{
    /// <summary>
    /// The EF <see cref="System.Data.Entity.DbContext"/> to <see cref="Movie"/> type.
    /// </summary>
    public class MovieDbContext : DbContext
    {
        #region Properties

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }

        #endregion Properties

        #region Constructors

        public MovieDbContext()
        {
            Database.SetInitializer<MovieDbContext>(null);
        }

        #endregion Constructors

        #region Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasRequired(c => c.Movie).WithMany(a => a.Comments);

            modelBuilder.Entity<Movie>().HasMany(m => m.Comments).WithRequired();
            modelBuilder.Entity<Movie>().HasMany(m => m.EnrolledGroups).WithMany(g => g.SharedMovies);
            modelBuilder.Entity<Movie>().HasRequired<User>(m => m.Owner).WithMany().HasForeignKey(m => m.OwnerId);

            modelBuilder.Entity<Group>().HasMany(g => g.EnrolledUsers).WithMany();
            modelBuilder.Entity<Group>().HasRequired<User>(g => g.Owner).WithMany().HasForeignKey(m => m.OwnerId);
            modelBuilder.Entity<User>().ToTable("aspnet_Users");
        }

        #endregion Overrides
    }
}