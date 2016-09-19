using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieRecomendationWebApp.MovieRecomendation.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string ImdbUrl { get; set; }

        public Movie(int movieId, string movieTitle, string imdbUrl)
        {
            Id = movieId;
            Title = movieTitle;
            ImdbUrl = imdbUrl;
        }
    }
}