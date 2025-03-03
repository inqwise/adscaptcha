using System;
using System.Runtime.Serialization;
using System.Threading;

namespace Inqwise.AdsCaptcha.Common.Data
{

    [DataContract]
    public class NewRequestArgs : IRequestBase
    {
        public NewRequestArgs()
        {
            RequestGuid = Guid.NewGuid();
            TimeStamp = DateTime.Now;
        }

        [DataMember]
        public DateTime TimeStamp { get; set; }

        [DataMember]
        public Guid RequestGuid { get; set; }
        
        public long? CaptchaId { get; set; }
        
        public string ClientIp { get; set;}
        
        public string ReferrerUrl { get; set; }
        
        public string SessionId { get; set; }
        [DataMember]
        public int CountOfFrames { get; set; }
        
        public string PublicKey { get; set; }
        public ICaptcha Captcha { get; private set; }
        
        public long RequestId { get; set; }

        public void SetCaptcha(ICaptcha captcha)
        {
            Captcha = captcha;
            CaptchaId = captcha.Id;
            PublicKey = captcha.PublicKey;
            if (null == Width)
            {
                Width = captcha.MaxWidth; 
            }

            if (null == Height)
            {
                Height = captcha.MaxHeight; 
            }
        }

        public IRequestBase Request { get; set; }
        
        public int CountryId { get; set; }
        [DataMember]
        public int CorrectIndex { get; set; }

        public string PreviousRequestGuid { get; set; }

        public EffectTypes EffectType { get; set; }

        public string FileName { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        public ImageType ImageType { get; set; }

        [DataMember]
        public string ClickUrl { get; set; }
        
        [DataMember]
        public string SpriteBase64LowQuality
        {
            get { return _spriteBase64LowQuality; }
            set
            {
                if (_spriteBase64LowQuality != value)
                {
                    _spriteBase64LowQuality = value;
                    Changed();
                }
            }
        }

        [DataMember]
        public string ImagePath { get; set; }

        [DataMember]
        private int _countOfTouches;
        int IRequestBase.Touch(int? selectedIndex, int? successRate, string clientIp)
        {
            int result;
            lock (this)
            {
                result = ++_countOfTouches;
            }

            return result;
        }

        int IRequestBase.CountOfTouches
        {
            get { return _countOfTouches; }
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
        string IRequestBase.SpriteFilePath
        {
            get; set; }

        [DataMember]
        public string LikeUrl { get; set; }

        [DataMember]
        public string VisitorUid { get; set; }

        [DataMember]
        public string PhotoBy { get; set; }

        [DataMember]
        public string PhotoByUrl { get; set; }

        public CaptchaSecurityLevel SecurityLevel { get; set; }

        [DataMember]
        public int? DifficultyLevelId { get; set; }

        [DataMember]
        public string CountryPrefix { get; set; }

        private IAd _ad;
        public void SetAd(IAd ad)
        {
            _ad = ad;
            if (null != ad)
            {
                LikeUrl = _ad.LikeUrl;
                ClickUrl = _ad.ClickUrl;
                ImagePath = _ad.ImagePath;
            }
        }

        public void Dispose()
        {
            Captcha = null;
            Request = null;
        }

        private Action<IRequestBase> changeCallback;
        private string _spriteBase64LowQuality;
        private string _spriteUrl;

        private void Changed()
        {
            if (null != changeCallback) changeCallback(this);
        }

        public void SetChangeCallback(Action<IRequestBase> callback)
        {
            changeCallback = callback;
        }
    }
}