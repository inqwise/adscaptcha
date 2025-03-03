using System;
using Jayrock.Json;

namespace Inqwise.AdsCaptcha.Common.Data
{
    public class NewImageArgs
    {
        private const string WIDTH_PARAM = "width";
        private const string HEIGHT_PARAM = "height";
        private const string NAME_PARAM = "name";
        private const string SIZE_PARAM = "size";
        private const string CONTENT_TYPE = "contentType";
        private const string PATH_PARAM = "path";
        private const string EXTENSION_PARAM = "extension";
        private const string IMAGE_TYPE_ID_PARAM = "typeId";

        public NewImageArgs()
        {
        }

        public NewImageArgs(JsonObject args)
        {
            Name = args.GetString(NAME_PARAM);
            Size = args.GetInt(SIZE_PARAM);
            Width = args.GetInt(WIDTH_PARAM);
            Height = args.GetInt(HEIGHT_PARAM);
            ContentType = args.GetString(CONTENT_TYPE);
            Extension = args.GetString(EXTENSION_PARAM);
            RelativeFilePath = args.GetString(PATH_PARAM);
            ImageType = (ImageType) args.OptInt(IMAGE_TYPE_ID_PARAM, (int)ImageType.Commercial).Value;
        }

        
        public string Name { get; set; }

        public long Size { get; set; }

        public AdsCaptchaErrors? Error { get; set; }

        public bool HasError
        {
            get { return Error.HasValue; }
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public string AbsoluteFilePath { get; set; }

        public ImageType ImageType { get; set; }

        public string ContentType { get; set; }

        public string RelativeFilePath { get; set; }

        public string Extension { get; set; }

        public Tuple<int,int>[] AllowedDimansions { get; set; }

        public string[] AllowedContentTypes { get; set; }

        public int CountOfFrames { get; set; }

        public DateTime? TouchDate { get; private set; }

        public int? CampaignId { get; set; }

        public int? AdvertiserId { get; set; }

        public void Touch()
        {
            TouchDate = DateTime.Now;
        }

        public JsonObject ToJson()
        {
            var jo = new JsonObject();
            jo.Put(NAME_PARAM, Name);
            jo.Put(SIZE_PARAM, Size);
            jo.Put(WIDTH_PARAM, Width);
            jo.Put(HEIGHT_PARAM, Height);
            jo.Put(CONTENT_TYPE, ContentType);
            jo.Put(PATH_PARAM, RelativeFilePath);
            jo.Put(EXTENSION_PARAM, Extension);
            jo.Put(IMAGE_TYPE_ID_PARAM, (int)ImageType);
            return jo;
        }

        public long? AdId { get; set; }

        public long? ImageId { get; set; }

        public long? CaptchaId { get; set; }
    }
}