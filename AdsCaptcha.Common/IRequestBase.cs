using System;
using System.Runtime.Serialization;
using System.Threading;

namespace Inqwise.AdsCaptcha.Common
{
    public interface IRequestBase : IDisposable
    {
        Guid RequestGuid { get; }
        int CountOfFrames { get; }
        int CorrectIndex { get; }
        string ImagePath { get; }
        int Touch(int? selectedIndex, int? successRate, string clientIp);
        int CountOfTouches { get; }
        string SpriteUrl { get; set; }
        string SpriteFilePath { get; set; }
        string LikeUrl { get; }
        string ClickUrl { get; }
        string SpriteBase64LowQuality { get; set; }
        string VisitorUid { get; set; }
        string PhotoBy { get; set; }
        string PhotoByUrl { get; set; }
        int? DifficultyLevelId { get; }
    }
}