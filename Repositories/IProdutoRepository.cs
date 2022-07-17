using NerdStore.Models;
using System.Collections.Generic;

namespace NerdStore.Repositories
{
    public interface IProdutoRepository
    {
        void SaveProdutos(List<Objeto> Objetos);
        //Método pra obter produtos
        IList<Produto> GetProdutos();
    }
}