using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CtrlBox.Application.ViewModel;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CtrlBox.Services.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        private readonly IRouteApplicationService _routeApplicationService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeApplicationService"></param>
        public MobileController(IRouteApplicationService routeApplicationService)
        {
            _routeApplicationService = routeApplicationService;
        }

        /// <summary>
        /// GetRoutesAvailable
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>--IEnumerable<RouteVM></returns>
        [HttpGet("[action]/{userID}")]
        public IEnumerable<RouteVM> GetRoutesAvailable(Guid userID)
        {
            var routes = _routeApplicationService.GetAll();
            return routes;
        }
    }
}
