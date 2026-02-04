using Business.common;
using Entity.DTO;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IArticuloService
    {
        Task<Result<IEnumerable<Articulo>>> GetAll();
        Task<Result<Articulo>> GetById(int id);
        Task<Result<IEnumerable<Tienda>>> GetTiendasByArticuloId(int tienda);

        Task<Result<Articulo>> Create(ArticuloCreateDTO articulo);
        Task<Result<Articulo>> Update(ArticuloCreateDTO articulo);
        Task<Result<bool>> Delete(int id);
    }
}
