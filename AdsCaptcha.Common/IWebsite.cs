using System;
using System.Collections.Generic;

namespace Inqwise.AdsCaptcha.Common
{
    public interface IWebsite
    {
        int Id { get; }
        int PublisherId { get; }
        string Url { get; }
        Status Status { get; }
        string PublicKey { get; }
        string PrivateKey { get; }
        DateTime ModifyDate { get; }
        IEnumerable<int> Categories { get; }
        int? CampaignId { get; }
        AdsCaptchaOperationResult<int> CreateCampaign(string campaignName);
    }
}