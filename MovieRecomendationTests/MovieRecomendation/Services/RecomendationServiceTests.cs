using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieRecomendationWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecomendationWebApp.Services.Tests
{
    [TestClass()]
    public class RecomendationServiceTests
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

            int[] similarusers = RecomendationMachine.CosineSimilarity(userRatings, testRatings);


            CollectionAssert.AreEqual(expected, similarusers);
        }

        [TestMethod()]
        public void recomendedMoviesTest()
        {
            RecomendationMachine rm = new RecomendationMachine();
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

            Assert.AreEqual(10, recomededMovies.Count);
        }

        [TestMethod()]
        public void GetRandomMoviesTest()
        {
            RecomendationMachine rm = new RecomendationMachine();
            List<Movie> rndMovies = rm.GetRandomMovies();

            Assert.AreEqual(20, rndMovies.Count);
        }
    }
}