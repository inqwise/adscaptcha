using System;
using System.Configuration;
using System.Data;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Inqwise.AdsCaptcha.Common;
using Inqwise.AdsCaptcha.Dal;
using Inqwise.AdsCaptcha.Dal.Cache;
using Inqwise.AdsCaptcha.SystemFramework.GeoIp;

namespace Inqwise.AdsCaptcha.Entities
{
    public static class RequestEntityExtender
    {
        public static RequestEntity WithChangeCallback(this RequestEntity r, Action<IRequest> callback)
        {
            if (null != r)
            {
                r.SetChangeCallback(callback); 
            }
            return r;
        }
    }

    [DataContract]
    public class RequestEntity : IRequest
    {    
        public RequestEntity(IDataReader reader)
        {
            RequestId = reader.ReadInt64("Request_Id");
            try
            {
                SessionId = reader.ReadString("Session_Id");
                AdId = reader.OptInt("Ad_Id");
                var adTypeId = reader.OptInt("Ad_Type_ID");
                if (adTypeId.HasValue)
                {
                    AdType = (AdType) adTypeId;
                }
                CampaignId = reader.OptInt("Campaign_Id");
                AdvertiserId = reader.OptInt("Advertiser_Id");
                CaptchaId = reader.OptLong("Captcha_Id");
                RequestGuid = Guid.Parse(reader.ReadString("Request_Guid"));
                Timestamp = reader.ReadDateTime("Timestamp");
                ClickUrl = reader.SafeGetString("Link_Url");
                LikeUrl = reader.SafeGetString("FacebookLikeUrl") ??
                          ConfigurationManager.AppSettings["Slider.DefaultLikeUrl"];
                CountOfFrames = Convert.ToInt32(reader["FramesCount"]);
                CorrectIndex = Convert.ToInt32(reader["CorrectFrameIndex"]);
                PublicKey = reader.ReadString("PublicKey");
                PrivateKey = reader.ReadString("PrivateKey");
                ClientIp = reader.ReadString("IP_Address");
                ImageId = Convert.ToInt64(reader["ImageId"]);
                ImagePath = reader.GetValueOrDefault<string>("ImagePath");
                IsDemo = reader.ReadBool("IsDemo");
                EffectType = (EffectTypes) Convert.ToInt32(reader["EffectTypeId"]);
                SpriteUrl = reader.GetValueOrDefault<string>("SpriteUrl");
                ImageExternalId = reader.GetValueOrDefault<string>("ImageExternalId");
                ImageSource = reader.GetValueOrDefault<string>("ImageSource");
                DifficultyLevelId = reader.OptInt("DifficultyLevelId");
                CountryCode = LookupServiceFactory.GetLookupService().getCountry(ClientIp).getCode();
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to parse the Request #" + RequestId, ex);
            }
        }

        public RequestEntity()
        {
            
        }

        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public int? AdId { get; set; }
        [DataMember]
        public AdType? AdType { get; set; }
        [DataMember]
        public int? CampaignId { get; set; }
        [DataMember]
        public int? AdvertiserId { get; set; }
        [DataMember]
        public long? CaptchaId { get; set; }
        [DataMember]
        public long RequestId { get; set; }
        [DataMember]
        public Guid RequestGuid { get; set; }
        [DataMember]
        public DateTime Timestamp { get; set; }
        [DataMember]
        public string LikeUrl { get; set; }
        [DataMember]
        public string ClickUrl { get; set; }

        private object _spriteBase64LowQualityLocker = new object();
        public string SpriteBase64LowQuality
        {
            get
            {
                if (null == _spriteBase64LowQuality)
                {
                    lock (_spriteBase64LowQualityLocker)
                    {
                        if (null == _spriteBase64LowQuality)
                        {
                            _spriteBase64LowQuality = ImagesDataAccess.GetSpriteBase64(RequestId);
                        } 
                    }
                }
                return _spriteBase64LowQuality;
            }
            set 
            {
                if (_spriteBase64LowQuality != value)
                {
                    _spriteBase64LowQuality = value;
                }
            }
        }

        [DataMember]
        public string VisitorUid
        {
            get { return _visitorUid; }
            set
            {
                if (value != _visitorUid)
                {
                    _visitorUid = value;
                    Changed();
                }
            }
        }

        [DataMember]
        public string PhotoBy { get; set; }

        [DataMember]
        public string PhotoByUrl
        {
            get { return _photoByUrl; }
            set
            {
                if (_photoByUrl != value)
                {
                    _photoByUrl = value;
                    Changed();
                }
            }
        }

        [DataMember]
        public int? DifficultyLevelId { get; set; }

        [DataMember]
        public int CountOfFrames { get; set; }

        [DataMember]
        public int CorrectIndex { get; set; }

        [DataMember]
        public string PublicKey { get; set; }

        [DataMember]
        public string PrivateKey { get; set; }

        [DataMember]
        public string ClientIp { get; set; }

        [DataMember]
        public long ImageId { get; set; }

        [DataMember]
        public string ImagePath { get; set; }

        [DataMember]
        public int CountOfTouches
        {
            get { return _countOfTouches; }
            set
            {
                if (_countOfTouches != value)
                {
                    _countOfTouches = value;
                    Changed(); 
                }
            }
        }

        [DataMember]
        public string SpriteUrl
        {
            get { return _spriteUrl; }
            set
            {
                if (_spriteUrl != value)
                {
                    _spriteUrl = value;
                    Changed(); 
                }
            }
        }

        [DataMember]
        public string SpriteFilePath { get; set; }

        [DataMember]
        public bool IsDemo { get; set; }

        [DataMember]
        public EffectTypes EffectType { get; set; }

        public string ImageExternalId { get; private set; }
        public string ImageSource { get; private set; }

        private Task _task = null;
        private Action<IRequest> changeCallback;
        private string _spriteBase64LowQuality;

        private string _visitorUid;
        private int _countOfTouches;
        private string _spriteUrl;
        private string _photoByUrl;

        public int Touch(int? selectedIndex, int? successRate, string clientIp)
        {
            int result;
            lock (this)
            {
                result = ++CountOfTouches;
            }

            // async
            _task = Task.Run(() => CountOfTouches = RequestsDataAccess.Touch(RequestId, selectedIndex, successRate, clientIp, VisitorUid));

            

            return result;
        }

        public void Dispose()
        {
            if (null != _task)
            {
                if (_task.IsCompleted)
                {
                    _task.Dispose();
                }
                
                _task = null;
            }
        }

        private void Changed()
        {
            if (null != changeCallback) changeCallback(this);
        }

        public void SetChangeCallback(Action<IRequest> callback)
        {
            changeCallback = callback;
        }

        [DataMember]
        public string CountryCode
        {
            get;
            set;
        }
    }
}