using Business.common;
using Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IOrderService
    {
        Task<Result<bool>> create (OrderRequestDTO orderRequestDTO);
    }
}
