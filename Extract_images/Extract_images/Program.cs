using System.Drawing;
using System.IO;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Exporting;
using Syncfusion.Pdf.Parsing;


namespace Extract_images
{
    class Program
    {
        static void Main(string[] args)
        {
            //Load the template document
            PdfLoadedDocument doc = new PdfLoadedDocument(new FileStream("../../../../../Data/ImageInput.pdf",FileMode.Open));
            //Load the first page
            PdfPageBase pageBase = doc.Pages[0];
            //Extract images from first page
            Image[] extractedImages = pageBase.ExtractImages();
            //Save images to file
            for (int i = 0; i < extractedImages.Length; i++)
            {
                extractedImages[i].Save("Image" + i + ".jpg");
            }

            //Close the document
            doc.Close(true);

        }
    }
}
