namespace UsuariosAPI.Models.Entities
{
    public class EmailConfirmationCode
    {
        public string Value { get; }
        public EmailConfirmationCode(string value)
        {
            Value = value;
        }
    }
}
