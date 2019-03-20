using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CtrlBox.Application.ViewModels;
using CtrlBox.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;

namespace CtrlBox.UI.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientApplicationService _userService;
        private readonly IMapper _mapper;

        public ClientController(IClientApplicationService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult AjaxHandlerClientes()
        {
            try
            {
                var clientes = _userService.GetAll();

                IList<ClientVM> c  = _mapper.Map<List<ClientVM>>(clientes);

                return Json(c);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /*
        public ActionResult CriarCliente()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CriarCliente(ClientVM cliente)
        {
            ModelCodeFirst context = new ModelCodeFirst();
            Cliente c = new Cliente();
            c.Nome = cliente.Nome;
            c.TotalCaixa = cliente.TotalCaixa;
            c.SaldoDevedor = cliente.SaldoDevedor;
            c.ClienteID = Guid.NewGuid();

            context.Clientes.Add(c);
            context.SaveChanges();

            return View("Clientes");
        }


        public ActionResult AssociarContatos(string clienteID)
        {
            ViewData["clienteID"] = clienteID;
            ModelCodeFirst context = new ModelCodeFirst();
            var cliente = context.Clientes.Where(x => x.ClienteID == new Guid(clienteID)).FirstOrDefault();

            return View(new ClientVM(cliente));
        }

        public ActionResult AjaxHandlerContatosDisponiveis()
        {
            try
            {

                ModelCodeFirst context = new ModelCodeFirst();
                //verificar se está vinculado à algum cliente
                var users = context.vw_aspnet_MembershipUsers.ToList();

                var clientes = context.Clientes.Include(p => p.aspnet_Users).ToList();
                var usersMembership = (from x in users where !clientes.Any(y => y.aspnet_Users.Any(z => z.UserId == x.UserId)) select x).ToList();

                IList<UserVM> usersVM = new List<UserVM>();
                foreach (var item in usersMembership)
                {
                    usersVM.Add(new UserVM(item));
                }

                return Json(new
                {
                    aaData = usersVM,

                    success = true
                    // SaldoAnterior = cadastroVenda.SaldoAnterior,
                    // CaixasEmDebito = cadastroVenda.CaixasEmDebito
                },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AjaxHandlerContatosVinculados(string clienteID)
        {
            try
            {

                ModelCodeFirst context = new ModelCodeFirst();

                var cliente = context.Clientes.Where(x => x.ClienteID == new Guid(clienteID)).FirstOrDefault();

                var us = cliente.aspnet_Users.ToList();
                //verificar se está vinculado à algum cliente
                var membershipUsers = context.vw_aspnet_MembershipUsers.ToList();

                var users = (from x in membershipUsers where us.Any(y => y.UserId == x.UserId) select x).ToList();

                IList<UserVM> usersVM = new List<UserVM>();
                foreach (var item in users)
                {
                    usersVM.Add(new UserVM(item));
                }

                return Json(new
                {
                    aaData = usersVM,

                    success = true
                    // SaldoAnterior = cadastroVenda.SaldoAnterior,
                    // CaixasEmDebito = cadastroVenda.CaixasEmDebito
                },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult SubmitAdicionarContatos(string clienteID, string contatosIDs)
        {
            var contatos = contatosIDs.Split('&').ToList();
            ModelCodeFirst context = new ModelCodeFirst();
            Guid idCliente = new Guid(clienteID);

            var cliente = context.Clientes.Where(x => x.ClienteID == idCliente).FirstOrDefault();

            foreach (var item in contatos)
            {
                var id = item.Split('=')[1];
                Guid idContato = new Guid(id);

                var contato = context.aspnet_Users.Where(x => x.UserId == idContato).FirstOrDefault();
                cliente.aspnet_Users.Add(contato);
            }

            context.SaveChanges();
            return Json(new
            {
                success = true,
                Message = "OK"
            },
                  JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult SubmitRemoverContatos(string clienteID, string contatosIDs)
        {
            var contatos = contatosIDs.Split('&').ToList();
            ModelCodeFirst context = new ModelCodeFirst();
            Guid idCliente = new Guid(clienteID);

            var cliente = context.Clientes.Where(x => x.ClienteID == idCliente).FirstOrDefault();

            foreach (var item in contatos)
            {
                var id = item.Split('=')[1];
                Guid idContato = new Guid(id);

                var contato = context.aspnet_Users.Where(x => x.UserId == idContato).FirstOrDefault();
                cliente.aspnet_Users.Remove(contato);
            }

            context.SaveChanges();
            return Json(new
            {
                success = true,
                Message = "OK"
            },
                  JsonRequestBehavior.AllowGet);

        }

    */
    }
}