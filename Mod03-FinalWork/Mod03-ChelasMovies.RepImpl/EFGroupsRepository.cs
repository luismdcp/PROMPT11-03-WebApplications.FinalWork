using System.Collections.Generic;
using System.Linq;
using Mod03_ChelasMovies.DomainModel;
using Mod03_ChelasMovies.DomainModel.Entities;
using Mod03_ChelasMovies.DomainModel.ServicesRepositoryInterfaces;
using Mod03_ChelasMovies.Rep;

namespace Mod03_ChelasMovies.RepImpl
{
    public class EFGroupsRepository : EFDbContextRepository<Group, int>, IGroupsRepository
    {
        public EFGroupsRepository(MovieDbContext moviesContext) : base(moviesContext) { }

        /// <summary>
        /// Gets all the Groups created by the logged User
        /// </summary>
        /// <param name="userName">The logged User name</param>
        /// <returns>Collection of Groups created by the logged User</returns>
        public ICollection<Group> GetGroupsCreatedByUser(string userName)
        {
            return this.GetAll().Where(g => g.Owner.UserName == userName).ToList();
        }

        /// <summary>
        /// Gets all the Groups that other Users have enrolled the logged User for Movie sharing
        /// </summary>
        /// <param name="userName">The logged User name</param>
        /// <returns>Collection of Groups enrolled by the logged User</returns>
        public ICollection<Group> GetEnrolledGroupsByUser(string userName)
        {
            return this.GetAll().Where(g => g.EnrolledUsers.Any(u => u.UserName == userName)).ToList();
        }

        /// <summary>
        /// Gets all the Users from the Membership Provider, except the logged User
        /// </summary>
        /// <param name="userName">The logged User name</param>
        /// <returns>Collection of Users from the Membership Provider</returns>
        public ICollection<User> GetAllUsersExceptLoggedUser(string userName)
        {
            return this.DbContext.Set<User>().Where(u => u.UserName != userName).ToList();
        }

        /// <summary>
        /// Gets the data for the logged User from the Membership Provider
        /// </summary>
        /// <param name="userName">The logged User name</param>
        /// <returns>User with the data related to the logged User</returns>
        public User GetUserInfo(string userName)
        {
            return this.DbContext.Set<User>().Where(u => u.UserName == userName).FirstOrDefault();
        }
    }
}