using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace MovieRecomendationWebApp.MovieRecomendation.Models
{
    public class UserRatingRepository
    {
        private IEnumerable<IGrouping<int, UserRating>> userRatings;

        public UserRatingRepository(string path)
        {
            string line;
            List<UserRating> ratings = new List<UserRating>();
            // Read the file and display it line by line.
            using (StreamReader file = new StreamReader(path))
            {
                while ((line = file.ReadLine()) != null)
                {

                    char[] delimiters = new char[] { '\t' };
                    string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    int[] ratingAtributes = new int[parts.Length];

                    for (int i = 0; i < parts.Length; i++)
                    {
                        ratingAtributes[i] = Int32.Parse(parts[i]);
                    }
                    ratings.Add(new UserRating(ratingAtributes));
                }

                file.Close();
            }
         

            userRatings = ratings.GroupBy(rating => rating.UserId);
        }

        public IEnumerable<IGrouping<int, UserRating>> GetAll()
        {
            return userRatings;
        }

        public IGrouping<int, UserRating> GetUserRatings(int userId)
        {
            return userRatings.ElementAt(userId);
        }

        public List<UserRating> GetUsersTopMovies(int userId, int nOfTopMovies)
        {
            IOrderedEnumerable<UserRating> usersRatedMovies = userRatings.ElementAt(userId).OrderByDescending(rating => rating.Rating);
            return usersRatedMovies.ToList().Take(nOfTopMovies).ToList();
        }
    }
}