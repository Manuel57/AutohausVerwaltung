using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using BenutzerverwaltungBL.Model.DataObjects;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;

namespace BenutzerverwaltungBL.Controller
{
   public static class PdfGenerator
    {
        #region private fields
        private static int PAGEWIDTH = (int)PageSize.A4.Width;
        private static int PAGEWIDTHHALF = PAGEWIDTH / 2;
        private static int PAGEWIDTHDRITTEL = PAGEWIDTH / 3;
        private static int PAGEWIDTH2DRITTEL = PAGEWIDTH / 3 *2;
        private static int PAGECORRECTURE = PAGEWIDTH / 9;
        private static MemoryStream ms = new MemoryStream();
        private static Document doc = new Document(PageSize.A4);
        private static PdfWriter writer;
        #endregion
        public static byte[] GeneratePDF(Rechnung rechnung)
        {
            return new byte[2];
        }

        
    }
}
