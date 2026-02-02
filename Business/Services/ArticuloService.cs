using Business.common;
using Business.Interfaces;
using Data.Interfaces;
using Entity.DTO;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ArticuloService : IArticuloService
    {
        private readonly IBaseRepository<Articulo> repository;
        private readonly IBaseRepository<TiendaArticulo> tiendaAticuloRepository;

        public ArticuloService(IBaseRepository<Articulo> repository, 
            IBaseRepository<TiendaArticulo> tiendaAticuloRepository) 
        {
            this.repository = repository;
            this.tiendaAticuloRepository = tiendaAticuloRepository;

        }

        public async Task<Result<IEnumerable<Articulo>>> GetAll()
        {
            var data = await repository.GetAllAsync();

            return Result<IEnumerable<Articulo>>.Ok(data);
        }

        public async Task<Result<Articulo>> GetById(int id)
        {
            var data = await repository.GetByIdAsync(id);

            if (data == null) return Result<Articulo>.Error("Artículo no encontrado");

            return Result<Articulo>.Ok(data);
        }

        public async Task<Result<Articulo>> Create(ArticuloCreateDTO articuloDTO)
        {

            if (articuloDTO == null) return Result<Articulo>.Error("Datos incorrectos");

            if(string.IsNullOrEmpty(articuloDTO.ImagenBase64)) 
                return Result<Articulo>.Error("Datos incorrectos");

            try
            {
                string imageBase64 = articuloDTO.ImagenBase64.Contains(",")
                    ? articuloDTO.ImagenBase64.Split(',')[1]
                    : articuloDTO.ImagenBase64;

                var articulo = new Articulo
                {
                    Codigo = articuloDTO.Codigo,
                    Descripcion = articuloDTO.Descripcion,
                    Precio = articuloDTO.Precio,
                    stock = articuloDTO.Stock,
                    Imagen = Convert.FromBase64String(imageBase64)
                };

                await repository.InsertAsync(articulo);
                await repository.SaveAsync();

                var tiendaArticulo = new TiendaArticulo
                {
                    TiendaId = articuloDTO.TiendaId,
                    ArticuloId = articulo.Id,
                    Fecha = DateTime.Now
                };

                await tiendaAticuloRepository.InsertAsync(tiendaArticulo);
                await tiendaAticuloRepository.SaveAsync();

                return Result<Articulo>.Ok(articulo);
            }
            catch (Exception ex) 
            {
                return Result<Articulo>.Error($"Ha ocurrido un error durante el registro: {ex.Message}");    
            }
        }

        public async Task<Result<Articulo>> Update(Articulo articulo)
        {
            var currentData = await repository.GetByIdAsync(articulo.Id);
            if (currentData == null) return Result<Articulo>.Error("Artículo no encontrado");

            currentData.Codigo = articulo.Codigo;
            currentData.Descripcion = articulo.Descripcion;
            currentData.Precio = articulo.Precio;
            currentData.Imagen = articulo.Imagen;
            currentData.stock = articulo.stock;

            await repository.SaveAsync();

            return Result<Articulo>.Ok(currentData);
        }

        public async Task<Result<bool>> Delete(int id)
        {
            var currentData = await repository.GetByIdAsync(id);
            if (currentData == null) return Result<bool>.Error("Artículo no encontrado");

            await repository.DeleteAsync(id);
            await repository.SaveAsync();

            return Result<bool>.Ok();
        }

    }
}
