using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Mod03_ChelasMovies.DomainModel
{
    public class Comment
    {
        [HiddenInput]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required]
        [StringLength(64)]
        [DataType(DataType.Text)]
        public String Description { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "The rating must be between 1 and 5")]
        public int Rating { get; set; }

        public int MovieID { get; set; }
        public virtual Movie Movie { get; set; }
    }
}