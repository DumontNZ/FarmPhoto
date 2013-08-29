using System;

namespace FarmPhoto.Website.Core.Resources
{
    public static class MessageResolver
    {
        public static string MessageCookieName = "farmphoto-messages";

        public static Message Resolve(string key)
        {
            return Resolve((MessageKey)Enum.Parse(typeof(MessageKey), key));
        }

        public static Message Resolve(MessageKey key)
        {

            switch (key)
            {
                case MessageKey.InvalidUsernameOrPassword:
                    return new Message
                    {
                        Type = MessageType.Warning,
                        Key = MessageKey.InvalidUsernameOrPassword,
                        Content = "Username or Password is Incorrect"
                    };
                case MessageKey.LoggedOut:
                    return new Message
                    {
                        Type = MessageType.Message,
                        Key = MessageKey.LoggedOut,
                        Content = "Logged out."
                    };
                case MessageKey.PasswordHasBeenReset:
                    return new Message
                    {
                        Type = MessageType.Message,
                        Key = MessageKey.PasswordHasBeenReset,
                        Content = "Your password has been reset. Please login using new password."
                    };
                case MessageKey.PasswordResetEmailSent:
                    return new Message
                    {
                        Type = MessageType.Message,
                        Key = MessageKey.PasswordResetEmailSent,
                        Content = "Password reminder sent."
                    };
                case MessageKey.InvalidToken:
                    return new Message
                    {
                        Type = MessageType.Error,
                        Key = MessageKey.InvalidToken,
                        Content = "Invalid Token or the Token has Expired. Request a new password."
                    };
                case MessageKey.AccountCreated:
                    return new Message
                    {
                        Type = MessageType.Message,
                        Key = MessageKey.AccountCreated,
                        Content = "Account was successfully created. Please Login."
                    };
                case MessageKey.YouFuckedUp:
                    return new Message
                        {
                            Type = MessageType.Message,
                            Key = MessageKey.YouFuckedUp,
                            Content = "HI YOU FUCKD UP "
                        };
                default:
                    return new Message
                        {
                            Type = MessageType.Message,
                            Key = MessageKey.YouFuckedUp,
                            Content = "This is the Deafult Case"
                        };
            }

        }
    }
}
