﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    public class ContadorController : Controller
    {
        // GET: Contador
        public ActionResult Index()
        {
            object valorNaSession = Session["Contador"];

            int contador = Convert.ToInt32(valorNaSession);

            contador++;

            Session["contador"] = contador;

            return View(contador);
        }
    }
}