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
    public class ClientController : ControllerBase
    {
        private readonly IClientApplicationService _clientApplicationService;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientApplicationService"></param>
        /// <param name="mapper"></param>
        public ClientController(IClientApplicationService clientApplicationService, IMapper mapper)
        {
            _clientApplicationService = clientApplicationService;
            _mapper = mapper;
        }
        // GET: api/Client
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ClientVM> GetClients()
        {
            var clients = _clientApplicationService.GetAll();

            IList<ClientVM> clientsVM = _mapper.Map<List<ClientVM>>(clients);
            return clientsVM;
        }

        // GET: api/Client/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetClient")]
        public ClientVM GetClient(Guid id)
        {
            var clients = _clientApplicationService.GetById(id);

           ClientVM clientVM = _mapper.Map<ClientVM>(clients);
            return clientVM;
        }

        // POST: api/Client
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientVM"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(Client), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(ClientVM clientVM)
        {
            //if (ModelState.IsValid)
            if (true)
            {
                try
                {
                    var client = _mapper.Map<Client>(clientVM);
                    var newClient = _clientApplicationService.Add(client);

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

        // PUT: api/Client/5
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idRoute"></param>
        /// <returns>Guid</returns>
        [HttpGet("[action]/{idRoute}")]
        public IEnumerable<ClientVM> GetAvailable(Guid idRoute)
        {
            var clients = _clientApplicationService.GetAvailable(idRoute);

            IList<ClientVM> clientsVM = _mapper.Map<List<ClientVM>>(clients);
            return clientsVM;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idRoute"></param>
        /// <returns>Guid</returns>
        [HttpGet("[action]/{idRoute}")]
        public IEnumerable<ClientVM> GetNotAvailable(Guid idRoute)
        {
            var clients = _clientApplicationService.GetNotAvailable(idRoute);

            IList<ClientVM> clientsVM = _mapper.Map<List<ClientVM>>(clients);
            return clientsVM;
        }
    }
}
