using NerdStore.Models;
using Microsoft.EntityFrameworkCore;
using NerdStore;
using NerdStore.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace NerdStore
{
    //Classe para iniciar banco de dados
    class DataService : IDataService
    {
        private readonly ApplicationContext contexto;
        private readonly IProdutoRepository produtoRepository;

        public DataService(ApplicationContext contexto, 
            IProdutoRepository produtoRepository)
        {
            this.contexto = contexto;
            this.produtoRepository = produtoRepository;
        }

        public void InicializaDb()
        {
            contexto.Database.Migrate();

            //Ler arquivo JSON
            List<Objeto> Objetos = GetProdutos();

            //Preencher banco de dados
            produtoRepository.SaveProdutos(Objetos);

        }       

        private static List<Objeto> GetProdutos()
        {
            var json = File.ReadAllText("produtos.json");
            var Objetos = JsonConvert.DeserializeObject<List<Objeto>>(json);
            return Objetos;
        }
    }

    
}
