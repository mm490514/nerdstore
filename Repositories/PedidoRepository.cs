using NerdStore.Controllers;
using NerdStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NerdStore.Models.ViewModel;

namespace NerdStore.Repositories
{
    public interface IPedidoRepository
    {
        Pedido GetPedido();
        void AddItem(string codigo);
        UpdateQuantidadeResponse UpdateQuantidade(ItemPedido itemPedido);
    }
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IItemPedidoRepository itemPedidoRepository;
        public PedidoRepository(ApplicationContext contexto, 
            IHttpContextAccessor contextAccessor,
            IItemPedidoRepository itemPedidoRepository) : base(contexto)
        {
            this.contextAccessor = contextAccessor;
            this.itemPedidoRepository = itemPedidoRepository;
        }

        public void AddItem(string codigo)
        {
            //obtendo o produto que tem o codigo no dbset
            var produto = contexto.Set<Produto>()
                 .Where(p => p.Codigo == codigo)
                 .SingleOrDefault();


            if(produto == null)
            {
                throw new ArgumentException("Produto não encontrado");
            }

            var pedido = GetPedido();

            //verificar se o item não existe para o pedido
            var itemPedido = contexto.Set<ItemPedido>()
                                 .Where(i => i.Produto.Codigo == codigo
                                         && i.Pedido.Id == pedido.Id)
                                 .SingleOrDefault();

            if (itemPedido == null)
            {
                itemPedido = new ItemPedido(pedido, produto, 1, produto.Preco);
                contexto.Set<ItemPedido>()
                    .Add(itemPedido);

                contexto.SaveChanges();


            }
        }

        public Pedido GetPedido()
        {
            //pegando pedidoId da sessão
            var pedidoId = GetPedidoId();

            var pedido = dbSet
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .Where(p => p.Id == pedidoId)
                .SingleOrDefault();
                    

            if (pedido == null)
            {
                pedido = new Pedido();
                dbSet.Add(pedido);
                contexto.SaveChanges();                
                SetPedidoId(pedido.Id);
            }

            return pedido;
        }

        //pegar o pedido na sessão
        private int? GetPedidoId()
        {
            return contextAccessor.HttpContext.Session.GetInt32("pedidoId");
        }

        //gravar pedido na sessão
        private void SetPedidoId(int pedidoId)
        {
            contextAccessor.HttpContext.Session.SetInt32("pedidoId", pedidoId);
        }

        public UpdateQuantidadeResponse UpdateQuantidade(ItemPedido itemPedido)
        {
            var itemPedidoDB = 
                itemPedidoRepository.
                GetItemPedido(itemPedido.Id);

            if (itemPedidoDB != null)
            {
                itemPedidoDB.AtualizaQuantidade(itemPedido.Quantidade);

                contexto.SaveChanges();

                var carrinhoViewModel = new CarrinhoViewModel(GetPedido().Itens);

                return new UpdateQuantidadeResponse(itemPedidoDB, carrinhoViewModel);
            }

            throw new ArgumentException("ItemPedido não encontrado");
        }
    }
}
