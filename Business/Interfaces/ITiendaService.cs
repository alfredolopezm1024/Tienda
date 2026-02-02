using Business.common;
using Entity.DTO;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ITiendaService
    {
        Task<Result<IEnumerable<Tienda>>> GetAll();
        Task<Result<Tienda>> GetById(int id);
        Task<Result<Tienda>> Create(Tienda tienda);
        Task<Result<Tienda>> Update(Tienda tienda);
        Task<Result<bool>> Delete(int id);
        Task<Result<IEnumerable<Articulo>>> GetArticulosByTiendaId(int tienda);
    }
}
