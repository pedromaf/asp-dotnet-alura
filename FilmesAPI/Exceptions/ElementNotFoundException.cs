using FilmesAPI.Resources;

namespace FilmesAPI.Exceptions
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(ElementType type) : base(GetErrorMessage(type))
        {
            
        }

        public static string GetErrorMessage(ElementType type)
        {
            switch(type)
            {
                case ElementType.MOVIE:
                    return Messages.MOVIE_NOT_FOUND;
                default:
                    return Messages.ELEMENT_NOT_FOUND;
            }
        }

        public enum ElementType
        {
            MOVIE
        }
    }
}
