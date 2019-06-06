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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="deliveryVM"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(DeliveryVM deliveryVM)
        {
            //if (ModelState.IsValid)
            if (true)
            {
                try
                {
                   var newClient = _deliveryApplicationService.Add(deliveryVM);

                    if (newClient.ID != Guid.Empty)
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