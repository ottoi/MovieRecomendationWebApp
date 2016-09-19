using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieRecomendationWebApp.Models
{
    public class MovieRating
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public string Title { get; set; }
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; } 
        
    }
}