using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Inqwise.AdsCaptcha.ImageProcessWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IImageProcessing
    {

        [OperationContract]
        int CreateImageDeformations(string imgUrl, int imageType, int width, int height);

        [OperationContract]
        int CreateImageDeformationsFromStream(byte[] imageStream, int imageType, int width, int height);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class DeformedImage
    {
        int imgID = -1;
        byte[] imageStream = null;

        [DataMember]
        public int ImgID
        {
            get { return imgID; }
            set { imgID = value; }
        }

        [DataMember]
        public byte[] ImageStream
        {
            get { return imageStream; }
            set { imageStream = value; }
        }
    }
}
