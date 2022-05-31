using TMDbLib.Client;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.General;

namespace MvcMovie.Class {
    public class SearchTMDBMovies {
        private List<SearchMovie> searchMovies;

        public List<SearchMovie> GetSearchMoives() {
            return searchMovies;
        }

        TMDbClient client = new TMDbClient("56e06b99e18e712442bf8dc3f319d84d");
        public SearchTMDBMovies() {
            client.DefaultLanguage = "zh";
            client.DefaultCountry = "CN";
        }
        public void Search(string movieName) {
            SearchContainer<SearchMovie> results = client.SearchMovieAsync(movieName).Result;
            searchMovies = results.Results;
        }
    }
}