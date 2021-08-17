using Syncfusion.Pdf;
using Syncfusion.Pdf.Exporting;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Replace_image
{
    class Program
    {
        static void Main(string[] args)
        {
            //Load the template document
            PdfLoadedDocument doc = new PdfLoadedDocument(new FileStream("../../../../../../Data/ImageInput.pdf",FileMode.Open));
            //Get first page of the document
            PdfLoadedPage page = doc.Pages[0] as PdfLoadedPage;
            //Get image info of the first image
            PdfImageInfo pdfImageInfo = page.GetImagesInfo()[0];
            RectangleF bounds = pdfImageInfo.Bounds;
            //Remove first image in the page
            page.RemoveImage(pdfImageInfo);
            //Pixlate the existing image
            Stream pixlated = Pixelate((Bitmap)pdfImageInfo.Image, new Rectangle(0, 0, pdfImageInfo.Image.Width, pdfImageInfo.Image.Height), 15);
            //Draw the pixlated image in the existing image bounds
            page.Graphics.DrawImage(new PdfBitmap(pixlated), bounds.X,bounds.Y,bounds.Width,bounds.Height);

            FileStream stream = new FileStream("output.pdf", FileMode.Create);
            //Save the modified document to file
            doc.Save(stream);
            //Close the PDF document.
            doc.Close(true);

        }

        private static Stream Pixelate(Bitmap image, Rectangle rectangle, Int32 pixelateSize)
        {
            Bitmap pixelated = new System.Drawing.Bitmap(image.Width, image.Height);

            // make an exact copy of the bitmap provided
            using (Graphics graphics = System.Drawing.Graphics.FromImage(pixelated))
                graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            // look at every pixel in the rectangle while making sure we're within the image bounds
            for (Int32 xx = rectangle.X; xx < rectangle.X + rectangle.Width && xx < image.Width; xx += pixelateSize)
            {
                for (Int32 yy = rectangle.Y; yy < rectangle.Y + rectangle.Height && yy < image.Height; yy += pixelateSize)
                {
                    Int32 offsetX = pixelateSize / 2;
                    Int32 offsetY = pixelateSize / 2;

                    // make sure that the offset is within the boundry of the image
                    while (xx + offsetX >= image.Width) offsetX--;
                    while (yy + offsetY >= image.Height) offsetY--;

                    // get the pixel color in the center of the soon to be a pixelated area
                    Color pixel = pixelated.GetPixel(xx + offsetX, yy + offsetY);

                    // for each pixel in the pixelate size, set it to the center color
                    for (Int32 x = xx; x < xx + pixelateSize && x < image.Width; x++)
                        for (Int32 y = yy; y < yy + pixelateSize && y < image.Height; y++)
                            pixelated.SetPixel(x, y, pixel);
                }
            }

            MemoryStream stream = new MemoryStream();
            pixelated.Save(stream, ImageFormat.Jpeg);
            stream.Position = 0;
            pixelated.Dispose();
            return stream;
        }
    }
}

