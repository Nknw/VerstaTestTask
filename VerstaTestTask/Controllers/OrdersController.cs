using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerstaTestTask.Db;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace VerstaTestTask.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrdersRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async IAsyncEnumerable<OrderViewModel> Get()
        {
            await foreach (var order in _ordersRepository.GetOrdersAsync())
                yield return _mapper.Map<OrderViewModel>(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateOrderViewModel createOrderViewModel)
        {
            var order = _mapper.Map<Order>(createOrderViewModel);
            var createdOrder = await _ordersRepository.InsertOrderAsync(order);
            return Ok(createdOrder);
        }
    }

    public class CreateOrderViewModel 
    {
        [Required]
        [StringLength(50)]
        public string SenderCity { get; set; }
        [Required]
        [StringLength(200)]
        public string SenderAddress { get; set; }
        [Required]
        [StringLength(50)]
        public string RecipientCity { get; set; }
        [Required]
        [StringLength(200)]
        public string RecipientAddress { get; set; }
        [Required]
        [Range(0, 999999)]
        public double Weight { get; set; }
        [Required]
        public DateTime ShipmentDate { get; set; }
    }

    public class OrderViewModel : CreateOrderViewModel
    {
        public int Id { get; set; }
    }
}
