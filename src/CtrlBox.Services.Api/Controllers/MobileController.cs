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
        private readonly IBoxApplicationService _boxApplicationService;

        /// <summary>
        /// MobileController
        /// </summary>
        /// <param name="routeApplicationService"></param>
        /// <param name="boxApplicationService"></param>
        public MobileController(IRouteApplicationService routeApplicationService, IBoxApplicationService boxApplicationService)
        {
            _routeApplicationService = routeApplicationService;
            _boxApplicationService = boxApplicationService;
        }

        /// <summary>
        /// GetRoutesAvailable
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>--IEnumerable<RouteVM></returns>
        [HttpGet("[action]/{userID}")]
        public IEnumerable<RouteVM> GetRoutesAvailable(Guid userID)
        {
            var routes = _routeApplicationService.GetRoutesWithoutOpenOrder();
            return routes;
        }

        /// <summary>
        /// GetBoxesStockParents
        /// </summary>
        /// <param name="routeID"></param>
        /// <returns></returns>
        [HttpGet("[action]/{routeID}")]
        public IEnumerable<BoxVM> GetBoxesStockParents(Guid routeID)
        {
            var boxesVM = _boxApplicationService.GetBoxesStockParents(routeID);
            return boxesVM;
        }
    }
}
