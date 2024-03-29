﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDGIIScraping.Models;

namespace WebApiDGIIScraping.Controllers
{
    public class CompanyDetailsController : ApiController
    {
        public static CompanyModel result = new CompanyModel();
        // GET api/<controller>/5
        public CompanyModel Get(string rncCedula)
        {
            
            try
            {
                var client = new RestClient("http://www.dgii.gov.do/app/WebApps/Consultas/");
                var request = new RestRequest("rnc/RncWeb.aspx", Method.POST);

                ////Esto en realidad es un force. Son valores que ASP.NET espera en el HTTP Request. 
                request.AddParameter("__EVENTTARGET", "");
                request.AddParameter("__EVENTARGUMENT", "");
                request.AddParameter("__LASTFOCUS", "");
                request.AddParameter("__VIEWSTATE", "/wEPDwUKMTY4ODczNzk2OA9kFgICAQ9kFgQCAQ8QZGQWAWZkAg0PZBYCAgMPPCsACwBkZHTpAYYQQIXs/JET7TFTjBqu3SYU");
                request.AddParameter("__EVENTVALIDATION", "/wEWBgKl57TuAgKT04WJBAKM04WJBAKDvK/nCAKjwtmSBALGtP74CtBj1Z9nVylTy4C9Okzc2CBMDFcB");

                //Estos son los valores
                request.AddParameter("rbtnlTipoBusqueda", "0");
                request.AddParameter("txtRncCed", rncCedula);
                request.AddParameter("btnBuscaRncCed", "Buscar");

                // HTTP Headers
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                var response = client.Execute(request);

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(response.Content);
                var trs = doc.DocumentNode.Descendants("tr")
                    .FirstOrDefault(f => f.Attributes.Contains("class") && f.Attributes["class"].Value == "GridItemStyle");

                if (trs != null)
                {
                    var valores = trs.Descendants("td").ToList();

                    result.CedulaRnc = valores.First().InnerText;
                    result.Nombre = valores.Skip(1).First().InnerText.Replace("               ", " ");
                    result.NombreComercial = valores.Skip(2).First().InnerText;
                    result.Categoria = valores.Skip(3).First().InnerText;
                    result.RegimenDePago = valores.Skip(4).First().InnerText;
                    result.Estado = valores.Skip(5).First().InnerText;
                    result.ResponseType = "success";
                }
                return result;
            }
            catch (Exception e)
            {
                result.ResponseType = e.ToString();
                return result;
            }
        }

       
    }
}