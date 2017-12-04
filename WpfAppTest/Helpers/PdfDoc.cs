using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTest.Helpers
{
    class PdfDoc
    {
        private string inputFilename;
        private PdfReader reader;
        private FileStream fs;
        private PdfWriter writer;

        public String OutputFilename { get; set; }

        public PdfDoc(string inputFilename, string outputFilename)
        {
            this.inputFilename = inputFilename;
            this.OutputFilename = outputFilename;

            OpenPdf();
        }

        public void OpenPdf()
        {
            // open the reader
            reader = new PdfReader(this.inputFilename);

            Rectangle size = reader.GetPageSizeWithRotation(1);
            Document document = new Document(size);

            // open the writer
            fs = new FileStream(this.OutputFilename, FileMode.Create, FileAccess.Write);
            writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            // the pdf content
            PdfContentByte content = writer.DirectContent;

            // create the new page and add it to the pdf
            PdfImportedPage page = writer.GetImportedPage(reader, 1);
            content.AddTemplate(page, 0, 0);

            // select the font properties
            //var font = GetTahoma().BaseFont;
            //font.FontType = Font.BOLD;

            //var font = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            var font = GetTahoma().BaseFont;

            //Font font = new Font(Font.FontFamily.HELVETICA, 22f, Font.BOLD, BaseColor.BLACK);

            //content.SetColorFill(BaseColor.BLACK);
            content.SetFontAndSize(font, 12);

            int leftMargin = 307;
            int rowDx = 28;

            WriteText(content, "Serial-Number-009", leftMargin, 620);
            WriteText(content, "Contractor-XXX", leftMargin, 592);
            WriteText(content, "JobName-002332", leftMargin, 592 - rowDx);
            WriteText(content, "Model-XXL223", leftMargin, 592 - 2 * rowDx);



            // close the streams and voilá the file should be changed :)
            document.Close();

            ClosePdf();
        }

        public void WriteText(PdfContentByte content, string text, float x, float y)
        {
            

            // write the text in the pdf content
            content.BeginText();
            //string text = "Some random blablablabla...";
            // put the alignment and coordinates here
            content.ShowTextAligned(Element.ALIGN_LEFT, text, x, y, 0);
            content.EndText();
        }

        public void ClosePdf()
        {
            fs.Close();
            writer.Close();
            reader.Close();
        }

        public static iTextSharp.text.Font GetTahoma()
        {
            var fontName = "Tahoma";
            if (!FontFactory.IsRegistered(fontName))
            {
                var fontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\tahoma.ttf";
                FontFactory.Register(fontPath);
            }
            return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 22f, Font.BOLD);
        }
    }
}
