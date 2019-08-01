using System;
using System.Collections.Generic;
using AutoMapper;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;

namespace CtrlBox.Services.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
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
        public IEnumerable<OrderVM> Get()
        {
            var deliveries = _deliveryApplicationService.GetAll();

            IList<OrderVM> deliveriesVM = _mapper.Map<List<OrderVM>>(deliveries);
            return deliveriesVM;
        }

        // GET: api/Delivery/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public OrderVM Get(Guid id)
        {
            var deliveries = _deliveryApplicationService.GetById(id);

            OrderVM DeliveryVM = _mapper.Map<OrderVM>(deliveries);
            return DeliveryVM;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="deliveryVM"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(OrderVM deliveryVM)
        {
            //if (ModelState.IsValid)
            if (true)
            {
                try
                {
                   var newClient = _deliveryApplicationService.Add(deliveryVM);

                    if (string.IsNullOrEmpty(newClient.DT_RowId))
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
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


    }
}