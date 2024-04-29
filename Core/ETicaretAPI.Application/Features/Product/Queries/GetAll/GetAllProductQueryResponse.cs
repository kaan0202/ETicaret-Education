using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Product.Queries.GetAll
{
    public class GetAllProductQueryResponse
    {
        public object Products { get; set; }
        public int TotalCount { get; set; }
    }
}
