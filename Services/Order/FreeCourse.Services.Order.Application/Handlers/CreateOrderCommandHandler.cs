using FreeCourse.Services.Order.Application.Commands;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Domain.OrderAggregate;
using FreeCourse.Services.Order.Infrastructure;
using FreeCourse.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand,Response< CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAdress = new Address(request.AddressDto.Province, request.AddressDto.Distinct, request.AddressDto.Street, request.AddressDto.ZipCode, request.AddressDto.Line);
            Domain.OrderAggregate.Order order = new Domain.OrderAggregate.Order(newAdress, request.BuyerId);
            request.OrderItems.ForEach(item =>
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.Price, item.PictureUrl);
            });
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId=order.Id},200) ;
        }
    }
}
