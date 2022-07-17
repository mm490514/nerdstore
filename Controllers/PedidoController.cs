using NerdStore.Models;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NerdStore.Models.ViewModel;

namespace NerdStore.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;
        private readonly IItemPedidoRepository itemPedidoRepository;

        public PedidoController(IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository,
            IItemPedidoRepository itemPedidoRepository)
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.itemPedidoRepository = itemPedidoRepository;
        }

        //Chamar Views
        public IActionResult Carrossel()
        {          
            return View(produtoRepository.GetProdutos());
        }

        //passa como argumento o codigo do produto que foi selecionado no carrossel
        public IActionResult Carrinho(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo))
            {
                pedidoRepository.AddItem(codigo);
            }

            List<ItemPedido> itens = pedidoRepository.GetPedido().Itens;
            CarrinhoViewModel carrinhoViewModel = new CarrinhoViewModel(itens);
            return base.View(carrinhoViewModel);
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult Resumo()
        {
            Pedido pedido = pedidoRepository.GetPedido();
            return View(pedido);
        }

        [HttpPost]//Atributo de método
        public void UpdateQuantidade([FromBody]ItemPedido itemPedido) //FromBody - indicar que vem do corpo da requisição
        {
            itemPedidoRepository.UpdateQuantidade(itemPedido);
        }

    }
}
