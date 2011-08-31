using System;
using System.Collections.Generic;
using System.Linq;
using Mod03_ChelasMovies.DomainModel.Entities;
using Mod03_ChelasMovies.DomainModel.Services;
using Mod03_ChelasMovies.DomainModel.ServicesRepositoryInterfaces;

namespace Mod03_ChelasMovies.DomainModel.ServicesImpl
{
    public class RepositoryGroupsService : IGroupsService
    {
        #region Fields

        private readonly IGroupsRepository _groupsRepository;

        #endregion Fields

        #region Constructors

        public RepositoryGroupsService(IGroupsRepository groupsRepository)
        {
            this._groupsRepository = groupsRepository;
        }

        #endregion Constructors

        #region Public Methods

        public ICollection<Group> GetAll()
        {
            return this._groupsRepository.GetAll().ToList();
        }

        public ICollection<Group> GetGroupsCreatedByUser(string userName)
        {
            return this._groupsRepository.GetGroupsCreatedByUser(userName);
        }

        public ICollection<Group> GetEnrolledGroupsByUser(string userName)
        {
            return this._groupsRepository.GetEnrolledGroupsByUser(userName);
        }

        public Group Get(int id)
        {
            return this._groupsRepository.Get(id);
        }

        public void Add(Group newGroup)
        {
            this._groupsRepository.Add(newGroup);
            this._groupsRepository.Save();
        }

        public void Update(Group group)
        {
            this._groupsRepository.Save();
        }

        public void Delete(int id)
        {
            try
            {
                this._groupsRepository.Delete(id);
                this._groupsRepository.Save();
            }
            catch (Exception e)
            {
                throw new ArgumentException(String.Format("Group with id {0} could not be found", id), "id", e);
            }
        }

        public void Dispose()
        {
            this._groupsRepository.Dispose();
        }

        public User GetUserInfo(string userName)
        {
            return this._groupsRepository.GetUserInfo(userName);
        }

        public ICollection<User> GetAllUsersExceptLoggedUser(string currentUser)
        {
            return this._groupsRepository.GetAllUsersExceptLoggedUser(currentUser);
        }

        #endregion Public Methods
    }
}