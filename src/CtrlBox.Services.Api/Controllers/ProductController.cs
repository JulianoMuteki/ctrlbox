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
        [HttpGet]
        public IEnumerable<ProductVM> GetProdutsItems()
        {
            var prod = _productApplicationService.GetAll();

            IList<ProductVM> productVMs = _mapper.Map<List<ProductVM>>(prod);
            return productVMs;
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "GetProdutsItems")]
        public string GetProdutsItems(int id)
        {
            return "value";
        }

        // POST: api/Product
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productVM"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(ProductVM productVM)
        {
            //https://www.codeproject.com/Articles/1259484/CRUD-Operation-in-ASP-NET-Core-Web-API-with-Entity

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