using System.Collections.Generic;
using Mod03_ChelasMovies.DomainModel.Entities;

namespace Mod03_ChelasMovies.WebApp.Models.ViewModels
{
    /// <summary>
    /// View Model for the Index View of the Index Action from the Groups Controller
    /// </summary>
    public class GroupsIndexViewModel
    {
        public ICollection<Group> OwnedGroups { get; set; } // Groups created by the logged User
        public ICollection<Group> EnrolledGroups { get; set; } // Groups where the logged User was enrolled

        public GroupsIndexViewModel(ICollection<Group> ownedGroups, ICollection<Group> enrolledGroups)
        {
            this.OwnedGroups = ownedGroups;
            this.EnrolledGroups = enrolledGroups;
        }
    }
}