using Business.common;
using Business.Interfaces;
using Data.Interfaces;
using Entity.DTO;
using Entity.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Business.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IBaseRepository<Cliente> repository;

        public ClienteService(IBaseRepository<Cliente> repository)
        {
            this.repository = repository;
        }

        public async Task<Result<IEnumerable<ClienteDTO>>> GetAll()
        {
            var data = await repository.GetAllAsync();

            var dtos = data.Select(c => MapToDTO(c));

            return Result<IEnumerable<ClienteDTO>>.Ok(dtos);
        }

        public async Task<Result<ClienteDTO>> GetById(int id)
        {
            var data = await repository.GetByIdAsync(id);

            if (data == null) return Result<ClienteDTO>.Error("Cliente no encontrado");

            return Result<ClienteDTO>.Ok(MapToDTO(data));
        }

        public async Task<Result<ClienteDTO>> Create(Cliente cliente)
        {

            cliente.password = BCrypt.Net.BCrypt.HashPassword(cliente.password);

            await repository.InsertAsync(cliente);
            await repository.SaveAsync();

            return Result<ClienteDTO>.Ok(MapToDTO(cliente));
        }

        public async Task<Result<ClienteDTO>> Update(Cliente cliente)
        {
            var currentData= await repository.GetByIdAsync(cliente.Id);
            if(currentData == null) return Result<ClienteDTO>.Error("Cliente no encontrado");

            currentData.Nombre = cliente.Nombre;
            currentData.Apellidos = cliente.Apellidos;
            currentData.Direccion = cliente.Direccion;

            await repository.SaveAsync();

            return Result<ClienteDTO>.Ok(MapToDTO(currentData));
        }

        public async Task<Result<bool>> Delete(int id)
        {
            var currentData = await repository.GetByIdAsync(id);
            if (currentData == null) return Result<bool>.Error("Cliente no encontrado");

            await repository.DeleteAsync(id);
            await repository.SaveAsync();

            return Result<bool>.Ok();
        }

        private ClienteDTO MapToDTO(Cliente cliente)
        {
            return new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellidos = cliente.Apellidos,
                Direccion = cliente.Direccion,
                correo = cliente.correo
            };
        }

    }
}
