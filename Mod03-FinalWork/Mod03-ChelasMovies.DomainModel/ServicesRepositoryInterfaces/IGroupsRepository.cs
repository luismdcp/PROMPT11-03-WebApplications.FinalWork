using System.Collections.Generic;
using Mod03_ChelasMovies.DomainModel.Entities;
using Mod03_ChelasMovies.Rep;

namespace Mod03_ChelasMovies.DomainModel.ServicesRepositoryInterfaces
{
    public interface IGroupsRepository : IRepository<Group, int>
    {
        ICollection<Group> GetGroupsCreatedByUser(string userName);
        ICollection<Group> GetEnrolledGroupsByUser(string userName);
        ICollection<User> GetAllUsersExceptLoggedUser(string userName);
        User GetUserInfo(string userName);
    }
}