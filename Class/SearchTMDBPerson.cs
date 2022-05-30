using TMDbLib.Client;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.General;

namespace MvcMovie.Class {
    public class SearchTMDBPerson {
        private List<SearchPerson> searchPeople;

        public List<SearchPerson> GetSearchPeople() {
            return searchPeople;
        }

        TMDbClient client = new TMDbClient("56e06b99e18e712442bf8dc3f319d84d");
        public SearchTMDBPerson() {
            client.DefaultLanguage = "zh";
            client.DefaultCountry = "CN";
        }

        public void Search(string personName) {
            SearchContainer<SearchPerson> results = client.SearchPersonAsync(personName).Result;
            searchPeople = results.Results;
        }

    }
}