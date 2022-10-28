using System.Runtime.Serialization;

namespace UsuariosAPI.Resources
{
    public class Messages
    {
        internal const string USER_REGISTRATION_FAILED = "An error has occurred in attempt to register a new user.";
        internal const string USER_CONFIRMATION_PASSWORD_MATCH_ERROR = "The confirmation password doesn't match.";
        internal const string LOGIN_UNAUTHORIZED_ERROR = "Login attempt failed. Invalid credentials.";
        internal const string USER_LOGOUT_NOT_PERFORMED_ERROR = "Logout attempt failed.";
        internal const string EMAIL_CONFIRMATION_NEEDED_EXCEPTION = "Login attempt failed. Email confirmation needed.";
    }
}
