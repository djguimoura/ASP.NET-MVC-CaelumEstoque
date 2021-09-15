using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CaelumEstoque.DAO;
using CaelumEstoque.Filtro;
using CaelumEstoque.Models;

namespace CaelumEstoque.Controllers
{
    //ANOTATION DA CLASSE FILTER PARA PERMISSIONAR TODA A CLASSE
    [AutotizacaoFilter]
    public class ProdutoController : Controller
    {
        // GET: Produto
        //DATA ANOTATION
        [Route("produtos", Name ="ListaProdutos")]
        public ActionResult Index()
        {
            DAO.ProdutosDAO dao = new DAO.ProdutosDAO();
            var produtos = dao.Lista();
            //PARA ENVIARMOS INFORMAÇÕES PARA A CAMADA DE VISUALIZAÇÃO, PODEMOS UTILIZAR A VARIÁVEL VIEWBAG HERDADA DA CLASSE CONTROLLER
            return View(produtos);
        }

        [Route("produto", Name = "FormProduto")]
        public ActionResult Form()
        {
            //PARA INSERIR A LISTA DE OPÇÕES DO COMBO BOX DE CATEGORIAS NO FORM DE PRODUTOS
            //ViewBag UTILIZADA PARA GUARDAR OS DADOS APÓS VALIDAÇÕES DAREM ERROS
            DAO.CategoriasDAO dao = new DAO.CategoriasDAO();
            ViewBag.Produto = new Models.Produto();
            ViewBag.Categorias = dao.Lista();
            return View();
        }

        //LIMITANDO OS TIPOS DE REQUISIÇÕES SOMENTE PARA O TIPO POST
        [HttpPost]
        //SE O TOKEN FOR VÁLIDO EXECUTA O MÉTODO, SENÃO, SERÁ REJEITADO
        [ValidateAntiForgeryToken]
        public ActionResult Adiciona(Models.Produto produto)
        {
            //VALIDANDO CAMPO PREÇO IMPLEMENTANDO MANUALMENTE PARA ACEITAR VALOR ACIMA DE 100,00 SE O PRODUTO FOR DA CATEGORIA INFORMÁTICA
            //PORÉM O QUE ESTÁ SENDO USADO PARA OS PREÇOS DAS OUTRAS CATEGORIAS É O RangeAtributte NO MODELS
            int idDaInformatica = 1;
            if (produto.CategoriaId.Equals(idDaInformatica) && produto.Preco < 100)
            {
                ModelState.AddModelError("produto.Invalido", "Produtos da categoria informática devem ter preço maior do que 100");
            }

            //VALIDANDO O CAMPO NOME DO PRODUTO, SE VERDADEIRO GRAVA NO BANCO SENÃO RETORNA AO FORMULÁRIO
            if (ModelState.IsValid)
            {
                DAO.ProdutosDAO dao = new DAO.ProdutosDAO();
                dao.Adiciona(produto);
                //REDIRECIONA PARA O INDEX (LISTAGEM DE PRODUTOS) DO CONTROLLER ATUAL APÓS O SUBMIT
                return RedirectToAction("Index", "Produto");
            }
            else
            {
                //PARA GUARDAR OS DADOS PREENCHIDOS EM CASOS DE DADOS INVÁLIDOS, GUARDANDO NA ViewBag
                ViewBag.Produto = produto;
                DAO.CategoriasDAO categoriasDAO = new DAO.CategoriasDAO();
                ViewBag.Categorias = categoriasDAO.Lista();
                return View("Form");
            }

        }

        //CRIANDO VISUALIZAÇÃO INDIVIDUAL DE PRODUTOS
        [Route("produtos/{id}", Name ="VisualizaProduto")]
        public ActionResult Visualiza(int id)
        {
            DAO.ProdutosDAO dao = new DAO.ProdutosDAO();
            Models.Produto produto = dao.BuscaPorId(id);
            ViewBag.Produto = produto;
            return View();

        }

        //DECREMENTANDO QUANTIDADES DE PRODUTOS NO BANCO (RETIRANDO)
        public ActionResult DecrementaQtd(int id)
        {
            ProdutosDAO dao = new ProdutosDAO();
            Produto produto = dao.BuscaPorId(id);
            produto.Quantidade--;
            dao.Atualiza(produto);
            //PARA DEVOLVER UMA RESPOSTA COM O FORMATO JS
            return Json(produto);
        }
    }
}


/*PARA REDIRECIONAR ATRAVÉS DO MÉTODO DA HOMECONTROLLER
return RedirectToAction("Index", "Home");*/
