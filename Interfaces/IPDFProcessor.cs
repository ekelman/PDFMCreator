using System;
using System.Collections.Generic;
using iText.Forms.Fields;

namespace PDFMCreator.Interfaces
{
    public interface IPDFProcessor
    {
        String GetPdfFields();
        void FillPdfFields();
    }
}
