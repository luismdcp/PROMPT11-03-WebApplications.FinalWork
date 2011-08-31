using System;
using System.Collections.Generic;
using Mod03_ChelasMovies.DomainModel.Entities;

namespace Mod03_ChelasMovies.DomainModel.Services
{
    public interface IGroupsService : IDisposable, IService<Group, int>
    {
        ICollection<Group> GetGroupsCreatedByUser(string userName);
        ICollection<Group> GetEnrolledGroupsByUser(string userName);
        ICollection<User> GetAllUsersExceptLoggedUser(string userName);
        User GetUserInfo(string userName);
    }
}