using System.Runtime.Serialization;

namespace UsuariosAPI.Resources
{
    public class Messages
    {
        internal const string USER_REGISTRATION_FAILED = "An error has occurred in attempt to register a new user.";
        internal const string USER_CONFIRMATION_PASSWORD_MATCH_ERROR = "The confirmation password doesn't match.";
        internal const string USER_NOT_FOUND = "The specified user doesn't exist.";
        internal const string EMAIL_NOT_REGISTERED = "The specified email isn't registered.";
        internal const string LOGIN_UNAUTHORIZED_ERROR = "Login attempt failed. Invalid credentials.";
        internal const string USER_LOGOUT_NOT_PERFORMED_ERROR = "Logout attempt failed.";
        internal const string EMAIL_CONFIRMATION_NEEDED_EXCEPTION = "Login attempt failed. Email confirmation needed.";
        internal const string INVALID_EMAIL_CONFIRMATION_CODE = "Invalid email confirmation code.";
        internal const string EMAIL_SERVICE_INTERNAL_ERROR = "An error has occurred in attempt to send an account confirmation email.";
        internal const string RESET_PASSWORD_FAILED = "Reset password attempt failed.";
    }
}
