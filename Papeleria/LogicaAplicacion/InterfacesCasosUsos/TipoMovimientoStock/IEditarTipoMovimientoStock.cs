﻿using LogicaAplicacion.DTOs.TipoMovimientoStockDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosUsos.TipoMovimientoStock
{
    public interface IEditarTipoMovimientoStock
    {
        void Ejecutar(TipoMovimientoStockDto TipomovimientoStockDto, int id);

    }
}