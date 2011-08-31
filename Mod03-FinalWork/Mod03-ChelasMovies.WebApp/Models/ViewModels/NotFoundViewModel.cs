
namespace Mod03_ChelasMovies.WebApp.Models.ViewModels
{
    public class NotFoundViewModel
    {
        public int Id { get; set; } // ID of the entity
        public string Entity { get; set; } // Name of the Entity not found (Group, Comment, Movie)

        public NotFoundViewModel(int id, string entity)
        {
            this.Id = id;
            this.Entity = entity;
        }
    }
}