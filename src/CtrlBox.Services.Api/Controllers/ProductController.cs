﻿using System;
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
        /// Get Produts
        /// </summary>
        /// <returns>IList<ProductVM></returns>
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
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(ProductVM productVM)
        {
            //if (ModelState.IsValid)
            if (true)
            {
                try
                {
                    var product = _mapper.Map<Product>(productVM);
                    var prod = _productApplicationService.Add(product);

                    if (prod.Id != Guid.Empty)
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
                var product = _mapper.Map<Product>(productVM);
                var prod = _productApplicationService.Update(product);

                if (prod.Id != Guid.Empty)
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
    }
}