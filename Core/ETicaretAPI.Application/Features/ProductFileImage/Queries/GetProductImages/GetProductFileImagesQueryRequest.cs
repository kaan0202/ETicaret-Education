using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.ProductFileImage.Queries.GetProductImages
{
    public class GetProductFileImagesQueryRequest:IRequest<List<GetProductFileImagesQueryResponse>>
    {
        public string Id { get; set; }
    }
}
