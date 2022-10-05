using FilmesAPI.Resources;
using FilmesAPI.Models.Enums;

namespace FilmesAPI.Exceptions
{
    public class ElementBeingUsedException : Exception
    {
        public ElementBeingUsedException(ElementType type) : base(GetErrorMessage(type))
        {
            
        }

        public static string GetErrorMessage(ElementType type)
        {
            return type switch
            {
                ElementType.ADDRESS => Messages.ADDRESS_BEING_USED,
                _ => Messages.ELEMENT_BEING_USED
            };
        }
    }
}
