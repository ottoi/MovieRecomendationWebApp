using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRecomendationWebApp.MovieRecomendation.Models
{
    public class UserRating
    {   
        [Key]
        public int UserId { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        public Int32 Timestamp { get; set; }

        public UserRating(int[] atributes)
        {
            UserId = atributes[0];
            ItemId = atributes[1];
            Rating = atributes[2];
            Timestamp = atributes[3];
        }
    }
}