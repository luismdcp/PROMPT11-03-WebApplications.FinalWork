using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Mod03_ChelasMovies.DomainModel
{
    public class Movie
    {
        public Movie()
        {
            Year = DateTime.Now.Year;
        }

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
    }
}