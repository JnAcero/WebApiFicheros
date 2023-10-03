using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFicheros.Services
{
    public class FicheroService : IFicheroService
    {
        private readonly IWebHostEnvironment _webHostEnviroment;
        public FicheroService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnviroment = webHostEnvironment;
        }

        public byte[] BajarDocumento(string nombreFichero)
        {
            string rutaDestino = _webHostEnviroment.ContentRootPath + "\\FicherosSubidos";
            string rutaDestinoCompleta = Path.Combine(rutaDestino,nombreFichero);
            byte[] bytes = System.IO.File.ReadAllBytes(rutaDestinoCompleta);

            return bytes;
        }

        public object BajarDocumentoBase64(string nombreFichero)
        {
            string rutaDestino = _webHostEnviroment.ContentRootPath + "\\FicherosSubidos";
            string rutaDestinoCompleta = Path.Combine(rutaDestino,nombreFichero);
            byte[] bytes = System.IO.File.ReadAllBytes(rutaDestinoCompleta);
            var base64String = Convert.ToBase64String(bytes);
            
            return base64String;
        }

        public void SubirDocumento(IFormFile fichero)
        {
            string rutaDestino = _webHostEnviroment.ContentRootPath + "\\FicherosSubidos";
            if (!Directory.Exists(rutaDestino)) Directory.CreateDirectory(rutaDestino);
            string rutaDestinoCompleta = Path.Combine(rutaDestino, fichero.FileName);

            if (fichero.Length > 0)
            {
                using (var stream = new FileStream(rutaDestinoCompleta, FileMode.Create))
                {
                    fichero.CopyTo(stream);
                }
            }
        }
        public void SubirDocumentoBase64(string base64, string nombreFichero)
        {
            string rutaDestino = _webHostEnviroment.ContentRootPath + "\\FicherosSubidos";
            if (!Directory.Exists(rutaDestino)) Directory.CreateDirectory(rutaDestino);
            string rutaDestinoCompleta = Path.Combine(rutaDestino, nombreFichero);

            byte[] documento = Convert.FromBase64String(base64);
            System.IO.File.WriteAllBytes(rutaDestinoCompleta,documento);
            
        }
    }
}