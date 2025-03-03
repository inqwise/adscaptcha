using System;
using Jayrock.Json;

namespace Inqwise.AdsCaptcha.Common.Data
{
    public class UpdateAdArgs
    {
        private int? _advertiserId;
        private NewImageArgs _newImage;

        private const string STATUS_ID_PARAM = "adStatusId";
        private const string AD_ID_PARAM = "adId";
        private const string IMAGE_PARAM = "image";
        private const string MAX_BID_PARAM = "maxBid";
        private const string AD_NAME_PARAM = "adName";
        private const string CLICK_URL_PARAM = "clickUrl";
        private const string LIKE_URL_PARAM = "likeUrl";
        public DateTime Timestamp { get; private set; }
        public string AdName { get; private set; }
        public string ClickUrl { get; private set; }
        public string LikeUrl { get; private set; }
        public decimal MaxBid { get; private set; }
        public NewImageArgs NewImage
        {
            get { return _newImage; }
            set
            {
                _newImage = value;
                if (null != _newImage)
                {
                    _newImage.AdvertiserId = AdvertiserId;
                    _newImage.AdId = AdId;
                }
            }
        }

        public string TmpImageId { get; private set; }
        public long AdId { get; private set; }
        public Status Status { get; private set; }

        public UpdateAdArgs()
        {
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

        public UpdateAdArgs(JsonObject args)
            :this()
        {
            AdId = args.GetLong(AD_ID_PARAM);
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
            Status = (Status) args.GetInt(STATUS_ID_PARAM);
        }

        public JsonObject ToJson()
        {
            var jo = new JsonObject();
            jo.Put(AD_ID_PARAM, AdId);
            jo.Put(AD_NAME_PARAM, AdName);
            jo.Put(CLICK_URL_PARAM, ClickUrl);
            jo.Put(LIKE_URL_PARAM, LikeUrl);
            jo.Put(MAX_BID_PARAM, MaxBid);
            if (null != NewImage)
            {
                jo.Put(IMAGE_PARAM, NewImage.ToJson()); 
            }
            jo.Put(STATUS_ID_PARAM, (int)Status);
            return jo;
        }
    }
}