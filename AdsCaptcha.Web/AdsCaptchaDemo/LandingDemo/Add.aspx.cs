using System;

public partial class LandingDemo_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.Identity.IsAuthenticated)
            Response.Redirect("~/Account/Login?returnUrl=" + Request.Url.LocalPath);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            /*
            using (AdsCaptcha_ImagesEntities ent = new AdsCaptcha_ImagesEntities())
            {
                string urlname = Server.UrlEncode(txtName.Text.ToLower().Replace(" ", "_"));
                var urlnames = ent.T_DEMOS.Where(d => d.DemoUrlName == urlname).ToList();
                if (urlnames.Count == 0)
                {
                    int imageID = -1;
                    switch (ddlDeformations.SelectedValue)
                    {
                        case "1": SaveToDB(ent, ref imageID); break;
                        case "2": SaveToDBPolar(ent, ref imageID); break;
                        case "3": SaveToDBTile(ent, ref imageID); break;
                        case "4": SaveToDBFrostedGlass(ent, ref imageID); break;
                        case "5": SaveToDBRadialBlur(ent, ref imageID); break;
                        case "6": SaveToDBMotionBlur(ent, ref imageID); break;
                    }

                    if (imageID != -1)
                    {
                        try
                        {
                            T_DEMOS demo = new T_DEMOS();
                            demo.DemoName = txtName.Text;
                            demo.DemoDescription = txtDesc.InnerText;
                            demo.DemoUrlName = urlname;
                            demo.CreatedBy = User.Identity.Name;
                            demo.InsertDate = DateTime.Now;

                            if (txtClick.Text.Trim() != string.Empty)
                            {
                                string clickUrl = txtClick.Text.Trim();
                                if ((clickUrl.ToLower().IndexOf("http://") == -1) && (clickUrl.ToLower().IndexOf("https://") == -1))
                                    clickUrl = "http://" + clickUrl;
                                demo.ClickUrl = clickUrl;
                            }
                            if (txtLike.Text.Trim() != string.Empty)
                            {
                                string likeUrl = txtLike.Text.Trim();
                                if ((likeUrl.ToLower().IndexOf("http://") == -1) && (likeUrl.ToLower().IndexOf("https://") == -1))
                                    likeUrl = "http://" + likeUrl;
                                demo.LikeUrl = likeUrl;
                            }

                            T_DEMOS_IMAGES dimage = new T_DEMOS_IMAGES();
                            dimage.ImageID = imageID;
                            dimage.InsertDate = DateTime.Now;

                            demo.T_DEMOS_IMAGES.Add(dimage);

                            ent.AddToT_DEMOS(demo);

                            ent.SaveChanges();

                            Response.Redirect("~/lp/" + urlname);
                        }
                        catch (Exception exc)
                        {
                            lblError.Visible = true;
                            lblError.Text = exc.ToString();
                        }

                    }
                    else
                    {
                        lblError.Visible = true;
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Demo name already exists";
                }
            }
             */
        }
    }

    /*
    private void SaveToDB(AdsCaptcha_ImagesEntities ent, ref int imageID)
    {
        if ((uploadImage.PostedFile != null) && (uploadImage.PostedFile.ContentLength > 0))
        {
            int width = 300;
            int height = 250;
  
            string fn = Guid.NewGuid().ToString() + ".jpg";// System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            string SaveLocation = ConfigurationManager.AppSettings["RandomImagesFolder300"] + "\\" + fn;

            switch (ddlDimentions.SelectedValue)
            {
                case "2":
                    width = 180;
                    height = 150;
                    SaveLocation = ConfigurationManager.AppSettings["RandomImagesFolder180"] + "\\" + fn;
                    break;
                default: 
                    
                    break;
            }

            try
            {
                ImageUtilities utilities = new ImageUtilities();

                uploadImage.PostedFile.SaveAs(SaveLocation);

                string imageDirName = Guid.NewGuid().ToString();

                System.Drawing.Image coreimage = Bitmap.FromFile(SaveLocation);


                var resizedCoreBmp = utilities.ResizeImage(coreimage, width, height);
                FileInfo fi = new FileInfo(SaveLocation);

                var mainImage = ent.P_Images_Insert(101, utilities.SaveJpegToStream(resizedCoreBmp, 90).ToArray(),
                                                    fi.Extension.Replace(".", ""), width, height, null, null, null).FirstOrDefault();


                imageID = mainImage.ImageID;

                TwistEffect effect = new TwistEffect();
                Random rand = new Random(999);

                AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS defMain = new T_IMAGES_DEFORMATIONS();
                defMain.DeformationID = 30;
                //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                defMain.ImageID = mainImage.ImageID;
                defMain.DeformationType = 1;
                //defMain.ImageStream = mainImage.ImageStream;

                int next = rand.Next(999) * DateTime.Now.Millisecond % 2;
                double offFirst1 = 0.01 * ((next == 0) ? 1 : -1);

                next = rand.Next(999) * DateTime.Now.Millisecond % 2;
                double offSecond1 = 0.01 * ((next == 0) ? 1 : -1);

                next = rand.Next(999) * DateTime.Now.Millisecond % 2;
                double amount1 = 1.0 * ((next == 0) ? 1 : -1);
                double size1 = 0.01;// -sizeStep * (double)(i + 1);

                var tempBmp1 = new Bitmap(resizedCoreBmp);
                var destBMP1 = effect.ConvertImage(tempBmp1, amount1, size1, 2, offFirst1, offSecond1);

                defMain.ImageStream = utilities.SaveJpegToStream(destBMP1, 90).ToArray();

                ent.AddToT_IMAGES_DEFORMATIONS(defMain);

                //ent.P_Images_Deformations_Insert(30, 1, mainImage.ImageID, mainImage.ImageStream);


                double xStep = 0.02;

                
                double yStep = (double)(rand.Next(999) * DateTime.Now.Millisecond % 13) / (double)20 / (double)30;

                double amountStep = 1.1;// ((double)(rand.Next(999) * DateTime.Now.Millisecond % 25)) / (double)30 ;
                //double sizeStep = 0.035;// ((double)(rand.Next(999) * DateTime.Now.Millisecond % 5) / (double)10) / (double)30;
               

                for (int i = 0; i < 30; i++)
                {

                    var tempBmp = new Bitmap(resizedCoreBmp);

                    double offFirst = xStep * (double)(i + 1);
                    double offSecond = yStep * (double)(i + 1);
                    double amount = amountStep * (double)(i + 1) + 5.0;
                    double size = 0.50;// -sizeStep * (double)(i + 1);

                    var destBMP = effect.ConvertImage(tempBmp, amount, size, 2, offFirst, offSecond);
                    //var destBMP2 = effect.ConvertImage(tempBmp, amount, size, 2, (-1.0) * offFirst, (-1.0) * offSecond);

                    int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
                    jpgQuality = 90;
                    //jpgQuality = 90 - Convert.ToInt32((double)i * 0.4);
                    //utilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, jpgQuality);
                    //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, 90 - Convert.ToInt32((double)i * 1.1));

                    AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
                    def.DeformationID = 30 + i + 1;
                    //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                    //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                    def.ImageID = mainImage.ImageID;
                    def.DeformationType = 1;
                    def.ImageStream = utilities.SaveJpegToStream(destBMP, 90).ToArray();
                    ent.AddToT_IMAGES_DEFORMATIONS(def);

                    tempBmp = null;
                    destBMP = null;
                    //destBMP2 = null;
                }

                for (int i = 0; i < 30; i++)
                {

                    var tempBmp = new Bitmap(resizedCoreBmp);

                    double offFirst = xStep * (double)(i + 1);
                    double offSecond = yStep * (double)(i + 1);
                    double amount = amountStep * (double)(i + 1) + 5.0;
                    double size = 0.50;// 3.0 - sizeStep * (double)(i + 1);

                    //var destBMP = effect.ConvertImage(tempBmp, amount, size, 2, offFirst, offSecond);
                    var destBMP2 = effect.ConvertImage(tempBmp, (-1.0) * amount, size, 2, (-1.0) * offFirst, (-1.0) * offSecond);

                    //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, 90 - Convert.ToInt32((double)i * 1.1));
                    int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
                    jpgQuality = 90;
                    // utilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, jpgQuality);

                    AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
                    def.DeformationID = 30 - i - 1;
                    //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                    //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                    def.ImageID = mainImage.ImageID;
                    def.DeformationType = 1;
                    def.ImageStream = utilities.SaveJpegToStream(destBMP2, 90).ToArray();
                    ent.AddToT_IMAGES_DEFORMATIONS(def);

                    tempBmp = null;
                    //destBMP = null;
                    destBMP2 = null;
                }

                ent.SaveChanges();

                CacheImagesManager.AddToCache(mainImage.ImageID);
            }
            catch (Exception ex)
            {
                throw (ex);
                //imageID = -1;
            }
        }
        else
        {
            imageID = -1;
        }
         
    }

    private void SaveToDBTile(AdsCaptcha_ImagesEntities ent, ref int imageID)
    {
        
        if ((uploadImage.PostedFile != null) && (uploadImage.PostedFile.ContentLength > 0))
        {
            int width = 300;
            int height = 250;

            string fn = Guid.NewGuid().ToString() + ".jpg";// System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            string SaveLocation = ConfigurationManager.AppSettings["RandomImagesFolder300"] + "\\" + fn;

            switch (ddlDimentions.SelectedValue)
            {
                case "2":
                    width = 180;
                    height = 150;
                    SaveLocation = ConfigurationManager.AppSettings["RandomImagesFolder180"] + "\\" + fn;
                    break;
                default:

                    break;
            }
            try
            {
                ImageUtilities utilities = new ImageUtilities();

                uploadImage.PostedFile.SaveAs(SaveLocation);

                string imageDirName = Guid.NewGuid().ToString();

                //string imagesDir = ConfigurationManager.AppSettings["RandomImagesFolder300"] + imageDirName + "\\";
                //Directory.CreateDirectory(imagesDir);


                System.Drawing.Image coreimage = Bitmap.FromFile(SaveLocation);


                var resizedCoreBmp = utilities.ResizeImage(coreimage, width, height);
                FileInfo fi = new FileInfo(SaveLocation);

                var mainImage = ent.P_Images_Insert(2, utilities.SaveJpegToStream(resizedCoreBmp, 90).ToArray(),
                                                    fi.Extension.Replace(".", ""), width, height, null, null, null).FirstOrDefault();


                imageID = mainImage.ImageID;

                AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS defMain = new T_IMAGES_DEFORMATIONS();
                defMain.DeformationID = 30;
                //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                defMain.ImageID = mainImage.ImageID;
                defMain.DeformationType = 3;
                defMain.ImageStream = mainImage.ImageStream;
                ent.AddToT_IMAGES_DEFORMATIONS(defMain);

                //ent.P_Images_Deformations_Insert(30, 1, mainImage.ImageID, mainImage.ImageStream);


                //double xStep = 0.02;

                Random rand = new Random(999);
                double yStep = (double)(rand.Next(999) * DateTime.Now.Millisecond % 50) + 20.0;

                double rotation = (double)(rand.Next(999) * DateTime.Now.Millisecond % 50) + 20.0;// ((double)(rand.Next(999) * DateTime.Now.Millisecond % 25)) / (double)30 ;
                double squareSize = 190.0;// ((double)(rand.Next(999) * DateTime.Now.Millisecond % 5) / (double)10) / (double)30;
                double curvature = 0.0;
                TileEffect effect = new TileEffect();

                for (int i = 0; i < 30; i++)
                {

                    var tempBmp = new Bitmap(resizedCoreBmp);

                    double rotationIn = rotation + (double)(i + 1);
                    double squareSizeIn = squareSize - (double)(i * 8);
                    if (squareSizeIn < 15.0) squareSizeIn = 15.0;
                    double curvatureIn = curvature + (double)(i + 1) * 1.1;

                    var destBMP = effect.ConvertImage(tempBmp, rotationIn, squareSizeIn, 2, curvatureIn);
                    //var destBMP2 = effect.ConvertImage(tempBmp, amount, size, 2, (-1.0) * offFirst, (-1.0) * offSecond);

                    int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
                    jpgQuality = 90;
                    //jpgQuality = 90 - Convert.ToInt32((double)i * 0.4);
                    //utilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, jpgQuality);
                    //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, 90 - Convert.ToInt32((double)i * 1.1));

                    AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
                    def.DeformationID = 30 + i + 1;
                    //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                    //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                    def.ImageID = mainImage.ImageID;
                    def.DeformationType = 3;
                    def.ImageStream = utilities.SaveJpegToStream(destBMP, 90).ToArray();
                    ent.AddToT_IMAGES_DEFORMATIONS(def);

                    tempBmp = null;
                    destBMP = null;
                    //destBMP2 = null;
                }

                for (int i = 0; i < 30; i++)
                {

                    var tempBmp = new Bitmap(resizedCoreBmp);

                    double rotationIn = rotation - (double)(i + 1);
                    double squareSizeIn = squareSize - (double)(i * 8);
                    if (squareSizeIn < 15.0) squareSizeIn = 15.0;
                    double curvatureIn = curvature - (double)(i + 1) * 1.1;

                    var destBMP2 = effect.ConvertImage(tempBmp, rotationIn, squareSizeIn, 2, curvatureIn);
                    //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, 90 - Convert.ToInt32((double)i * 1.1));

                    int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
                    jpgQuality = 90;
                    // utilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, jpgQuality);

                    AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
                    def.DeformationID = 30 - i - 1;
                    //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                    //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                    def.ImageID = mainImage.ImageID;
                    def.DeformationType = 3;
                    def.ImageStream = utilities.SaveJpegToStream(destBMP2, 90).ToArray();
                    ent.AddToT_IMAGES_DEFORMATIONS(def);

                    tempBmp = null;
                    //destBMP = null;
                    destBMP2 = null;
                }

                ent.SaveChanges();

                CacheImagesManager.AddToCache(mainImage.ImageID);
            }
            catch (Exception ex)
            {
                throw (ex);
                //imageID = -1;
            }
        }
        else
        {
            imageID = -1;
        }
         
    }

    private void SaveToDBPolar(AdsCaptcha_ImagesEntities ent, ref int imageID)
    {
        /*
        if ((uploadImage.PostedFile != null) && (uploadImage.PostedFile.ContentLength > 0))
        {
            int width = 300;
            int height = 250;

            string fn = Guid.NewGuid().ToString() + ".jpg";// System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            string SaveLocation = ConfigurationManager.AppSettings["RandomImagesFolder300"] + "\\" + fn;

            switch (ddlDimentions.SelectedValue)
            {
                case "2":
                    width = 180;
                    height = 150;
                    SaveLocation = ConfigurationManager.AppSettings["RandomImagesFolder180"] + "\\" + fn;
                    break;
                default:

                    break;
            }
            try
            {
                ImageUtilities utilities = new ImageUtilities();

                uploadImage.PostedFile.SaveAs(SaveLocation);

                string imageDirName = Guid.NewGuid().ToString();

                //string imagesDir = ConfigurationManager.AppSettings["RandomImagesFolder300"] + imageDirName + "\\";
                //Directory.CreateDirectory(imagesDir);


                System.Drawing.Image coreimage = Bitmap.FromFile(SaveLocation);


                var resizedCoreBmp = utilities.ResizeImage(coreimage, width, height);
                FileInfo fi = new FileInfo(SaveLocation);

                var mainImage = ent.P_Images_Insert(2, utilities.SaveJpegToStream(resizedCoreBmp, 90).ToArray(),
                                                    fi.Extension.Replace(".", ""), width, height, null, null, null).FirstOrDefault();


                imageID = mainImage.ImageID;

                AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS defMain = new T_IMAGES_DEFORMATIONS();
                defMain.DeformationID = 30;
                //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                defMain.ImageID = mainImage.ImageID;
                defMain.DeformationType = 1;
                defMain.ImageStream = mainImage.ImageStream;
                ent.AddToT_IMAGES_DEFORMATIONS(defMain);

                //ent.P_Images_Deformations_Insert(30, 1, mainImage.ImageID, mainImage.ImageStream);


                double xStep = 0.05;

                Random rand = new Random(999);
                double yStep = (double)(rand.Next(999) * DateTime.Now.Millisecond % 26) / (double)20 / (double)30;

                double amountStep = 0.01;// ((double)(rand.Next(999) * DateTime.Now.Millisecond % 25)) / (double)30 ;

                WarpEffectBase effect = new PolarInversionEffect();

                for (int i = 0; i < 30; i++)
                {

                    var tempBmp = new Bitmap(resizedCoreBmp);

                    double offFirst = xStep * (double)(i + 1);
                    double offSecond = yStep * (double)(i + 1);
                    double amount = amountStep * Math.Pow((double)(i + 1), 1.7);
                    ((PolarInversionEffect)effect).Amount = amount;

                    var destBMP = effect.ConvertImage(tempBmp, amount, 2, offFirst, offSecond);
                    //var destBMP2 = effect.ConvertImage(tempBmp, amount, size, 2, (-1.0) * offFirst, (-1.0) * offSecond);

                    int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
                    jpgQuality = 90;
                    //jpgQuality = 90 - Convert.ToInt32((double)i * 0.4);
                    //utilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, jpgQuality);
                    //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, 90 - Convert.ToInt32((double)i * 1.1));

                    AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
                    def.DeformationID = 30 + i + 1;
                    //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                    //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                    def.ImageID = mainImage.ImageID;
                    def.DeformationType = 1;
                    def.ImageStream = utilities.SaveJpegToStream(destBMP, 90).ToArray();
                    ent.AddToT_IMAGES_DEFORMATIONS(def);

                    tempBmp = null;
                    destBMP = null;
                    //destBMP2 = null;
                }

                for (int i = 0; i < 30; i++)
                {

                    var tempBmp = new Bitmap(resizedCoreBmp);

                    double offFirst = -1 * xStep * (double)(i + 1);
                    double offSecond = -1 * yStep * (double)(i + 1);
                    double amount = amountStep * Math.Pow((double)(i + 1), 1.7);
                    ((PolarInversionEffect)effect).Amount = amount;

                    var destBMP2 = effect.ConvertImage(tempBmp, amount, 2, offFirst, offSecond);

                    //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, 90 - Convert.ToInt32((double)i * 1.1));
                    int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
                    jpgQuality = 90;
                    // utilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, jpgQuality);

                    AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
                    def.DeformationID = 30 - i - 1;
                    //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                    //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                    def.ImageID = mainImage.ImageID;
                    def.DeformationType = 1;
                    def.ImageStream = utilities.SaveJpegToStream(destBMP2, 90).ToArray();
                    ent.AddToT_IMAGES_DEFORMATIONS(def);

                    tempBmp = null;
                    //destBMP = null;
                    destBMP2 = null;
                }

                ent.SaveChanges();

                CacheImagesManager.AddToCache(mainImage.ImageID);
            }
            catch (Exception ex)
            {
                throw (ex);
                //imageID = -1;
            }
        }
        else
        {
            imageID = -1;
        }
    }

    private void SaveToDBFrostedGlass(AdsCaptcha_ImagesEntities ent, ref int imageID)
    {
        
        if ((uploadImage.PostedFile != null) && (uploadImage.PostedFile.ContentLength > 0))
        {
            int width = 300;
            int height = 250;

            string fn = Guid.NewGuid().ToString() + ".jpg";// System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            string SaveLocation = ConfigurationManager.AppSettings["RandomImagesFolder300"] + "\\" + fn;

            switch (ddlDimentions.SelectedValue)
            {
                case "2":
                    width = 180;
                    height = 150;
                    SaveLocation = ConfigurationManager.AppSettings["RandomImagesFolder180"] + "\\" + fn;
                    break;
                default:

                    break;
            }
            try
            {
                ImageUtilities utilities = new ImageUtilities();

                uploadImage.PostedFile.SaveAs(SaveLocation);

                string imageDirName = Guid.NewGuid().ToString();

                //string imagesDir = ConfigurationManager.AppSettings["RandomImagesFolder300"] + imageDirName + "\\";
                //Directory.CreateDirectory(imagesDir);


                System.Drawing.Image coreimage = Bitmap.FromFile(SaveLocation);


                var resizedCoreBmp = utilities.ResizeImage(coreimage, width, height);
                FileInfo fi = new FileInfo(SaveLocation);

                var mainImage = ent.P_Images_Insert(2, utilities.SaveJpegToStream(resizedCoreBmp, 90).ToArray(),
                                                    fi.Extension.Replace(".", ""), width, height, null, null, null).FirstOrDefault();


                imageID = mainImage.ImageID;

                AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS defMain = new T_IMAGES_DEFORMATIONS();
                defMain.DeformationID = 30;
                //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                defMain.ImageID = mainImage.ImageID;
                defMain.DeformationType = 1;
                defMain.ImageStream = mainImage.ImageStream;
                ent.AddToT_IMAGES_DEFORMATIONS(defMain);

                //ent.P_Images_Deformations_Insert(30, 1, mainImage.ImageID, mainImage.ImageStream);


                Random rand = new Random(999);
                double minRadius = (double)(rand.Next(999) * DateTime.Now.Millisecond % 100) + 50.0;

                double maxRadiusStep = 2.0;// ((double)(rand.Next(999) * DateTime.Now.Millisecond % 25)) / (double)30 ;

                FrostedGlassEffect effect = new FrostedGlassEffect();

                for (int i = 0; i < 30; i++)
                {

                    var tempBmp = new Bitmap(resizedCoreBmp);

                    //double maxRadius = maxRadiusStep * (double)(i + 1);
                    double maxRadius = maxRadiusStep * Math.Pow((double)(i + 1), 1.4);
                    if (minRadius > maxRadius) minRadius = Math.Max(maxRadius - 10.0, 0.0);


                    var destBMP = effect.ConvertImage(tempBmp, minRadius, maxRadius, 4);
                    //var destBMP2 = effect.ConvertImage(tempBmp, amount, size, 2, (-1.0) * offFirst, (-1.0) * offSecond);

                    int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
                    jpgQuality = 90;
                    //jpgQuality = 90 - Convert.ToInt32((double)i * 0.4);
                    //utilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, jpgQuality);
                    //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, 90 - Convert.ToInt32((double)i * 1.1));

                    AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
                    def.DeformationID = 30 + i + 1;
                    //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                    //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                    def.ImageID = mainImage.ImageID;
                    def.DeformationType = 1;
                    def.ImageStream = utilities.SaveJpegToStream(destBMP, 90).ToArray();
                    ent.AddToT_IMAGES_DEFORMATIONS(def);

                    tempBmp = null;
                    destBMP = null;
                    //destBMP2 = null;
                }

                for (int i = 0; i < 30; i++)
                {

                    var tempBmp = new Bitmap(resizedCoreBmp);

                    //double maxRadius = maxRadiusStep * (double)(i + 1);
                    double maxRadius = maxRadiusStep * Math.Pow((double)(i + 1), 1.4);
                    if (minRadius > maxRadius) minRadius = Math.Max(maxRadius - 10.0, 0.0);
                    //double maxRadius = maxRadiusStep * Math.Pow((double)(i + 1), 1.7);

                    var destBMP2 = effect.ConvertImage(tempBmp, minRadius, maxRadius, 4);

                    //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, 90 - Convert.ToInt32((double)i * 1.1));
                    int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
                    jpgQuality = 90;
                    // utilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, jpgQuality);

                    AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
                    def.DeformationID = 30 - i - 1;
                    //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                    //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                    def.ImageID = mainImage.ImageID;
                    def.DeformationType = 1;
                    def.ImageStream = utilities.SaveJpegToStream(destBMP2, 90).ToArray();
                    ent.AddToT_IMAGES_DEFORMATIONS(def);

                    tempBmp = null;
                    //destBMP = null;
                    destBMP2 = null;
                }

                ent.SaveChanges();

                CacheImagesManager.AddToCache(mainImage.ImageID);
            }
            catch (Exception ex)
            {
                throw (ex);
                //imageID = -1;
            }
        }
        else
        {
            imageID = -1;
        }
         
    }

    private void SaveToDBRadialBlur(AdsCaptcha_ImagesEntities ent, ref int imageID)
    {
        
        if ((uploadImage.PostedFile != null) && (uploadImage.PostedFile.ContentLength > 0))
        {
            int width = 300;
            int height = 250;

            string fn = Guid.NewGuid().ToString() + ".jpg";// System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            string SaveLocation = ConfigurationManager.AppSettings["RandomImagesFolder300"] + "\\" + fn;

            switch (ddlDimentions.SelectedValue)
            {
                case "2":
                    width = 180;
                    height = 150;
                    SaveLocation = ConfigurationManager.AppSettings["RandomImagesFolder180"] + "\\" + fn;
                    break;
                default:

                    break;
            }
            try
            {
                ImageUtilities utilities = new ImageUtilities();

                uploadImage.PostedFile.SaveAs(SaveLocation);

                string imageDirName = Guid.NewGuid().ToString();

                //string imagesDir = ConfigurationManager.AppSettings["RandomImagesFolder300"] + imageDirName + "\\";
                //Directory.CreateDirectory(imagesDir);


                System.Drawing.Image coreimage = Bitmap.FromFile(SaveLocation);


                var resizedCoreBmp = utilities.ResizeImage(coreimage, width, height);
                FileInfo fi = new FileInfo(SaveLocation);

                var mainImage = ent.P_Images_Insert(2, utilities.SaveJpegToStream(resizedCoreBmp, 90).ToArray(),
                                                    fi.Extension.Replace(".", ""), width, height, null, null, null).FirstOrDefault();


                imageID = mainImage.ImageID;

                AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS defMain = new T_IMAGES_DEFORMATIONS();
                defMain.DeformationID = 30;
                //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                defMain.ImageID = mainImage.ImageID;
                defMain.DeformationType = 1;
                defMain.ImageStream = mainImage.ImageStream;
                ent.AddToT_IMAGES_DEFORMATIONS(defMain);

                //ent.P_Images_Deformations_Insert(30, 1, mainImage.ImageID, mainImage.ImageStream);


                Random rand = new Random(999);

                double xStep = 0.04;
                double yStep = (double)(rand.Next(999) * DateTime.Now.Millisecond % 23) / (double)20 / (double)30;
                double angleStep = 0.5;// ((double)(rand.Next(999) * DateTime.Now.Millisecond % 25)) / (double)30 ;

                RadialBlurEffect effect = new RadialBlurEffect();

                for (int i = 0; i < 30; i++)
                {

                    var tempBmp = new Bitmap(resizedCoreBmp);

                    double offFirst = xStep * (double)(i + 1);
                    double offSecond = yStep * (double)(i + 1);
                    double angle = angleStep * Math.Pow((double)(i + 1), 1.7);

                    var destBMP = effect.ConvertImage(tempBmp, angle, 2, offFirst, offSecond);
                    //var destBMP2 = effect.ConvertImage(tempBmp, amount, size, 2, (-1.0) * offFirst, (-1.0) * offSecond);

                    int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
                    jpgQuality = 90;
                    //jpgQuality = 90 - Convert.ToInt32((double)i * 0.4);
                    //utilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, jpgQuality);
                    //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, 90 - Convert.ToInt32((double)i * 1.1));

                    AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
                    def.DeformationID = 30 + i + 1;
                    //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                    //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                    def.ImageID = mainImage.ImageID;
                    def.DeformationType = 1;
                    def.ImageStream = utilities.SaveJpegToStream(destBMP, 90).ToArray();
                    ent.AddToT_IMAGES_DEFORMATIONS(def);

                    tempBmp = null;
                    destBMP = null;
                    //destBMP2 = null;
                }

                for (int i = 0; i < 30; i++)
                {

                    var tempBmp = new Bitmap(resizedCoreBmp);

                    double offFirst = (-1) * xStep * (double)(i + 1);
                    double offSecond = (-1) * yStep * (double)(i + 1);
                    double angle = (-1) * angleStep * Math.Pow((double)(i + 1), 1.7);

                    var destBMP2 = effect.ConvertImage(tempBmp, angle, 2, offFirst, offSecond);


                    //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, 90 - Convert.ToInt32((double)i * 1.1));
                    int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
                    jpgQuality = 90;
                    // utilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, jpgQuality);

                    AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
                    def.DeformationID = 30 - i - 1;
                    //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                    //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                    def.ImageID = mainImage.ImageID;
                    def.DeformationType = 1;
                    def.ImageStream = utilities.SaveJpegToStream(destBMP2, 90).ToArray();
                    ent.AddToT_IMAGES_DEFORMATIONS(def);

                    tempBmp = null;
                    //destBMP = null;
                    destBMP2 = null;
                }

                ent.SaveChanges();

                CacheImagesManager.AddToCache(mainImage.ImageID);
            }
            catch (Exception ex)
            {
                throw (ex);
                //imageID = -1;
            }
        }
        else
        {
            imageID = -1;
        }
         
    }

    private void SaveToDBMotionBlur(AdsCaptcha_ImagesEntities ent, ref int imageID)
    {
        
        if ((uploadImage.PostedFile != null) && (uploadImage.PostedFile.ContentLength > 0))
        {
            int width = 300;
            int height = 250;

            string fn = Guid.NewGuid().ToString() + ".jpg";// System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            string SaveLocation = ConfigurationManager.AppSettings["RandomImagesFolder300"] + "\\" + fn;

            switch (ddlDimentions.SelectedValue)
            {
                case "2":
                    width = 180;
                    height = 150;
                    SaveLocation = ConfigurationManager.AppSettings["RandomImagesFolder180"] + "\\" + fn;
                    break;
                default:

                    break;
            }
            try
            {
                ImageUtilities utilities = new ImageUtilities();

                uploadImage.PostedFile.SaveAs(SaveLocation);

                string imageDirName = Guid.NewGuid().ToString();

                //string imagesDir = ConfigurationManager.AppSettings["RandomImagesFolder300"] + imageDirName + "\\";
                //Directory.CreateDirectory(imagesDir);


                System.Drawing.Image coreimage = Bitmap.FromFile(SaveLocation);


                var resizedCoreBmp = utilities.ResizeImage(coreimage, width, height);
                FileInfo fi = new FileInfo(SaveLocation);

                var mainImage = ent.P_Images_Insert(2, utilities.SaveJpegToStream(resizedCoreBmp, 90).ToArray(),
                                                    fi.Extension.Replace(".", ""), width, height, null, null, null).FirstOrDefault();


                imageID = mainImage.ImageID;

                AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS defMain = new T_IMAGES_DEFORMATIONS();
                defMain.DeformationID = 30;
                //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                defMain.ImageID = mainImage.ImageID;
                defMain.DeformationType = 1;
                defMain.ImageStream = mainImage.ImageStream;
                ent.AddToT_IMAGES_DEFORMATIONS(defMain);

                //ent.P_Images_Deformations_Insert(30, 1, mainImage.ImageID, mainImage.ImageStream);


                Random rand = new Random(999);

                double angle = ((double)(rand.Next(999) * DateTime.Now.Millisecond % 30)) + 15;

                MotionBlurEffect effect = new MotionBlurEffect();

                for (int i = 0; i < 30; i++)
                {

                    var tempBmp = new Bitmap(resizedCoreBmp);

                    int distance = (int)Math.Pow(i + 1, 1.7); ;

                    var destBMP = effect.ConvertImage(tempBmp, angle, distance, true);
                    //var destBMP2 = effect.ConvertImage(tempBmp, amount, size, 2, (-1.0) * offFirst, (-1.0) * offSecond);

                    int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
                    jpgQuality = 90;
                    //jpgQuality = 90 - Convert.ToInt32((double)i * 0.4);
                    //utilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, jpgQuality);
                    //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, 90 - Convert.ToInt32((double)i * 1.1));

                    AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
                    def.DeformationID = 30 + i + 1;
                    //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                    //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                    def.ImageID = mainImage.ImageID;
                    def.DeformationType = 1;
                    def.ImageStream = utilities.SaveJpegToStream(destBMP, 90).ToArray();
                    ent.AddToT_IMAGES_DEFORMATIONS(def);

                    tempBmp = null;
                    destBMP = null;
                    //destBMP2 = null;
                }

                angle = angle + 180.0;
                for (int i = 0; i < 30; i++)
                {

                    var tempBmp = new Bitmap(resizedCoreBmp);

                    int distance = (int)Math.Pow(i + 1, 1.7);

                    var destBMP2 = effect.ConvertImage(tempBmp, angle, distance, true);


                    //ImageUtilities.SaveJpeg(imagesDir + Convert.ToString(30 + i + 1) + ".jpg", destBMP, 90 - Convert.ToInt32((double)i * 1.1));
                    int jpgQuality = 90 - Convert.ToInt32((double)i * 1.1);
                    jpgQuality = 90;
                    // utilities.SaveJpeg(imagesDir + Convert.ToString(30 - i - 1) + ".jpg", destBMP2, jpgQuality);

                    AdsCaptcha_DemoModel.T_IMAGES_DEFORMATIONS def = new T_IMAGES_DEFORMATIONS();
                    def.DeformationID = 30 - i - 1;
                    //defMain.T_IMAGES_DEFORMATIONS_TYPESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES", "ImageID", mainImage.ImageID);
                    //defMain.T_IMAGESReference.EntityKey = new EntityKey("AdsCaptcha_ImagesEntities.T_IMAGES_DEFORMATIONS_TYPES", "DeformationTypeID", 1);
                    def.ImageID = mainImage.ImageID;
                    def.DeformationType = 1;
                    def.ImageStream = utilities.SaveJpegToStream(destBMP2, 90).ToArray();
                    ent.AddToT_IMAGES_DEFORMATIONS(def);

                    tempBmp = null;
                    //destBMP = null;
                    destBMP2 = null;
                }

                ent.SaveChanges();

                CacheImagesManager.AddToCache(mainImage.ImageID);
            }
            catch (Exception ex)
            {
                throw (ex);
                //imageID = -1;
            }
        }
        else
        {
            imageID = -1;
        }
         
    }
*/
}