using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Mod03_ChelasMovies.DomainModel.Entities
{
    public class Comment
    {
        #region Properties

        [HiddenInput]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [Required]
        [StringLength(64)]
        [DataType(DataType.Text)]
        public String Description { get; set; }

        [Required]
        public int Rating { get; set; }

        [ScaffoldColumn(false)]
        public int MovieID { get; set; }
        public virtual Movie Movie { get; set; }

        #endregion Properties 
    }
}