using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;
using Mod03_ChelasMovies.DomainModel;
using Mod03_ChelasMovies.DomainModel.Entities;

namespace Mod03_ChelasMovies.WebApp.Models
{
    public class MoviesInitializer : //DropCreateDatabaseAlways<MovieDbContext> 
        DropCreateDatabaseIfModelChanges<MovieDbContext>
    {
        protected override void Seed(MovieDbContext context)
        {
            Membership.CreateUser("User1", "password1");
            Membership.CreateUser("User2", "password2");
            Membership.CreateUser("User3", "password3");

            var user1 = context.Users.Where(u => u.UserName == "User1").FirstOrDefault();
            var user2 = context.Users.Where(u => u.UserName == "User2").FirstOrDefault();
            var user3 = context.Users.Where(u => u.UserName == "User3").FirstOrDefault();

            Group group1 = new Group { Name = "Group 1", Description = "Test Group1", Owner = user1, EnrolledUsers = new List<User>(), SharedMovies = new List<Movie>() };
            Group group3 = new Group { Name = "Group 3", Description = "Test Group3", Owner = user3, EnrolledUsers = new List<User>(), SharedMovies = new List<Movie>()};

            group1.EnrolledUsers.Add(user2);
            group3.EnrolledUsers.Add(user2);

            Movie movie1 = new Movie 
                                    { 
                                       Title = "Avatar", 
                                       Actors = "Sam Worthington, Zoe Saldana, Sigourney Weaver, Michelle Rodriguez",
                                       Director = "James Cameron",
                                       Genre = "Action, Adventure, Fantasy, Sci-Fi",
                                       Image = @"http://ia.media-imdb.com/images/M/MV5BMTYwOTEwNjAzMl5BMl5BanBnXkFtZTcwODc5MTUwMw@@._V1._SX320.jpg",
                                       Runtime = new TimeSpan(2, 42, 0),
                                       Owner = user1,
                                       Comments = new List<Comment>()
                                    };

            Movie movie2 = new Movie
                                    {
                                        Title = "Transformers",
                                        Actors = "Shia LaBeouf, Megan Fox, Josh Duhamel, Tyrese Gibson",
                                        Director = "Michael Bay",
                                        Genre = "Action, Sci-Fi, Thriller",
                                        Image = @"http://ia.media-imdb.com/images/M/MV5BMTQwNjU5MzUzNl5BMl5BanBnXkFtZTYwMzc1MTI3._V1._SX320.jpg",
                                        Runtime = new TimeSpan(2, 24, 0),
                                        Owner = user3,
                                        Comments = new List<Comment>()
                                    };

            Movie movie3 = new Movie
                                    {
                                        Title = "Battlestar Galactica",
                                        Actors = "Edward James Olmos, Mary McDonnell, Jamie Bamber, James Callis",
                                        Director = "N/A",
                                        Genre = "Action, Adventure, Drama, Sci-Fi",
                                        Image = @"http://ia.media-imdb.com/images/M/MV5BMTc1NTg1MDk3NF5BMl5BanBnXkFtZTYwNDYyMjI3._V1._SX320.jpg",
                                        Runtime = new TimeSpan(0, 50, 0),
                                        Owner = user2,
                                        Comments = new List<Comment>()
                                    };

            Movie movie4 = new Movie
                                    {
                                        Title = "RahXephon",
                                        Actors = "Jason Douglas, Hilary Haag, Ayako Kawasumi, Robert Anderson",
                                        Director = "Yutaka Izubuchi",
                                        Genre = "Animation, Sci-Fi, Action",
                                        Image = @"http://ia.media-imdb.com/images/M/MV5BMjA2MTQ2NTAwNF5BMl5BanBnXkFtZTcwMzQ2MDkyMQ@@._V1._SX320.jpg",
                                        Runtime = new TimeSpan(10, 50, 0),
                                        Owner = user2,
                                        Comments = new List<Comment>()
                                    };

            group1.SharedMovies.Add(movie1);
            group3.SharedMovies.Add(movie2);

            Comment comment1 = new Comment { Description = "Porreiro", Rating = (int) Rating.Incredible, Movie = movie1 };
            Comment comment2 = new Comment { Description = "Falta a mística da série original mas dá para ver", Rating = (int) Rating.Good, Movie = movie2 };
            Comment comment3 = new Comment { Description = "Podia ser melhor mas dá para ver", Rating = (int) Rating.Good, Movie = movie3 };
            Comment comment4 = new Comment { Description = "O melhor anime de sempre", Rating = (int) Rating.Incredible, Movie = movie4 };

            context.Movies.Add(movie1);
            context.Movies.Add(movie2);
            context.Movies.Add(movie3);
            context.Movies.Add(movie4);
        }
    }
}