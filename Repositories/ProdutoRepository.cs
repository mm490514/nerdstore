using CasaDoCodigo;
using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        //Construtor que passe o contexto pra classe base
        public ProdutoRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        //Acessar dbset e retornar uma lista
        public IList<Produto> GetProdutos()
        {
            return dbSet.ToList();
        }

        public void SaveProdutos(List<Objeto> Objetos)
        {
            foreach (var objeto in Objetos)
            {
                if (!dbSet.Where(p => p.Codigo == objeto.Codigo).Any())
                {
                    dbSet.Add(new Produto(objeto.Codigo, objeto.Nome, objeto.Preco));
                }
            }

            contexto.SaveChanges();
        }
    }

    public class Objeto
    {
        public Objeto(string codigo, string nome, decimal preco)
        {
            Codigo = codigo;
            Nome = nome;
            Preco = preco;
        }

        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}
