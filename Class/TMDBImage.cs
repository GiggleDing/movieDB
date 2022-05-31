namespace MvcMovie.Class {
    public class TMDBImage {
        public static string GetImageURL(string fileSize, string filePath) {
            if(filePath == null)
                return null;
            return "https://image.tmdb.org/t/p/" + fileSize + filePath;
        }
    }
}