using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaelumEstoque.Models
{
    public class Produto
    {
        public int Id { get; set; }

        //Model Validators PARA DEFINIR REGRAS DE VALIDAÇÃO NOS ATRIBUTOS - ABAIXO ESTÁ SENDO LIMITADO O CAMPO NOME PARA 20 CARACTERES
        [StringLength(20, ErrorMessage ="Campo só permite até 20 caracteres!")]
        [Required(ErrorMessage ="Campo obrigatório!")]
        public String Nome { get; set; }

        //RangeAtributte PARA VALIDAR INTERVALO DE VALORES PARA UM CAMPO
        [Range(0.0, 10000.0, ErrorMessage ="Campo só permite inserir valores entre 0,00 e 10.000,00!")]
        public float Preco { get; set; }

        public CategoriaDoProduto Categoria { get; set; }

        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório!")]
        public String Descricao { get; set; }

        [Range(1.0, 10000.0, ErrorMessage = "Quantidade não pode ser igual à 0!")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public int Quantidade { get; set; }
    }
}