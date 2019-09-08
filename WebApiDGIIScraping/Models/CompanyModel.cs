using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiDGIIScraping.Models
{
    public class CompanyModel
    {
        public string CedulaRnc { get; set; }
        public string Nombre { get; set; }
        public string NombreComercial { get; set; }
        public string Categoria { get; set; }
        public string RegimenDePago { get; set; }
        public string Estado { get; set; }
        public string ResponseType { get; set; }

    }
}