using TMDbLib.Client;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.General;

namespace MvcMovie.Class {
    public class TMDBMovie {
        public int MovieId{ get; set; }
        public string MovieTitle{ get; set; }
        public double MovieVoteAverage{ get; set; }
        public int MovieVoteCount{ get; set; }

        public double MoviePopularity{ get; set; }
        public string MovieOverview{ get; set; }
        public System.DateTime? MovieReleaseDate{ get; set; }
        public string MovieBackdropPath{ get; set; }
        public string MoviePosterPath{get; set; }
        // 电影风格
        public List<Genre> MovieGenres{ get; set; }


        // API 密钥
        TMDbClient client = new TMDbClient("56e06b99e18e712442bf8dc3f319d84d");

        public TMDBMovie() {
            client.DefaultLanguage = "zh";
            client.DefaultCountry = "CN";
        }
        // 通过MovieId搜索电影，并设置MovieTitle、MovieVoteAverage、MovieVoteCount等
        public void searchMovieById(int MovieId) {
            this.MovieId = MovieId;
            Movie movie = client.GetMovieAsync(MovieId).Result;
            this.MovieTitle = movie.Title;
            this.MovieVoteAverage = movie.VoteAverage;
            this.MovieVoteCount = movie.VoteCount;
            this.MoviePopularity = movie.Popularity;
            this.MovieOverview = movie.Overview;
            this.MovieGenres = movie.Genres;
            this.MovieReleaseDate = movie.ReleaseDate;
            this.MovieBackdropPath = movie.BackdropPath;
            this.MoviePosterPath = movie.PosterPath;
        }
    }
}