using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Mod03_ChelasMovies.DomainModel.Entities
{
    /// <summary>
    /// A Group that a User can create and share Movies with other Users enrolled int the Group
    /// </summary>
    public class Group
    {
        #region Properties

        [HiddenInput]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The Name must have less than 50 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(128, ErrorMessage = "The Description must have less than 50 characters")]
        public string Description { get; set; }

        public virtual ICollection<Movie> SharedMovies { get; set; } // The Movies to be shared with the Users enrolled in the Group
        public virtual ICollection<User> EnrolledUsers { get; set; } // The Users that were enrolled in the Group by the owner's Group

        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; } // The Group creator

        [ScaffoldColumn(false)]
        public Guid OwnerId { get; set; }

        #endregion Properties
    }
}