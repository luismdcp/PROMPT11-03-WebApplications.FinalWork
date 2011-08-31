
namespace Mod03_ChelasMovies.WebApp.Models.ViewModels
{
    /// <summary>
    /// View Model for the Enrolled Users block
    /// </summary>
    public class UsersDetailsViewModel
    {
        public string AddUsersButton { get; set; } // To check if the user clicked the Add Button from the Enrolled Users block
        public string DeleteUsersButton { get; set; } // To check if the user clicked the Delete Button from the Enrolled Users block
        public string[] DeleteEnrolledUsers { get; set; } // Collection of User names that were selected from the checkbox list in the Enrolled Users block for deletion
    }
}