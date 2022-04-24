using AutoMapper;
using EcommerceDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Web
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductViewModel, Product>();

            //CreateMap<List<Product>,List<ProductViewModel>>();
        }
    }
}
