﻿using NerdStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Repositories
{
    public interface IItemPedidoRepository
    {
        ItemPedido GetItemPedido(int itemPedido);

    }
    public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public ItemPedido GetItemPedido(int itemPedidoId)
        {
            return
                dbSet
                 .Where(ip => ip.Id == itemPedidoId)
                 .SingleOrDefault();
        }
    }
}
