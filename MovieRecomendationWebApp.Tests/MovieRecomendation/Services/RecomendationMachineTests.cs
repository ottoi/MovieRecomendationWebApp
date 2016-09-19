using MovieRecomendationWebApp.MovieRecomendation.Services;
using MovieRecomendationWebApp.MovieRecomendation.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieRecomendationWebApp.Models;

namespace MovieRecomendationWebApp.MovieRecomendation.Tests
{
    [TestClass()]
    public class RecomendationMachineTests
    {
        [TestMethod()]
        public void CosineSimilarityTest()
        {
            UserRatingRepository testRatings = new UserRatingRepository(@"u1.test");
            SortedList<int, int> userRatings = new SortedList<int, int>();
            userRatings.Add(6, 1);
            userRatings.Add(10, 1);
            userRatings.Add(14, 1);
            userRatings.Add(281, 1);
            userRatings.Add(356, 1);
            userRatings.Add(280, 1);
            userRatings.Add(40, 1);
            int[] expected = { 1, 2, 5 };

            int[] similarusers = RecomendationService.CosineSimilarity(userRatings, testRatings);


            CollectionAssert.AreEqual(expected, similarusers);
        }

        [TestMethod()]
        public void recomendedMoviesTest()
        {
            RecomendationService rm = new RecomendationService(@"u.data",@"u.item");
            //UserRatingRepository testRatings = new UserRatingRepository(@"u1.test");
            SortedList<int, int> userRatings = new SortedList<int, int>();
            userRatings.Add(6, 1);
            userRatings.Add(10, 1);
            userRatings.Add(14, 1);
            userRatings.Add(281, 1);
            userRatings.Add(356, 1);
            userRatings.Add(280, 1);
            userRatings.Add(40, 1);

            List<Movie> recomededMovies = rm.RecomendedMovies(userRatings);
            foreach (var movie in recomededMovies)
            {
                Assert.IsNotNull(movie);
            }

            Assert.AreEqual(10, recomededMovies.Count);
        }

        [TestMethod()]
        public void GetRandomMoviesTest()
        {
            RecomendationService rm = new RecomendationService(@"u.data", @"u.item");
            List<Movie> rndMovies = rm.GetRandomMovies();

            foreach (var movie in rndMovies)
            {
                Assert.IsNotNull(movie);
            }
            Assert.AreEqual(20, rndMovies.Count);
        }

    }
}