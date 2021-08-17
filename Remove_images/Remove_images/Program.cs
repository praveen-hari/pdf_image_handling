using Syncfusion.Pdf;
using Syncfusion.Pdf.Exporting;
using Syncfusion.Pdf.Parsing;
using System.IO;

namespace Remove_images
{
    class Program
    {
        static void Main(string[] args)
        {
            //Load the template document
            PdfLoadedDocument doc = new PdfLoadedDocument(new FileStream("../../../../../Data/ImageInput.pdf",FileMode.Open));
            //Get first page of the document
            PdfLoadedPage page = doc.Pages[0] as PdfLoadedPage;
            //Remove first image in the page
            page.RemoveImage(page.GetImagesInfo()[0]);
            FileStream stream = new FileStream("output.pdf", FileMode.Create);
            //Save the modified document to file
            doc.Save(stream);
            //Close the PDF document.
            doc.Close(true);
            stream.Close();

        }
    }
}
