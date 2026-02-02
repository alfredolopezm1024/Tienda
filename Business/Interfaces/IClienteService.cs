using Business.common;
using Entity.DTO;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IClienteService
    {
        Task<Result<IEnumerable<ClienteDTO>>> GetAll();
        Task<Result<ClienteDTO>> GetById(int id);
        Task<Result<ClienteDTO>> Create(Cliente cliente);
        Task<Result<ClienteDTO>> Update(Cliente cliente);
        Task<Result<bool>> Delete(int id);
    }
}
