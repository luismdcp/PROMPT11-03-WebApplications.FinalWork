using System;
using System.ComponentModel.DataAnnotations;

namespace Mod03_ChelasMovies.DomainModel.Entities
{
    /// <summary>
    /// Represents the data from a row in the table aspnet_Users created by the Membership Provider
    /// </summary>
    public class User
    {
        #region Properties

        public Guid ApplicationId { get; set; }

        [ScaffoldColumn(false)]
        public Guid UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public string LoweredUserName { get; set; }
        public string MobileAlias { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime LastActivityDate { get; set; }

        #endregion Properties
    }
}