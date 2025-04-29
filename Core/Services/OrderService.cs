using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.OrderModels;
using Services.Abstractions;
using Shared.OrderModels;

namespace Services
{
    public class OrderService(
        IMapper mapper,
        IBasketRepository basketRepository,
        IUnitOfWork unitOfWork
        ) 
        : IOrderService
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequest, string userEmail)
        {
            // 1. Address
           var address = mapper.Map<Address>(orderRequest.ShipToAddress);

            // 2. Order Items -> Basket
            var basket = await basketRepository.GetBasketAsync(orderRequest.BasketId);
            if(basket is null) throw new BasketNotFoundException(orderRequest.BasketId);

            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                // check id of item matches product that in DB
                var product = unitOfWork.GetRepository<Product, int>().GetAsync(item.Id);
                if(product is null) throw new ProductNotFoundExceptions(item.Id);

                var orderItem = new OrderItem(new ProductInOrderItem(product.Id,product.Name,product.PictureUrl ), item.Quantity, product.Price);
            }

            // Create Order
            var order = new Order(userEmail, address, orderItems);
        }

        public Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods()
        {
            throw new NotImplementedException();
        }

        public Task<OrderResultDto> GetOrderByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderResultDto>> GetOrderByUserEmailAsync(string userEmail)
        {
            throw new NotImplementedException();
        }
    }
}
