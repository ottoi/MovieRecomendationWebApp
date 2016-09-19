using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace MovieRecomendationWebApp.MovieRecomendation.Models
{
    public class MovieRepository
    {
        List<Movie> movies = new List<Movie>();

        public MovieRepository(string path)
        { 
            string line;
            // Read the file and display it line by line.
            using (StreamReader file = new StreamReader(path))
            {
                while ((line = file.ReadLine()) != null)
                {

                    char[] delimiters = new char[] { '|' };
                    string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    movies.Add(new Movie(Int32.Parse(parts[0]), parts[1], parts[4]));
                }

                file.Close();
            }

        }

        public Movie getMovie(int movieId)
        {
            return movies.Find(x => x.Id == movieId);
        }

        public List<Movie> GetRandomMovies(int nOfRandomMovies)
        {
            List<Movie> randomMovies = new List<Movie>();
            int nMovies = movies.Count;
            Random rnd = new Random();
            for(int i = 1; i <= nOfRandomMovies; i++)
            {
                int rndInt = rnd.Next(1, nMovies);
                randomMovies.Add(movies.Find(x => x.Id == rndInt));
            }
            return randomMovies;
        }
    
    }
}