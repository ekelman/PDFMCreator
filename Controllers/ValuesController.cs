using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using iText.Layout;
using Microsoft.AspNetCore.Hosting;

namespace PDFMCreator.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ValuesController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //string webRootPath = _hostingEnvironment.WebRootPath;
            String src = _hostingEnvironment.ContentRootPath + "/Docs/BLGLegal Intake Form AcroForm.pdf";
            String dest = _hostingEnvironment.ContentRootPath + "/Docs/BLGLegal Intake Form AcroForm Filled.pdf";;

            using (PdfDocument pdf = new PdfDocument(new PdfReader(src), new PdfWriter(dest)))
            {
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
                IDictionary<String, PdfFormField> fields = form.GetFormFields();
                PdfFormField toSet;
                fields.TryGetValue("Client Name", out toSet);
                toSet.SetValue("James Bond");
                pdf.Close();
            }
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
