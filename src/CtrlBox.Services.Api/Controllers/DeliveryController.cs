using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CtrlBox.Application.ViewModels;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CtrlBox.Services.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryApplicationService _deliveryApplicationService;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deliveryApplicationService"></param>
        /// <param name="mapper"></param>
        public DeliveryController(IDeliveryApplicationService deliveryApplicationService, IMapper mapper)
        {
            this._deliveryApplicationService = deliveryApplicationService;
            this._mapper = mapper;
        }

        // GET: api/Delivery
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<DeliveryVM> Get()
        {
            var deliveries = _deliveryApplicationService.GetAll();

            IList<DeliveryVM> deliveriesVM = _mapper.Map<List<DeliveryVM>>(deliveries);
            return deliveriesVM;
        }

        // GET: api/Delivery/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public DeliveryVM Get(Guid id)
        {
            var deliveries = _deliveryApplicationService.GetById(id);

            DeliveryVM DeliveryVM = _mapper.Map<DeliveryVM>(deliveries);
            return DeliveryVM;
        }

        // POST: api/Delivery
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DeliveryVM"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(DeliveryVM deliveryVM)
        {
            //if (ModelState.IsValid)
            if (true)
            {
                try
                {
                    Delivery delivery = _mapper.Map<Delivery>(deliveryVM);
                   var newClient = _deliveryApplicationService.Add(delivery);

                    if (newClient.Id != Guid.Empty)
                    {
                        return Ok(newClient);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        // PUT: api/Delivery/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}