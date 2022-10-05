namespace FilmesAPI.Resources
{
    public class Messages
    {
        // Element not found.
        internal const string ELEMENT_NOT_FOUND = "The requested element doesn't exist.";
        internal const string MOVIE_NOT_FOUND = "The requested movie doesn't exist.";
        internal const string ADDRESS_NOT_FOUND = "The requested address doesn't exist.";
        internal const string MOVIETHEATER_NOT_FOUND = "The requested movie theater doesn't exist.";
        internal const string MANAGER_NOT_FOUND = "The requested manager doesn't exist.";
        
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

        // Address attributes.
        internal const string ADDRESS_CITY_REQUIRED = "The address city can't be empty.";
        internal const string ADDRESS_DISTRICT_REQUIRED = "The address district can't be empty.";
        internal const string ADDRESS_STREET_REQUIRED = "The address street can't be empty.";
        internal const string ADDRESS_NUMBER_REQUIRED = "The address number can't be empty.";
        internal const string ADDRESS_POSTALCODE_REQUIRED = "The address postal code can't be empty.";
        internal const string ADDRESS_CITY_TOO_LONG = "The address city is too long.";
        internal const string ADDRESS_DISTRICT_TOO_LONG = "The address district is too long.";
        internal const string ADDRESS_STREET_TOO_LONG = "The address street is too long.";
        internal const string ADDRESS_NUMBER_OUTOFRANGE = "The address number is out of range.";

        // MovieTheater attributes.
        internal const string MOVIETHEATER_NAME_REQUIRED = "The movie theater name can't be empty.";
        internal const string MOVIETHEATER_ADDRESSID_REQUIRED = "The movie theater address id can't be empty.";
        internal const string MOVIETHEATER_NAME_TOO_LONG = "The movie theater name is too long.";

        // Manager attributes.
        internal const string MANAGER_NAME_REQUIRED = "The manager name can't be empty.";
        internal const string MANAGER_NAME_TOO_LONG = "The manager name is too long.";
    }
}
