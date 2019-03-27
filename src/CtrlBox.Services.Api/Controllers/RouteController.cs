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
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteApplicationService _routeApplicationService;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeApplicationService"></param>
        /// <param name="mapper"></param>
        public RouteController(IRouteApplicationService routeApplicationService, IMapper mapper)
        {
            this._routeApplicationService = routeApplicationService;
            this._mapper = mapper;
        }

        // GET: api/Route
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<RouteVM> GetRoutes()
        {
            var routes = _routeApplicationService.GetAll();

            IList<RouteVM> routesVM = _mapper.Map<List<RouteVM>>(routes);
            return routesVM;
        }

        // GET: api/Route/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetRoute")]
        public RouteVM GetRoute(Guid id)
        {
            var routes = _routeApplicationService.GetById(id);

            RouteVM RouteVM = _mapper.Map<RouteVM>(routes);
            return RouteVM;
        }

        // POST: api/Route
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RouteVM"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(RouteVM RouteVM)
        {
            //if (ModelState.IsValid)
            if (true)
            {
                try
                {
                    var route = _mapper.Map<Route>(RouteVM);
                    var newClient = _routeApplicationService.Add(route);

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

        // PUT: api/Route/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("[action]")]
        public IActionResult ConnectRouteToClient(RouteVM routeVM)
        {
            //if (ModelState.IsValid)
            if (true)
            {
                try
                {
                    var route = _mapper.Map<Route>(routeVM);

                    var newClient = _routeApplicationService.ConnectRouteToClient(route, routeVM.ClientesIDs);

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