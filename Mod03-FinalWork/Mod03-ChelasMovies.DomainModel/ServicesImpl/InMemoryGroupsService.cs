using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Mod03_ChelasMovies.DomainModel.Entities;
using Mod03_ChelasMovies.DomainModel.Services;

namespace Mod03_ChelasMovies.DomainModel.ServicesImpl
{
    public class InMemoryGroupsService : IGroupsService
    {
        #region Fields

        static List<Group> _groups;
        static List<User> _users;
        static int _newId;

        #endregion Fields

        #region Constructors

        static InMemoryGroupsService()
        {
            _groups = new List<Group>()
                          {
                              new Group() {Description = "Test Group 1", ID = 1, Name = "Group1"},
                              new Group() {Description = "Test Group 2", ID = 2, Name = "Group2"},
                              new Group() {Description = "Test Group 3", ID = 3, Name = "Group3"}
                          };

            _users = new List<User>()
                          {
                              new User() {UserId = Guid.NewGuid(), UserName = "User1"},
                              new User() {UserId = Guid.NewGuid(), UserName = "User2"},
                              new User() {UserId = Guid.NewGuid(), UserName = "User3"}
                          };

            _groups[0].Owner = _users[0];
            _groups[1].Owner = _users[1];
            _groups[2].Owner = _users[2];

            _groups[0].EnrolledUsers = new List<User>();
            _groups[1].EnrolledUsers = new List<User>();
            _groups[2].EnrolledUsers = new List<User>();
            _groups[0].EnrolledUsers.Add(_users[1]);
            _groups[1].EnrolledUsers.Add(_users[0]);

            _newId = _groups.Count;
        }

        #endregion Constructors

        #region Public Methods

        public ICollection<Group> GetAll()
        {
            return _groups;
        }

        public ICollection<Group> GetGroupsCreatedByUser(string userName)
        {
            return _groups.Where(g => g.Owner.UserName == userName).ToList();
        }

        public ICollection<Group> GetEnrolledGroupsByUser(string userName)
        {
            return _groups.Where(g => g.EnrolledUsers.Any(u => u.UserName == userName)).ToList();
        }

        public ICollection<User> GetAllUsers()
        {
            return _users;
        }

        public User GetUserInfo(string userName)
        {
            return _users.Where(u => u.UserName == userName).FirstOrDefault();
        }

        public Group Get(int id)
        {
            return _groups.Where(g => g.ID == id).FirstOrDefault();
        }

        public void Add(Group newGroup)
        {
            newGroup.ID = Interlocked.Increment(ref _newId);
            _groups.Add(newGroup);
        }

        public void Update(Group group)
        {
            
        }

        public void Delete(int id)
        {
            var groupToDelete = _groups.Where(g => g.ID == id).FirstOrDefault();
            _groups.Remove(groupToDelete);
        }

        public void Dispose()
        {
            _groups.Clear();
            _users.Clear();
        }

        public ICollection<User> GetAllUsersExceptLoggedUser(string userName)
        {
            return _users.Where(u => u.UserName != userName).ToList();
        }

        #endregion Public Methods
    }
}