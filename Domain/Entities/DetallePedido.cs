﻿using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class DetallePedido
{
    public int CodigoPedido { get; set; }

    public string CodigoProducto { get; set; } = null!;

    public int Cantidad { get; set; }

    public decimal PrecioUnidad { get; set; }

    public short NumeroLinea { get; set; }

    public virtual Pedido CodigoPedidoNavigation { get; set; } = null!;

    public virtual Producto CodigoProductoNavigation { get; set; } = null!;
}
