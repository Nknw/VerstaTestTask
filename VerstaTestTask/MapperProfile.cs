using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VerstaTestTask.Controllers;
using VerstaTestTask.Db;

namespace VerstaTestTask
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Order, OrderViewModel>();
            CreateMap<CreateOrderViewModel, Order>();
        }
    }
}
