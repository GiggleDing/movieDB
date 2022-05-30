using TMDbLib.Client;
using TMDbLib.Objects.People;

namespace MvcMovie.Class{
    public class TMDBPerson {
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public string PersonBiography { get; set; }
        TMDbClient client = new TMDbClient("56e06b99e18e712442bf8dc3f319d84d");
        public TMDBPerson() {
            client.DefaultLanguage = "zh";
            client.DefaultCountry = "CN";
        }
        // 通过PersonId搜索演员，并设置PersonName、PersonBiography
        public void searchPersonById(int PersonId) {
            this.PersonId = PersonId;
            Person person = client.GetPersonAsync(PersonId).Result;
            this.PersonName = person.Name;
            this.PersonBiography = person.Biography;
        }
    }
}