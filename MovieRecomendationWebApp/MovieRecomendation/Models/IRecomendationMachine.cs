using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRecomendationWebApp.MovieRecomendation.Models
{
    interface IRecomendationService
    {
        List<Movie> GetRandomMovies();
        List<Movie> RecomendedMovies(SortedList<int, int> givenRatings);

    }
}