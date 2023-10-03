using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiFicheros.Services;

namespace WebApiFicheros.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FicheroController : ControllerBase
    {
        private readonly IFicheroService _ficheroService;
        public FicheroController(IFicheroService ficheroService)
        {
            _ficheroService = ficheroService;
        }

        [HttpPost("subirDocumento")]
        public ActionResult SubirDocumento([FromForm] IFormFile fichero)
        {
            try
            {
                _ficheroService.SubirDocumento(fichero);
                return Ok("El documento se ha subido exitosamente");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("subirDocumentoBase64")]
        public ActionResult SubirDocumentoBase64([FromForm] string base64, [FromForm] string nombreFichero)
        {
            try
            {
                _ficheroService.SubirDocumentoBase64(base64, nombreFichero);
                return Ok("El documento se ha subido exitosamente");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("bajarDocumento")]
        public ActionResult BajarDocumento([FromForm] string nombreFichero)
        {
            try
            {
                var bytes = _ficheroService.BajarDocumento(nombreFichero);
                return File(bytes, "application/octet-stream", nombreFichero);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpPost("bajarDocumentoBase64")]
        public ActionResult BajarDocumentoBase64([FromForm] string nombreFichero)
        {
            try
            {
                var baseString = _ficheroService.BajarDocumentoBase64(nombreFichero);
                return Ok(baseString);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}