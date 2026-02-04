using Business.common;
using Business.Interfaces;
using Data.Context;
using Data.Interfaces;
using Entity.DTO;
using Entity.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace Business.Services
{
    public class ArticuloService : IArticuloService
    {
        private readonly IBaseRepository<Articulo> repository;
        private readonly IBaseRepository<TiendaArticulo> tiendaAticuloRepository;
        private readonly ApplicationDbContext context;

        public ArticuloService(IBaseRepository<Articulo> repository, 
            IBaseRepository<TiendaArticulo> tiendaAticuloRepository,
            ApplicationDbContext context) 
        {
            this.repository = repository;
            this.tiendaAticuloRepository = tiendaAticuloRepository;
            this.context = context;
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

            if(string.IsNullOrEmpty(articuloDTO.Imagen)) 
                return Result<Articulo>.Error("Datos incorrectos");

            try
            {
                string imageBase64 = articuloDTO.Imagen.Contains(",")
                    ? articuloDTO.Imagen.Split(',')[1]
                    : articuloDTO.Imagen;

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

                return Result<Articulo>.Ok(articulo);
            }
            catch (Exception ex) 
            {
                return Result<Articulo>.Error($"Ha ocurrido un error durante el registro: {ex.Message}");    
            }
        }

        public async Task<Result<Articulo>> Update(ArticuloCreateDTO articulo)
        {
            var currentData = await repository.GetByIdAsync(articulo.Id);
            if (currentData == null) return Result<Articulo>.Error("Artículo no encontrado");

            currentData.Codigo = articulo.Codigo;
            currentData.Descripcion = articulo.Descripcion;
            currentData.Precio = articulo.Precio;
            currentData.stock = articulo.Stock;

            if(!string.IsNullOrEmpty(articulo.Imagen))
            {
                string imageBase64 = articulo.Imagen.Contains(",")
                    ? articulo.Imagen.Split(',')[1]
                    : articulo.Imagen;

                try 
                { 
                    byte[] imageByte = Convert.FromBase64String(imageBase64);

                    if (currentData.Imagen == null 
                        || !StructuralComparisons.StructuralEqualityComparer.Equals(currentData.Imagen, imageByte))
                    { 
                        currentData.Imagen = imageByte;
                    }
                }
                catch(FormatException) 
                {
                    return Result<Articulo>.Error("Formato de imagen no válido");
                }
            }

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

        public async Task<Result<IEnumerable<Tienda>>> GetTiendasByArticuloId(int id)
        {
            try
            {
                var query = from tienda in context.Tiendas
                            join tiendaArticulo in context.TiendasArticulos on tienda.Id equals tiendaArticulo.TiendaId
                            where tiendaArticulo.ArticuloId == id
                            select tienda;

                var tiendas = await query.ToListAsync();

                if (tiendas == null)
                    return Result<IEnumerable<Tienda>>.Error("No se encontraron artículos para esta tienda.");

                return Result<IEnumerable<Tienda>>.Ok(tiendas);

            }
            catch (Exception ex)
            {
                return Result<IEnumerable<Tienda>>.Error("Error al obtener los artículos: " + ex.Message);
            }
        }

    }
}
