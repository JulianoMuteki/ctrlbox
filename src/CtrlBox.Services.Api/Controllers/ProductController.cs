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
    [Route("api/Product")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductApplicationService _productApplicationService;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productApplicationService"></param>
        /// <param name="mapper"></param>
        public ProductController(IProductApplicationService productApplicationService, IMapper mapper)
        {
            _productApplicationService = productApplicationService;
            _mapper = mapper;
        }

        // GET: api/Product
        /// <summary>
        /// GetProdutsItems
        /// </summary>
        /// <returns>Lista ProductVM</returns>
        [HttpGet]
        public IEnumerable<ProductVM> GetProdutsItems()
        {
            var prod = _productApplicationService.GetAll();

            IList<ProductVM> productVMs = _mapper.Map<List<ProductVM>>(prod);
            return productVMs;
        }

        // GET: api/Product/5
        /// <summary>
        /// Get Produt
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>ProductVM</returns>
        [HttpGet("{id}", Name = "GetProdutsItems")]
        public ProductVM GetProdutsItems(Guid id)
        {
            var prod = _productApplicationService.GetById(id);

            ProductVM productVM = _mapper.Map<ProductVM>(prod);
            return productVM;
        }

        // POST: api/Product
        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="productVM">ProductVM</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult Post(ProductVM productVM)
        {
            //if (ModelState.IsValid)
            if (true)
            {
                try
                {
                    var prod = _productApplicationService.Add(productVM);

                    if (string.IsNullOrEmpty(prod.DT_RowId))
                    {
                        return Ok(prod);
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

        // PUT: api/Product/5
        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="productVM">ProductVM</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public IActionResult Put(ProductVM productVM)
        {
            try
            {
                var prod = _productApplicationService.Update(productVM);

                if (string.IsNullOrEmpty(prod.DT_RowId))
                {
                    return Ok(prod);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="id">Guid</param>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _productApplicationService.Delete(id);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPost("[action]")]
        public IActionResult ConnectProductToClient(ICollection<ClientProductValueVM> clientsProductsValuesVM)
        {
            //if (ModelState.IsValid)
            if (true)
            {
                try
                {
                    var newClient = _productApplicationService.ConnectRouteToClient(clientsProductsValuesVM);

                    if (newClient.Count > 0)
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
      
    }
}