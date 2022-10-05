using FilmesAPI.Resources;
using FilmesAPI.Models.Enums;

namespace FilmesAPI.Exceptions
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(ElementType type) : base(GetErrorMessage(type))
        {
            
        }

        public static string GetErrorMessage(ElementType type)
        {
            return type switch
            {
                ElementType.MOVIE => Messages.MOVIE_NOT_FOUND,
                ElementType.ADDRESS => Messages.ADDRESS_NOT_FOUND,
                ElementType.MOVIETHEATER => Messages.MOVIETHEATER_NOT_FOUND,
                ElementType.MANAGER => Messages.MANAGER_NOT_FOUND,
                _ => Messages.ELEMENT_NOT_FOUND,
            };
        }
    }
}
