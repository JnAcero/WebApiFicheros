using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFicheros.Services
{
    public interface IFicheroService
    {
        void SubirDocumento(IFormFile fichero);
        void SubirDocumentoBase64(string base64, string nombreFichero);
        byte[] BajarDocumento(string nombreFichero);
        object BajarDocumentoBase64(string nombreFichero);



    }

}
