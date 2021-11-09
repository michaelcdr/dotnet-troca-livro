using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebApp.Helpers
{
    public class EstadosHelper
    {
        public static List<SelectListItem> ObterTodos()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem("Acre","AC"),
                new SelectListItem("Alagoas","AL"),
                new SelectListItem("Amapá","AP"),
                new SelectListItem("Amazonas","AM"),
                new SelectListItem("Bahia","BA"),
                new SelectListItem("Ceará","CE"),
                new SelectListItem("Distrito Federal","DF"),
                new SelectListItem("Espírito Santo","ES"),
                new SelectListItem("Goiás","GO"),
                new SelectListItem("Maranhão","MA"),
                new SelectListItem("Mato Grosso","MT"),
                new SelectListItem("Mato Grosso do Sul","MS"),
                new SelectListItem("Minas Gerais","MG"),
                new SelectListItem("Pará","PA"),
                new SelectListItem("Paraíba","PB"),
                new SelectListItem("Paraná","PR"),
                new SelectListItem("Pernambuco","PE"),
                new SelectListItem("Piauí","PI"),
                new SelectListItem("Rio de Janeiro","RJ"),
                new SelectListItem("Rio Grande do Norte","RN"),
                new SelectListItem("Rio Grande do Sul","RS"),
                new SelectListItem("Rondônia","RO"),
                new SelectListItem("Roraima","RR"),
                new SelectListItem("Santa Catarina","SC"),
                new SelectListItem("São Paulo","SP"),
                new SelectListItem("Sergipe","SE"),
                new SelectListItem("Tocantins","TO")
            };
        }
    }
}