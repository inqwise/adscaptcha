namespace Inqwise.AdsCaptcha.Common.Mails
{
    public interface IMailArgs
    {
        string From { get; }
        string Recipients { get; }
        string Subject { get; }
        string Body { get; } 
    }
}