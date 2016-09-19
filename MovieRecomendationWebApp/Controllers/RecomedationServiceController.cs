using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MovieRecomendationWebApp.Models;
using MovieRecomendationWebApp.MovieRecomendation.Services;
using MovieRecomendationWebApp.MovieRecomendation.Models;
using System.Web.Http;
using System.Web.Hosting;

namespace MovieRecomendationWebApp.Controllers
{
    public class RecomedationServiceController : ApiController
    {
        static readonly IRecomendationService repository = 
            new RecomendationService(HostingEnvironment.MapPath(@"~/App_Data/u.data"), HostingEnvironment.MapPath(@"~/App_Data/u.item"));
        public IEnumerable<MovieRating> ratings;

        
        public IEnumerable<MovieRating> GetRandomMovies()
        {
            var rndMovies = repository.GetRandomMovies();
            ratings = MoviesToMovieRatings(rndMovies);
            return ratings;
                

        }

        [HttpGet]
        public IEnumerable<Movie> RecomendedMovies()
        {
            SortedList<int, int> givenRatings = new SortedList<int, int>();
            foreach(var rating in ratings)
            {

                givenRatings.Add(rating.Id, rating.Rating);
        
            }
   
            return repository.RecomendedMovies(givenRatings);
        }

        private static List<MovieRating> MoviesToMovieRatings(List<Movie> movies)
        {
            List<MovieRating> ratings = new List<MovieRating>();
            foreach(var movie in movies)
            {
                MovieRating rating = new MovieRating();
                rating.Id = movie.Id;
                rating.Title = movie.Title;
                ratings.Add(rating);
            }
            return ratings;
        }

    }
}
