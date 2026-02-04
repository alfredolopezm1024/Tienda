using Business.common;
using Business.Interfaces;
using Data.Context;
using Data.Interfaces;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class TiendaService : ITiendaService
    {
        private readonly IBaseRepository<Tienda> tiendaRepository;
        private readonly IBaseRepository<TiendaArticulo> tiendaArticuloRepository;
        private readonly ApplicationDbContext context;

        public TiendaService(IBaseRepository<Tienda> tiendaRepository, 
            IBaseRepository<TiendaArticulo> tiendaArticuloRepository,
            ApplicationDbContext context)
        {
            this.tiendaRepository = tiendaRepository;
            this.tiendaArticuloRepository = tiendaArticuloRepository;
            this.context = context;
        }

        public async Task<Result<IEnumerable<Tienda>>> GetAll()
        {
            var data = await tiendaRepository.GetAllAsync();

            return Result<IEnumerable<Tienda>>.Ok(data);
        }

        public async Task<Result<Tienda>> GetById(int id)
        {
            var data = await tiendaRepository.GetByIdAsync(id);

            if (data == null) return Result<Tienda>.Error("Tienda no encontrado");

            return Result<Tienda>.Ok(data);
        }

        public async Task<Result<Tienda>> Create(Tienda tienda)
        {
            await tiendaRepository.InsertAsync(tienda);
            await tiendaRepository.SaveAsync();

            return Result<Tienda>.Ok(tienda);
        }

        public async Task<Result<Tienda>> Update(Tienda tienda)
        {
            var currentData = await tiendaRepository.GetByIdAsync(tienda.Id);
            if (currentData == null) return Result<Tienda>.Error("Tienda no encontrado");

            currentData.Sucursal = tienda.Sucursal;
            currentData.Direccion = tienda.Direccion;

            await tiendaRepository.SaveAsync();

            return Result<Tienda>.Ok(currentData);
        }

        public async Task<Result<bool>> Delete(int id)
        {
            var currentData = await tiendaRepository.GetByIdAsync(id);
            if (currentData == null) return Result<bool>.Error("Tienda no encontrado");

            await tiendaRepository.DeleteAsync(id);
            await tiendaRepository.SaveAsync();

            return Result<bool>.Ok();
        }

        public async Task<Result<IEnumerable<Articulo>>> GetArticulosByTiendaId(int id) 
        {
            try
            {
                var query = from articulo in context.Articulos
                            join tiendaArticulo in context.TiendasArticulos on articulo.Id equals tiendaArticulo.ArticuloId
                            where tiendaArticulo.TiendaId == id
                            select articulo;

                var articulos = await query.ToListAsync();

                if(articulos == null)
                    return Result<IEnumerable<Articulo>>.Error("No se encontraron artículos para esta tienda.");

                return Result<IEnumerable<Articulo>>.Ok(articulos);

            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Articulo>>.Error("Error al obtener los artículos: " + ex.Message);
            }
        }
    }
}
