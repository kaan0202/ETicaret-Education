using ETicaretAPI.Application.Abstraction.Storage;
using ETicaretAPI.Application.Features.Product.Commands.Create;
using ETicaretAPI.Application.Features.Product.Commands.Remove;
using ETicaretAPI.Application.Features.Product.Commands.Update;
using ETicaretAPI.Application.Features.Product.Queries.GetAll;
using ETicaretAPI.Application.Features.Product.Queries.GetById;
using ETicaretAPI.Application.Features.ProductFileImage.Commands.Remove;
using ETicaretAPI.Application.Features.ProductFileImage.Commands.Upload;
using ETicaretAPI.Application.Features.ProductFileImage.Queries.GetProductImages;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParameters;

using ETicaretAPI.Application.ViewModels;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Results;
using ETicaretAPI.Persistance.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        readonly private IStorageService _storageService;
       readonly private IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly private IConfiguration _configuration;
        readonly private IMediator _mediator;



        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository, IConfiguration configuration, IMediator mediator = null)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _storageService = storageService;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest request)
        {
           GetAllProductQueryResponse dto = await _mediator.Send(request);

            return Ok(dto);
            
          
         
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] CreateProductCommandRequest request) 
        {
          CreateProductCommandResponse response = await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery]UploadProductFileImageCommandRequest request)
        {
            request.FormFileCollection = Request.Form.Files;
         UploadProductFileImageCommandResponse response = await _mediator.Send(request);
            return Ok(response);


        }
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetProductImages([FromRoute]GetProductFileImagesQueryRequest request)
        {
          List<GetProductFileImagesQueryResponse> response = await _mediator.Send(request); 
            return Ok(response);

        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest request)
        {
            GetByIdProductQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest request)
        {
           UpdateProductCommandResponse response =  await _mediator.Send(request);
            return Ok(response);
            
        }
        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] RemoveProductFileImageCommandRequest request, [FromQuery] string id)
        {
            request.ImageId = id;
          RemoveProductFileImageCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest request)
        {
            RemoveProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);

        }


    }

}

