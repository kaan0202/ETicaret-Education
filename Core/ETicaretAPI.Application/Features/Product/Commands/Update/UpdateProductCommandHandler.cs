using ETicaretAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Product.Commands.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly private IProductReadRepository _productReadRepository;
        readonly private IProductWriteRepository _productWriteRepository;
        public UpdateProductCommandHandler(IProductReadRepository readRepository,IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = readRepository;
            _productWriteRepository = productWriteRepository;
        }
        async Task<UpdateProductCommandResponse> IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>.Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetByIdAsync(request.Id);
            product.Name = request.Name;
            product.Stock = request.Stock;
            product.Price = request.Price;
            _productWriteRepository.Update(product);
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
