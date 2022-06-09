using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Movie
    {
        public int MovieId{ get; set; }
        public string MovieTitle{ get; set; }
        public double MovieVoteAverage{ get; set; }
        public int MovieVoteCount{ get; set; }

        public double MoviePopularity{ get; set; }
        public string MovieOverview{ get; set; }
        public System.DateTime? MovieReleaseDate{ get; set; }
        public string MovieBackdropPath{ get; set; }

        public List<Movie> movieList{ get; set; }

    }
}