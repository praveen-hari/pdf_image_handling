
using System.IO;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;

namespace Add_image
{
    class Program
    {
        static void Main(string[] args)
        {
            //Load a PDF document
            PdfLoadedDocument doc = new PdfLoadedDocument(new FileStream("../../../../../Data/input.pdf",FileMode.Open));
            //Get first page from document
            PdfLoadedPage page = doc.Pages[0] as PdfLoadedPage;
            //Create PDF graphics for the page
            PdfGraphics graphics = page.Graphics;
            //Load the image from the disk
            PdfBitmap image = new PdfBitmap(new FileStream("../../../../../Data/Sample.jpg",FileMode.Open));
            //Draw the image
            graphics.DrawImage(image, new RectangleF(50, 150, 400, 250));
            //Creat stream object to save file
            FileStream stream = new FileStream("Output.pdf", FileMode.Create);
            //Save the document
            doc.Save(stream);
            //Close the document
            doc.Close(true);
            //Close stream
            stream.Close();
        }
    }
}
