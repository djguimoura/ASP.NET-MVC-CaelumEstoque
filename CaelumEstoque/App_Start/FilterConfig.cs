using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaelumEstoque.Filtro;

namespace CaelumEstoque.App_Start
{
    public class FilterConfig
    {
        //CONFIGURANDO UM FILTRO GLOBAL, PARA APLICAR O PERMISSIONAMENTO DO USER LOGADO PARA TODA A APLICAÇÃO
        public static void RegisterGlobalFilter(GlobalFilterCollection filters)
        {
            //filters.Add(new AutotizacaoFilterAttribute());
            filters.Add(new HandleErrorAttribute()
            {
                View = "ErroNaoMapeado"
            });
        }
    }
}