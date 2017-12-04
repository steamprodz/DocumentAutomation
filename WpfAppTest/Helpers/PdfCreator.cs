using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTest.Helpers
{
    class PdfCreator
    {
        public void OpenPdf(string filepath)
        {
            PdfReader reader = new PdfReader(filepath);
            PdfRectangle size = reader.GetPageSizeWithRotation
        }
    }
}
