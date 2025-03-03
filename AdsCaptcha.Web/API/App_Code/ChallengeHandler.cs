using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Inqwise.AdsCaptcha.API
{
    public class ChallengeHandler
    {
        /// <summary>
        /// Generates the challenge image.
        /// </summary>
        /// <param name="text">The text to be rendered into the image.</param>
        /// <param name="width">The width of the image to generate.</param>
        /// <param name="height">The height of the image to generate.</param>
        /// <returns>A dynamically-generated challenge image.</returns>
        public Bitmap GenerateImage(string textMessage, int width, int height, int[] severity, string colorBack, string colorText, bool rtl)
        {
            RandomNumbers random = new RandomNumbers();
            Size size = new Size(width, height);

            // Create a new 32-bit bitmap image.
            Bitmap bitmap = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);

            // Create a graphics object for drawing.
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            GraphicsPath path = new GraphicsPath();

            // Choose font to draw the letter with.
            FontFamily fontFamily = _families[random.Next(_families.Length)];

            // Choose a random font style (1=Regular, 2=Bold, 3=Italic, 4=Underline, 5=Strikeout).
            FontStyle fontStyle = (FontStyle)random.Next(2);

            // If current font doesn't supports the randomed style, switch to regular style.
            if (!fontFamily.IsStyleAvailable(fontStyle))
            {
                fontStyle = FontStyle.Regular;
            }

            // Choose font size.
            //float factor = 1 + (float)Convert.ToInt16(textMessage.Length / 2) / 10f;
            //float currFontSize = (float)bitmap.Height / factor; //1.6f
            float factor = (float)bitmap.Width / ((float)textMessage.Length * 20F);
            factor = (factor > 1 ? 1 : factor);
            float currFontSize = (float)(bitmap.Height - 2) * factor;

            // Set font.
            Font font = new Font(fontFamily, currFontSize, fontStyle);

            // Get string size.
            SizeF sizeText = g.MeasureString(textMessage, font);

            // Set string format as center aligment.
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;            
            if (rtl)
                stringFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            // Calculate middle point coordinates.
            PointF point = new PointF(bitmap.Width / 2, bitmap.Height / 2);

            // Define background and text brushes.            
            Brush brushBack = new SolidBrush(ColorTranslator.FromHtml(colorBack));
            Brush brushText = new SolidBrush(ColorTranslator.FromHtml(colorText));

            // Fill in the background.           
            g.FillRectangle(brushBack, rect);

            // Adds the text to grpahics path.
            path.AddString(textMessage, font.FontFamily, (int)font.Style, font.Size, point, stringFormat);

            int warp = severity[0];
            int distort = severity[1];
            int ellipse = severity[2];
            int arc = severity[3];
            int beizer = severity[4];
            int curve = severity[5];

            // Warp text.
            if (warp != 0) 
                WarpText(path, rect, warp);

            // Draw the text.
            g.FillPath(brushText, path);

            // Insert some random ellipse noise.
            if (ellipse != 0) 
                AddEllipseNoise(g, size, brushText, ellipse);

            // Insert some random arcs noise.
            if (arc != 0) 
                AddArcNoise(g, size, brushText, arc);

            // Insert some random beizer noise.
            if (beizer != 0) 
                AddBeizerNoise(g, size, brushText, beizer);

            // Insert some random curves noise.
            if (curve != 0) 
                AddCurveNoise(g, size, brushText, curve);

            // Distort image.
            if (distort != 0) 
                DistortImage(bitmap, distort);

            // Clean up.
            brushBack.Dispose();
            brushText.Dispose();
            path.Dispose();
            g.Dispose();

            // Set the image.
            return bitmap;
        }

        /// <summary>
        /// Warp text.
        /// </summary>
        /// <param name="path">Graphic path to be warpped.</param>
        /// <param name="rect">Rectangle area to be warped.</param>
        /// <param name="severity">Severity level [1..10].</param>
        private void WarpText(GraphicsPath path, Rectangle rect, int severity)
        {
            RandomNumbers random = new RandomNumbers();

            severity = (severity > 5 ? 5 : (severity < 1 ? 1 : severity));
            
            float v = 12F - severity;

            PointF[] points = {
				               new PointF(random.Next(rect.Width) / v, random.Next(rect.Height) / v),
				               new PointF(rect.Width - random.Next(rect.Width) / v, random.Next(rect.Height) / v),
				               new PointF(random.Next(rect.Width) / v, rect.Height - random.Next(rect.Height) / v),
				               new PointF(rect.Width - random.Next(rect.Width) / v, rect.Height - random.Next(rect.Height) / v)
			                  };
            Matrix matrix = new Matrix();
            matrix.Translate(0F, 0F);

            // Warp the text.
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);
        }

        /// <summary>
        /// Distorts the image.
        /// </summary>
        /// <param name="b">The image to be transformed.</param>
        /// <param name="severity">Severity level [1..10].</param>
        private void DistortImage(Bitmap b, int severity)
        {
            RandomNumbers random = new RandomNumbers();

            int width = b.Width;
            int height = b.Height;

            severity = (severity > 5 ? 5 : (severity < 1 ? 1 : severity));

            int min = (int)((float)severity * 0.9F);
            int max = min + 2;

            int distortion = random.Next(min, max) * (random.Next(2) == 1 ? 1 : -1);

            // Copy the image so that we're always using the original for source color.
            using (Bitmap copy = (Bitmap)b.Clone())
            {
                // Iterate over every pixel.
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        try
                        {
                            // Adds a simple wave.
                            int newX = (int)(x + (distortion * Math.Sin(Math.PI * y / 64.0)));
                            int newY = (int)(y + (distortion * Math.Cos(Math.PI * x / 64.0)));
                            if (newX < 0 || newX >= width) newX = 0;
                            if (newY < 0 || newY >= height) newY = 0;

                            // Re-set pixel.
                            b.SetPixel(x, y, copy.GetPixel(newX, newY));
                        }
                        catch { }
                    }
                }
            }
        }

        /// <summary>
        /// Adds ellipses noise.
        /// </summary>
        /// <param name="g">Graphics object to add noise to..</param>
        /// <param name="brush">Brush type.</param>
        /// <param name="severity">Severity level [1..5].</param>
        private void AddEllipseNoise(Graphics g, Size size, Brush brush, int severity)
        {
            RandomNumbers random = new RandomNumbers();

            int width = size.Width;
            int height = size.Height;

            severity = (severity > 5 ? 5 : (severity < 1 ? 1 : severity));

            for (int i = 0; i < (int)(width * height * 0.0012 * severity); i++)
            {
                try
                {
                    int x = random.Next(width);
                    int y = random.Next(height);
                    int w = random.Next(1, severity);
                    int h = random.Next(1, severity);

                    // Draw ellipse.
                    g.FillEllipse(brush, x, y, w, h);
                }
                catch { }
            }
        }

        /// <summary>
        /// Adds arcs noise.
        /// </summary>
        /// <param name="g">Graphics object to add noise to.</param>
        /// <param name="brush">Brush type.</param>
        /// <param name="severity">Severity level [1..5].</param>
        private void AddArcNoise(Graphics g, Size size, Brush brush, int severity)
        {
            RandomNumbers random = new RandomNumbers();

            int width = size.Width;
            int height = size.Height;

            severity = (severity > 5 ? 5 : (severity < 1 ? 1 : severity));

            int splitW = width / (2 + severity);

            for (int i = 1; i <= severity + 2; i++)
            {
                try
                {
                    int x = (width / 2) + (i % 2 == 0 ? i : -i) * (splitW / 2) + random.Next(-splitW / 4, splitW / 4);
                    int y = random.Next(height / 4, height / 4 * 3);
                    int w = random.Next(Math.Min(20, width - splitW), splitW);
                    int h = random.Next(Math.Min(10, height - y), height - y);

                    // Draw arc.
                    Pen pen = new Pen(brush, (float)random.NextDouble() * 1.4F);

                    int a = random.Next(-60, 60);
                    g.DrawArc(pen, x, y, w, h, a, a + random.Next(-220, 220));
                }
                catch { }
            }
        }

        /// <summary>
        /// Adds beizer noise.
        /// </summary>
        /// <param name="g">Graphics object to add noise to..</param>
        /// <param name="brush">Brush type.</param>
        /// <param name="severity">Severity level [1..5].</param>
        private void AddBeizerNoise(Graphics g, Size size, Brush brush, int severity)
        {
            RandomNumbers random = new RandomNumbers();

            int width = size.Width;
            int height = size.Height;

            severity = (severity > 5 ? 5 : (severity < 1 ? 1 : severity));

            int splitW = width / severity;

            for (int i = 0; i < severity; i++)
            {
                try
                {
                    int a = (width / 2) - severity * (width / 10);

                    int x1 = a + random.Next((i - 1) * splitW + 1, i * splitW);
                    int y1 = random.Next(height / 6, height / 6 * 4);
                    int x2 = a + random.Next((i - 1) * splitW + 1, i * splitW);
                    int y2 = random.Next(10, height / 6 * 7);
                    int x3 = a + random.Next(i * splitW + 1, (i + 1) * splitW);
                    int y3 = random.Next(height / 5, height / 6 * 5);
                    int x4 = a + random.Next(i * splitW + 1, (i + 1) * splitW);
                    int y4 = random.Next(10, height / 6 * 7);

                    // Draw beizer.
                    Pen pen = new Pen(brush, (0.4F + 0.1F * severity));
                    g.DrawBezier(pen, x1, y1, x2, y2, x3, y3, x4, y4);
                }
                catch { }
            }
        }        
        
        /// <summary>
        /// Adds curves noise.
        /// </summary>
        /// <param name="g">Graphics object to add noise to..</param>
        /// <param name="brush">Brush type.</param>
        /// <param name="severity">Severity level [1..5].</param>
        private void AddCurveNoise(Graphics g, Size size, Brush brush, int severity)
        {
            RandomNumbers random = new RandomNumbers();

            int width = size.Width;
            int height = size.Height;

            severity = (severity > 5 ? 5 : (severity < 1 ? 1 : severity));

            int splitW = width / severity;
            splitW = (int)((float)splitW / 1.5F);

            for (int i = 1; i < width / splitW + 1; i++)
            {
                int x1 = random.Next((i - 1) * splitW + 1, i * splitW);
                int y1 = random.Next(height / 8, height / 8 * 7);
                int x2 = random.Next(x1, x1 + splitW / 3);
                int y2 = random.Next(height / 8, height / 8 * 7);
                int x3 = random.Next(x2, x2 + splitW / 3);
                int y3 = random.Next(height / 8, height / 8 * 7);
                int x4 = random.Next(x3, x3 + splitW / 3);
                int y4 = random.Next(height / 8, height / 8 * 7);
                int x5 = random.Next(x4, x4 + splitW / 3);
                int y5 = random.Next(height / 8, height / 8 * 7);

                Point pt1 = new Point(x1, y1);
                Point pt2 = new Point(x2, y2);
                Point pt3 = new Point(x3, y3);
                Point pt4 = new Point(x4, y4);
                Point pt5 = new Point(x5, y5);
                Point[] ptsArray = { pt1, pt2, pt3, pt4, pt5 };

                int offset = 0;
                int segments = 3;
                float tension = (float)random.NextDouble();

                // Draw curve.
                Pen pen = new Pen(brush, (0.4F + 0.1F * severity));
                g.DrawCurve(pen, ptsArray, offset, segments, tension);
            }
        }

        /// <summary>
        /// Distorts the image.
        /// </summary>
        /// <param name="b">The image to be transformed.</param>
        /// <param name="distortion">An amount of distortion.</param>
        private static void DistortImage(Bitmap b, double distortion)
        {
            int width = b.Width, height = b.Height;

            // Copy the image so that we're always using the original for source color
            using (Bitmap copy = (Bitmap)b.Clone())
            {
                // Iterate over every pixel
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Adds a simple wave
                        int newX = (int)(x + (distortion * Math.Sin(Math.PI * y / 64.0)));
                        int newY = (int)(y + (distortion * Math.Cos(Math.PI * x / 64.0)));
                        if (newX < 0 || newX >= width) newX = 0;
                        if (newY < 0 || newY >= height) newY = 0;
                        b.SetPixel(x, y, copy.GetPixel(newX, newY));
                    }
                }
            }
        }

        /// <summary>
        /// List of fonts that can be used for rendering text.
        /// </summary>
        private static FontFamily[] _families = {
                                                     new FontFamily("Verdana"),
													 new FontFamily("Arial"),
													 //new FontFamily("Helvetica"),
													 new FontFamily("Tahoma"),
                                                     new FontFamily("Times New Roman"),
													 new FontFamily("Georgia"),
													 //new FontFamily("Stencil"),
													 new FontFamily("Comic Sans MS")
												 };
    }
}
