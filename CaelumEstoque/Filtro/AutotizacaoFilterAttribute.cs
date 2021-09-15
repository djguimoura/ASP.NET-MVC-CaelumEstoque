using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CaelumEstoque.Filtro
{
    //APÓS O ":" SIGNIFICA A CLASSE PRINCIPAL ESTÁ HERDANDO DA CLASSE ActionFilterAttribute
    public class AutotizacaoFilterAttribute : ActionFilterAttribute
    {
        //OVERRIDE SOBREESCREVE O MÉTODO EXISTENTE
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //ACESSA A SESSION PARA CAPTURAR O USUÁRIO
            object usuario = filterContext.HttpContext.Session["usuarioLogado"];
            //SE NÃO EXISTIR USUÁRIO, REDIRECIONA PARA A PAGE DE LOGIN
            if (usuario == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { controller = "Login", action = "Index"}    
                    )
                );
            }
        }
    }
}