using System;
using Jayrock.Json;

namespace Inqwise.AdsCaptcha.Common.Data
{
    public class NewAdArgs
    {
        private int? _campaignId;
        private int? _advertiserId;
        private NewImageArgs _newImage;
        private long? _adId;
        private const string IMAGE_PARAM = "image";
        private const string MAX_BID_PARAM = "maxBid";
        private const string AD_NAME_PARAM = "adName";
        private const string CLICK_URL_PARAM = "clickUrl";
        private const string LIKE_URL_PARAM = "likeUrl";
        public Guid Guid { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string AdName { get; private set; }
        public string ClickUrl { get; private set; }
        public string LikeUrl { get; private set; }
        public decimal MaxBid { get; private set; }
        public int? WebsiteId { get; private set; }

        public NewImageArgs NewImage
        {
            get { return _newImage; }
            set 
            {
                _newImage = value;
                if (null != _newImage)
                {
                    _newImage.CampaignId = CampaignId;
                    _newImage.AdvertiserId = AdvertiserId;
                    _newImage.AdId = AdId;
                }
            }
        }

        public string TmpImageId { get; private set; }
        public int? CampaignId
        {
            get { return _campaignId; }
            set 
            { 
                _campaignId = value;
                if (null != NewImage)
                {
                    NewImage.CampaignId = value;
                }
            }
        }

        public NewAdArgs()
        {
            Guid = Guid.NewGuid();
            Timestamp = DateTime.Now;
        }

        public int? AdvertiserId
        {
            get { return _advertiserId; }
            set
            {
                _advertiserId = value;
                if (null != NewImage)
                {
                    NewImage.AdvertiserId = value;
                }
            }
        }

        public long? AdId
        {
            get { return _adId; }
            set 
            {
                _adId = value;
                if (null != NewImage)
                {
                    NewImage.AdId = value;
                }
            }
        }

        public int? PublisherId { get; set; }

        public NewAdArgs(JsonObject args)
            :this()
        {
            AdName = args.GetString(AD_NAME_PARAM);
            ClickUrl = args.GetString(CLICK_URL_PARAM);
            LikeUrl = args.GetString(LIKE_URL_PARAM);
            MaxBid = args.GetDecimal(MAX_BID_PARAM);
            object objImage;
            if (null != (objImage = args.Opt(IMAGE_PARAM, null)))
            {
                NewImage = new NewImageArgs(objImage as JsonObject);
            }

            TmpImageId = args.OptString("imageId");
            CampaignId = args.OptInt("campaignId");
            WebsiteId = args.OptInt("websiteId");
        }

        public JsonObject ToJson()
        {
            var jo = new JsonObject();
            jo.Put(AD_NAME_PARAM, AdName);
            jo.Put(CLICK_URL_PARAM, ClickUrl);
            jo.Put(LIKE_URL_PARAM, LikeUrl);
            jo.Put(MAX_BID_PARAM, MaxBid);
            if (null != NewImage)
            {
                jo.Put(IMAGE_PARAM, NewImage.ToJson()); 
            }
            return jo;
        }
    }
}