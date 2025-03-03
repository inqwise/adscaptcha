namespace Inqwise.AdsCaptcha.Common
{
    public enum AdsCaptchaErrors
    {
        GeneralError = 1,
        MethodNotFound = 2,
        InvalidArgument = 3,
        NoResults = 4,
        CaptchaidNotSet,
        CaptchaidInvalid,
        PublickeyNotSet,
        ChallengeNotSet,
        PrivatekeyNotSet,
        ResponseNotSet,
        RemoteaddressNotSet,
        ResponseInvalid,
        Abort,
        MissingFileName,
        InvalidFileName,
        MaxFileSize,
        MinFileSize,
        InvalidFormat,
        InvalidDimension,
        ImageNotFound,
        NotLoggedIn,
        NotImplemented,
        CampaignidNotSet,
        AdvertiseridNotSet,
        WebsiteidNotSet,
        PublisheridNotSet,
        AdidNotSet,
        AdidInvalid,
        InvalidOperation
    }
}