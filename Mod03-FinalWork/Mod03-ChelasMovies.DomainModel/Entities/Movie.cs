using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Mod03_ChelasMovies.DomainModel.Entities
{
    public class Movie
    {
        #region Constructors

        public Movie()
        {
            Year = DateTime.Now.Year;
        }

        #endregion Constructors

        #region Overrides
        // Overrides for the correct use of the LINQ operator Distinct()

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Movie m = obj as Movie;

            if (m == null)
            {
                return false;
            }

            return (ID == m.ID);
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        #endregion Overrides

        #region Properties

        [HiddenInput]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required]
        [MaxLength(128, ErrorMessage = "The Title must have less than 128 characters")]
        public string Title { get; set; }

        [Required]
        [Range(1900, 2100, ErrorMessage = "The Year must be between 1900 and 2100")]
        public int Year { get; set; }

        public string Genre { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Image { get; set; }
        public TimeSpan Runtime { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Group> EnrolledGroups { get; set; }

        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }

        [ScaffoldColumn(false)]
        public Guid OwnerId { get; set; }

        #endregion Properties
    }
}