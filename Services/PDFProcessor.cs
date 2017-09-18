using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using iText.Layout;
using Microsoft.AspNetCore.Hosting;
using PDFMCreator.Interfaces;

namespace PDFMCreator.Services
{
    public class PDFProcessor : IPDFProcessor
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public PDFProcessor(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;

        }

        public String GetPdfFields()
        {
            //string webRootPath = _hostingEnvironment.WebRootPath;
            String src = _hostingEnvironment.ContentRootPath + "/Docs/BLGLegal Intake Form AcroForm.pdf";
            String dest = _hostingEnvironment.ContentRootPath + "/Docs/BLGLegal Intake Form AcroForm Filled.pdf";
            IDictionary<String, PdfFormField> fields = null;
            StringBuilder sb = new StringBuilder();

            using (PdfDocument pdf = new PdfDocument(new PdfReader(src), new PdfWriter(dest)))
            {
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
                fields = form.GetFormFields();
                sb.Append("{");
                foreach (var field in fields)
                {
                    sb.Append("{ \"" + field.Key + "\", \"String\" },");
                }
                sb.Append("};");
            }
            return sb.ToString();
        }

        public void FillPdfFields()
        {
            //string webRootPath = _hostingEnvironment.WebRootPath;
            GetPdfFields();

            String src = _hostingEnvironment.ContentRootPath + "/Docs/BLGLegal Intake Form AcroForm.pdf";
            String dest = _hostingEnvironment.ContentRootPath + "/Docs/BLGLegal Intake Form AcroForm Filled.pdf"; ;

            using (PdfDocument pdf = new PdfDocument(new PdfReader(src), new PdfWriter(dest)))
            {
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
                IDictionary<String, PdfFormField> fields = form.GetFormFields();
                PdfFormField toSet;
                fields.TryGetValue("Client Name", out toSet);
                toSet.SetValue("James Bond");
                pdf.Close();
            }
        }
    }
}
