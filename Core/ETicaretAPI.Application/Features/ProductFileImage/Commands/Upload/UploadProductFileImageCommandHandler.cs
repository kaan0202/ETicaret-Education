using ETicaretAPI.Application.Abstraction.Storage;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.ProductFileImage.Commands.Upload
{
    public class UploadProductFileImageCommandHandler : IRequestHandler<UploadProductFileImageCommandRequest, UploadProductFileImageCommandResponse>
    {
        readonly private IProductImageFileReadRepository _productImageFileReadRepository;
        readonly private IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly private IStorageService _storageService;
        readonly private IProductReadRepository _productReadRepository;
        public UploadProductFileImageCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository,IProductImageFileReadRepository productImageFileReadRepository,IStorageService storageService,IProductReadRepository productReadRepository)
        {
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _storageService = storageService;
            _productReadRepository = productReadRepository;
        }
         async Task<UploadProductFileImageCommandResponse> IRequestHandler<UploadProductFileImageCommandRequest, UploadProductFileImageCommandResponse>.Handle(UploadProductFileImageCommandRequest request, CancellationToken cancellationToken)
        {
            //azure storage'da bu kısım files olarak değiştirilmelidir.
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", request.FormFileCollection);

            var product = await _productReadRepository.GetByIdAsync(request.Id);
            await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
                Product = new List<Domain.Entities.Product>() { product }

            }).ToList());
           await _productImageFileWriteRepository.SaveAsync();
            return new();
        }
    }
}
