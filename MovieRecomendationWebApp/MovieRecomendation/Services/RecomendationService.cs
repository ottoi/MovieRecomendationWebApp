using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieRecomendationWebApp.MovieRecomendation.Models;
using System.Web.Hosting;

namespace MovieRecomendationWebApp.MovieRecomendation.Services
{
    public class RecomendationService : IRecomendationService
    {
        private UserRatingRepository userRatingRepo;
        private MovieRepository movieRepo;

        public RecomendationService(string userRatingDataPath, string moviesDataPath)
        {
           userRatingRepo = new UserRatingRepository(userRatingDataPath);
           movieRepo = new MovieRepository(moviesDataPath);
        }

        public List<Movie> GetRandomMovies()
        {
            return movieRepo.GetRandomMovies(20);
        }

        public List<Movie> RecomendedMovies(SortedList<int, int> givenRatings)
        {
            List<UserRating> topRatings = new List<UserRating>();
            int[] idsOfSimilarUsers = CosineSimilarity(givenRatings, userRatingRepo);
            for(int i = 0; i < idsOfSimilarUsers.Length; i++)
            {
                //IEnumerable<int> test = userRatingRepo.GetUsersTopMovies(idsOfSimilarUsers[i], 10).Select(x => x.ItemId);
                List<UserRating> usersTopMovies = userRatingRepo.GetUsersTopMovies(idsOfSimilarUsers[i], 10);
                topRatings.AddRange(usersTopMovies);
            }

            return GetMovies(topRatings.OrderByDescending(x => x.Rating).Take(10).ToList());

        }

        public static int[] CosineSimilarity(SortedList<int, int> givenRatings, UserRatingRepository ratings)
        {
            // Norm of the rated movies.
            IList<double> ratedValues = givenRatings.Values.Select<int, double>(i => i).ToList();
            double xNorm = Math.Sqrt((ratedValues.Aggregate((acc, val) => acc + Math.Pow(val, 2))));

            //SortedList<double, int> similarUsers = new SortedList<double, int>();
            List<SimilarUser> similarUsers = new List<SimilarUser>();
            foreach (var userRatings in ratings.GetAll())
            {

                double dotProduct = 0;
                double yNormSquare = 0;
                foreach (var rating in userRatings)
                {
                    int movieId = rating.ItemId;
                    int xx;
                    double y = rating.Rating;
                    yNormSquare += y * y;
                    if (givenRatings.TryGetValue(movieId, out xx))
                    {
                        double x = xx;     
                        dotProduct += (x * y);
                        
                    }

                }
                double similarity = dotProduct / (xNorm * Math.Sqrt(yNormSquare));
                if(!Double.IsNaN(similarity))
                {
                    similarUsers.Add(new SimilarUser(userRatings.Key, similarity));
                }
                
               // try
               // {
               //     similarUsers.Add(dotProduct / (Math.Sqrt(xNormSquare) * Math.Sqrt(yNormSquare)), userRatings.Key);
               // }
               // catch (ArgumentException) {  };

            }
            IOrderedEnumerable<SimilarUser> orderedSimilarUsers = similarUsers.OrderByDescending(x => x.Similarity);
            return new int[3] { orderedSimilarUsers.ElementAt(0).UserId, orderedSimilarUsers.ElementAt(1).UserId, orderedSimilarUsers.ElementAt(2).UserId };

        }

        private List<Movie> GetMovies(List<UserRating> userRatings)
        {
            IEnumerable<int> movieIds = userRatings.Select(x => x.ItemId);
            List<Movie> movies = new List<Movie>();
            foreach(var movieId in movieIds)
            {
                movies.Add(movieRepo.getMovie(movieId));
            }
            return movies;
        }

        private class SimilarUser
        {
            public int UserId;
            public double Similarity;

            public SimilarUser(int userId, double similarity)
            {
                UserId = userId;
                Similarity = similarity;
            }
        }
    }
}