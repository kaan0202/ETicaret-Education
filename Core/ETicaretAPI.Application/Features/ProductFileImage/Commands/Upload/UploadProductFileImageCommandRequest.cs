using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.ProductFileImage.Commands.Upload
{
    public class UploadProductFileImageCommandRequest:IRequest<UploadProductFileImageCommandResponse>
    {
        public string Id { get; set; }
        public IFormFileCollection  FormFileCollection { get; set; }
    }
}
