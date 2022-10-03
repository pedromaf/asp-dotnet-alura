namespace FilmesAPI.Resources
{
    public class Messages
    {
        // Movie attributes.
        internal const string MOVIE_NAME_REQUIRED = "The movie name can't be empty.";
        internal const string MOVIE_NAME_TOO_LONG = "The movie name is too long.";

        internal const string MOVIE_DIRECTOR_REQUIRED = "The director's name can't be empty.";
        internal const string MOVIE_DIRECTOR_TOO_LONG = "The director's name is too long.";
        
        internal const string MOVIE_GENRE_REQUIRED = "The genre can't be empty.";
        internal const string MOVIE_GENRE_TOO_LONG = "The genre is too long.";
        
        internal const string MOVIE_DESCRIPTION_REQUIRED = "The description can't be empty.";
        internal const string MOVIE_DESCRIPTION_TOO_SMALL = "The description is too small.";
        internal const string MOVIE_DESCRIPTION_TOO_LONG = "The description is too long.";

        internal const string MOVIE_RELEASE_DATE_REQUIRED = "The release date can't be empty.";

        // Element not found.
        internal const string ELEMENT_NOT_FOUND = "The requested element doesn't exist.";
        internal const string MOVIE_NOT_FOUND = "The requested movie doesn't exist.";

    }
}
