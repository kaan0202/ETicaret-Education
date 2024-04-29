using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.ProductFileImage.Queries.GetProductImages
{
    public class GetProductFileImagesQueryHandler : IRequestHandler<GetProductFileImagesQueryRequest, List<GetProductFileImagesQueryResponse>>
    {
        readonly private IProductReadRepository _productReadRepository;
        readonly private IConfiguration _configuration;
        public GetProductFileImagesQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _productReadRepository = productReadRepository;
        }
        public async Task<List<GetProductFileImagesQueryResponse>> Handle(GetProductFileImagesQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            return  product.ProductImageFiles.Select(p => new GetProductFileImagesQueryResponse
            {

                //Path = $"{_configuration["BaseUrl"]}/{p.Path}",
               Id= p.Id,
                Path = $"{_configuration["Storage:Local"]}/{p.Path}",
              FileName =  p.FileName
            }).ToList();
        }
    }
}
