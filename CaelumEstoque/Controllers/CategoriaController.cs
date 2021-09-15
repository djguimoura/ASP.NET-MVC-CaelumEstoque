using CaelumEstoque.Filtro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    //ANOTATION DA CLASSE FILTER PARA PERMISSIONAR TODA A CLASSE
    [AutotizacaoFilter]
    public class CategoriaController : Controller
    {
        // GET: Categoria
        [Route("categorias", Name = "ListaCategorias")]
        public ActionResult Index()
        {
            DAO.CategoriasDAO dao = new DAO.CategoriasDAO();
            IList<Models.CategoriaDoProduto> categorias = dao.Lista();
            //Para enviarmos informações para a camada de visualização, podemos utilizar a variável ViewBag herdada da classe Controller
            ViewBag.Categorias = categorias;

            return View();
        }

        [Route("categoria", Name = "FormCategoria")]
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Adiciona(Models.CategoriaDoProduto categoria)
        {
            DAO.CategoriasDAO dao = new  DAO.CategoriasDAO();
            dao.Adiciona(categoria);
            return RedirectToAction("Index");
        }
    }
}