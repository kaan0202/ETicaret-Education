using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ETicaretAPI.Domain.Results.Responses;

namespace ETicaretAPI.Application.Features.Product.Queries.GetAll
{
    public class GetAllProductQueryRequest:IRequest<GetAllProductQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
