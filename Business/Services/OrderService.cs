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
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<ClienteArticulo> repository;

        public OrderService(IBaseRepository<ClienteArticulo> repository)
        {
            this.repository = repository;
        }

        public async Task<Result<bool>> create(OrderRequestDTO orderRequestDTO)
        {
            var itemList = new List<ClienteArticulo>();

            try
            {
                foreach (var item in orderRequestDTO.ItemList) 
                {
                    itemList.Add(new ClienteArticulo
                    {
                        ArticuloId = item.Id,
                        ClienteId = orderRequestDTO.ClienteId,
                        Cantidad = item.Cantidad,
                        Fecha = DateTime.Now
                    });            
                }

                await repository.InsertRangeAsync(itemList);
            }
            catch (Exception ex) {
                return Result<bool>.Error($"Ha ocurrido un error al intentar procesar la compra: {ex.Message}");
            }


            return Result<bool>.Ok(false);
        }
    }
}
