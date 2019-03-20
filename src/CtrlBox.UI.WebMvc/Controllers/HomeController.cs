using CtrlBox.UI.WebMvc.EndPoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CtrlBox.UI.WebMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProdutoVMApi _api = null;

        public HomeController()
        {
            _api = new ProdutoVMApi("http://localhost:64195/");

        }
        public ActionResult Index()
        {
           
            var product = _api.GetProdutoVMAsync("api/Values");

            return View();
        }

        /*
        public ActionResult Vender(string linhaID, string clienteID, string entregaID)
        {
            ViewData["clienteID"] = clienteID;
            ViewData["linhaID"] = linhaID;
            ViewData["entregaID"] = entregaID;
            return View();
        }

        public ActionResult AjaxHandlerExecutarVenda(string clienteID, string linhaID)
        {
            try
            {
                Guid cliente = new Guid(clienteID);
                ModelCodeFirst context = new ModelCodeFirst();

                //Busca preço de produtos por clientes. Deve sempre existir preço para todos clientes
                var precoProdutos = context.PrecoProdutos_Clientes.Where(x => x.ClienteID == cliente).ToList();
                var produtos = context.Produtos.ToList();

                if (precoProdutos.Count != produtos.Count)
                    throw new Exception("Produto sem preço");

                CadastroVenda cadastroVenda = new CadastroVenda();
                cadastroVenda.ProdutosPrecos = new List<ProdutoPrecoVM>();
                foreach (var item in produtos)
                {
                    cadastroVenda.ProdutosPrecos.Add(new ProdutoPrecoVM()
                    {
                        DT_RowId = item.ProdutoID.ToString(),
                        NomeProduto = item.Nome,
                        ValorProduto = String.Format("{0:c}", (from x in precoProdutos where x.ProdutoID == item.ProdutoID && x.ClienteID == cliente select x).FirstOrDefault().Preco),
                        QtdeVenda = 0,
                        QtdeRetorno = 0,
                        Total = String.Format("{0:c}", 0)
                    });

                }

                return Json(new
                {
                    aaData = cadastroVenda.ProdutosPrecos,
                    success = true,
                    SaldoAnterior = cadastroVenda.SaldoAnterior,
                    CaixasEmDebito = cadastroVenda.CaixasEmDebito
                },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult SubmitData(string entregaID, string clienteID, string[] tbVenda, string ValorRecebido, string valorPrazo, string retornoCaixa, string[] tbCheques)
        {
            try
            {
                ModelCodeFirst context = new ModelCodeFirst();
                Guid entID = new Guid(entregaID); // new Guid(ViewData["entregaID"].ToString());

                JsonSerialize jsonS = new JsonSerialize();
                var vendas_Produtos = jsonS.JsonDeserialize<ProdutoPrecoVM>(tbVenda[0]);

                if (tbCheques[0].Count() > 0)
                {
                    var cheques = jsonS.JsonDeserialize<ChequeVM>(tbCheques[0]);
                }


                Venda venda = new Venda()
                {
                    VendaID = Guid.NewGuid(),
                    ClienteID = new Guid(clienteID),
                    EntregaID = entID,
                    ValorRecebido = Convert.ToDecimal(ValorRecebido),
                    ValorAPrazo = Convert.ToDecimal(valorPrazo),
                    CaixasRetornadas = Convert.ToInt32(retornoCaixa)

                };
                context.Vendas.Add(venda);

                foreach (var item in vendas_Produtos)
                {
                    Vendas_Produtos vendaProduto = new Vendas_Produtos()
                    {
                        VendasID = venda.VendaID,
                        ProdutoID = new Guid(item.DT_RowId),
                        Quantidade = Convert.ToInt32(item.QtdeVenda),
                        QuantidadeTroca = Convert.ToInt32(item.QtdeRetorno),
                        ValorVenda = Convert.ToDecimal(item.ValorProduto)
                    };

                    context.Vendas_Produtos.Add(vendaProduto);

                    var entregaProduto = context.Entregas_Produtos.Where(x => x.ProdutoID == vendaProduto.ProdutoID && x.EntregaID == entID).FirstOrDefault();
                    entregaProduto.Quantidade -= (int)vendaProduto.Quantidade;

                    context.Entregas_Produtos.Attach(entregaProduto);
                    context.Entry(entregaProduto).State = EntityState.Modified;
                }



                context.SaveChanges();
                return Json(new
                {
                    success = true,
                    Message = "OK"
                },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult ExecutarEntrega(string entregaID, string linhaID)
        {
            ViewData["entregaID"] = entregaID;
            ViewData["linhaID"] = linhaID;
            return View();
        }

        public ActionResult AjaxHandlerExecutarEntrega(string entregaID)
        {
            try
            {
                Guid idEntrega = new Guid(entregaID);
                ModelCodeFirst context = new ModelCodeFirst();

                var entrega = context.Entregas.Where(x => x.EntregaID == idEntrega).FirstOrDefault();
                var linha = context.Linhas.Where(x => x.LinhaID == entrega.LinhaID).FirstOrDefault();

                var clientes = linha.Clientes.ToList();
                EntregaVM entregaVM = new EntregaVM();

                entregaVM.Clientes = new List<ClienteVM>();

                foreach (var item in clientes)
                {
                    var venda = context.Vendas.Where(x => x.ClienteID == item.ClienteID && x.EntregaID == idEntrega).FirstOrDefault();
                    entregaVM.Clientes.Add(new ClienteVM() { DT_RowId = item.ClienteID.ToString(), Nome = item.Nome, Endereco = "Rua xxx", Contato = "Kleber", Telefone = "019-3232-9898", StatusEntrega = !(venda == null) });
                }


                var produtos = context.Produtos.ToList();
                var entregaProdutos = context.Entregas_Produtos.Where(x => x.EntregaID == entrega.EntregaID).ToList();

                var produtosQtdeEntrega = new List<Entrega_ProdutoVM>();
                foreach (var item in entregaProdutos)
                {
                    produtosQtdeEntrega.Add(new Entrega_ProdutoVM() { NomeProduto = produtos.Where(x => x.ProdutoID == item.ProdutoID).FirstOrDefault().Nome, Qtde = item.Quantidade });

                }
                var despesas = context.Despesas.Where(x => x.EntregaID == idEntrega).ToList();

                IList<DespesaVM> despesasVM = new List<DespesaVM>();

                foreach (var item in despesas)
                {
                    despesasVM.Add(new DespesaVM()
                    {
                        Descricao = item.Descricao,
                        Valor = item.Valor.ToString(),
                        DT_RowId = item.DespesaID.ToString()
                    });
                }

                return Json(new
                {
                    aaData = entregaVM.Clientes,
                    xaData = produtosQtdeEntrega,
                    xbData = despesasVM,
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

        public ActionResult SubmitDespesa(string entregaID, string descricao, string valor)
        {
            Guid idEntrega = new Guid(entregaID);
            ModelCodeFirst context = new ModelCodeFirst();

            Despesa despesa = new Despesa()
            {
                DespesaID = Guid.NewGuid(),
                EntregaID = idEntrega,
                Descricao = descricao,
                Valor = Convert.ToDouble(valor)
            };
            context.Despesas.Add(despesa);
            context.SaveChanges();

            return Json(new
            {
                success = true
            },
              JsonRequestBehavior.AllowGet);
        }

        public ActionResult Entregas()
        {

            return View();
        }

        [HttpGet]
        public ActionResult AjaxHandlerEntregas()
        {
            try
            {
                IList<object> lista = new List<object>();
                ModelCodeFirst context = new ModelCodeFirst();


                var entregasVM = context.Entregas.ToList();

                foreach (var item in entregasVM)
                {
                    lista.Add(new
                    {
                        DT_RowId = item.EntregaID.ToString(),
                        Linha = item.Linha.Nome,
                        LinhaID = item.Linha.LinhaID.ToString(),
                        CriadoPor = item.CriadoPor,
                        FinalizadoPor = item.FinalizadoPor,
                        DataCriação = item.DataInicio.ToString(),
                        DataFinalização = item.DataFim.ToString(),
                        Finalizado = item.Finalizado.ToString()
                    });
                }

                return Json(new
                {
                    aaData = lista,

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


        public ActionResult CadastrarEntrega()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AjaxHandlerDDL_Linhas()
        {
            try
            {
                IList<object> lista = new List<object>();
                ModelCodeFirst context = new ModelCodeFirst();
                var linhas = context.Linhas.ToList();
                var entregas = context.Entregas.Where(x => x.Finalizado == false).ToList();

                foreach (var item in linhas)
                {
                    if (!entregas.Any(x => x.LinhaID == item.LinhaID))// Não pode conter linha em aberto.
                        lista.Add(new
                        {
                            label = item.Nome,
                            value = item.LinhaID.ToString()
                        });
                }

                return Json(new
                {
                    aaData = lista,

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

        [HttpGet]
        public ActionResult AjaxHandlerProdutos()
        {
            try
            {
                IList<ProdutoVM> lista = new List<ProdutoVM>();
                ModelCodeFirst context = new ModelCodeFirst();

                var produtos = context.Produtos.ToList();

                foreach (var item in produtos)
                {
                    lista.Add(new ProdutoVM() { Nome = item.Nome, DT_RowId = item.ProdutoID.ToString() });
                }
                return Json(new
                {
                    aaData = lista,

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
        public ActionResult SubmitCadastrarEntrega(string[] tbProdutos, string linha)
        {
            try
            {
                ModelCodeFirst context = new ModelCodeFirst();
                Entrega entrega = new Entrega();
                JsonSerialize jsonS = new JsonSerialize();
                var produtosVM = jsonS.JsonDeserialize<ProdutoVM>(tbProdutos[0]);

                entrega.EntregaID = Guid.NewGuid();
                entrega.LinhaID = new Guid(linha);
                entrega.CriadoPor = "sistema";
                entrega.DataInicio = DateTime.Now;
                entrega.Finalizado = false;
                context.Entregas.Add(entrega);

                foreach (var item in produtosVM)
                {
                    Entregas_Produtos entProd = new Entregas_Produtos()
                    {
                        EntregaID = entrega.EntregaID,
                        ProdutoID = new Guid(item.DT_RowId),
                        Quantidade = Convert.ToInt32(item.Qtde)
                    };
                    context.Entregas_Produtos.Add(entProd);

                    var estoqueProduto = context.Estoque_Produtos.Where(x => x.ProdutoID == entProd.ProdutoID).FirstOrDefault();
                    estoqueProduto.Quantidade -= (int)entProd.Quantidade;

                    context.Estoque_Produtos.Attach(estoqueProduto);
                    context.Entry(estoqueProduto).State = EntityState.Modified;
                }

                context.SaveChanges();
                return Json(new
                {
                    success = true,
                    Message = "OK"
                },
                     JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult Clientes()
        {
            return View();
        }

        public ActionResult AjaxHandlerClientes()
        {
            try
            {

                ModelCodeFirst context = new ModelCodeFirst();

                var clientes = context.Clientes.ToList();
                IList<ClienteVM> c = new List<ClienteVM>();

                foreach (var item in clientes)
                {
                    c.Add(new ClienteVM() { DT_RowId = item.ClienteID.ToString(), Nome = item.Nome, SaldoDevedor = item.SaldoDevedor, TotalCaixa = item.TotalCaixa });
                }



                return Json(new
                {
                    aaData = c,

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


        public ActionResult CriarCliente()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CriarCliente(ClienteVM cliente)
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

            return View(new ClienteVM(cliente));
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


        public ActionResult Linhas()
        {
            return View();
        }

        public ActionResult AjaxHandlerLinhas()
        {
            try
            {

                ModelCodeFirst context = new ModelCodeFirst();

                var linhas = context.Linhas.ToList();

                IList<LinhaVM> linhasVM = new List<LinhaVM>();
                foreach (var item in linhas)
                {
                    linhasVM.Add(new LinhaVM(item));
                }

                return Json(new
                {
                    aaData = linhasVM,

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

        public ActionResult CadastrarLinha(string id)
        {
            ModelCodeFirst context = new ModelCodeFirst();

            LinhaVM linha = new LinhaVM();


            if (id != null)
            {
                var l = context.Linhas.Where(x => x.LinhaID == new Guid(id)).FirstOrDefault();
                linha = new LinhaVM(l);
            }

            return View(linha);
        }

        [HttpPost]
        public ActionResult CadastrarLinha(LinhaVM linhaVM)
        {
            ModelCodeFirst context = new ModelCodeFirst();
            Linha linha = new Linha();
            linha.LinhaID = (new Guid(linhaVM.DT_RowId) == Guid.Empty) ? Guid.NewGuid() : new Guid(linhaVM.DT_RowId);
            linha.Nome = linhaVM.Nome;
            linha.Tempo = Convert.ToInt32(linhaVM.Tempo);
            linha.Caminhao = linhaVM.Caminhao;
            linha.DistanciaKM = Convert.ToInt32(linhaVM.DistanciaKM);

            if (new Guid(linhaVM.DT_RowId) == Guid.Empty)
                context.Linhas.Add(linha);
            else
            {
                context.Linhas.Attach(linha);
                context.Entry(linha).State = EntityState.Modified;
            }

            context.SaveChanges();

            return View("Linhas");
        }


        public ActionResult AssociarClientes(string linhaID)
        {
            ViewData["linhaID"] = linhaID;

            LinhaVM linhaVM = new LinhaVM();
            using (ModelCodeFirst context = new ModelCodeFirst())
            {
                var linha = context.Linhas.Include(i => i.Entregas).Where(x => x.LinhaID == new Guid(linhaID)).FirstOrDefault();
                linhaVM = new LinhaVM(linha);

                linhaVM.ExisteEntregaAberta = linha.Entregas.Any(y => y.Finalizado == false);
            }
            return View(linhaVM);
        }

        public ActionResult AjaxHandlerClientesNaoDisponiveis(string linhaID)
        {
            try
            {
                Guid idLinha = new Guid(linhaID);
                ModelCodeFirst context = new ModelCodeFirst();

                //caso esta linha tenha alguma entrega pendente, não pode remover o cliente
                var linha = context.Linhas.Where(x => x.LinhaID == idLinha).FirstOrDefault();
                if (linha == null)
                    linha = new Linha();

                var clientes = linha.Clientes ?? new List<Cliente>();
                IList<ClienteVM> clientesVM = new List<ClienteVM>();

                foreach (var item in clientes)
                {
                    clientesVM.Add(new ClienteVM() { DT_RowId = item.ClienteID.ToString(), Nome = item.Nome, Endereco = "Rua xx", Contato = "Visualizar", Telefone = "019-3232-9898" });
                }

                return Json(new
                {
                    aaData = clientesVM,
                    success = true
                },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult AjaxHandlerClientesDisponiveis()
        {
            try
            {
                ModelCodeFirst context = new ModelCodeFirst();

                var clientes = context.Clientes.Where(x => x.Linhas.Count == 0).ToList();

                IList<ClienteVM> clientesVM = new List<ClienteVM>();

                foreach (var item in clientes)
                {
                    clientesVM.Add(new ClienteVM() { DT_RowId = item.ClienteID.ToString(), Nome = item.Nome, Endereco = "Rua xxx", Contato = "Kleber", Telefone = "019-3232-9898" });
                }

                return Json(new
                {
                    aaData = clientesVM,
                    success = true
                },
                JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult SubmitAdicionarClientes(string linhaID, string clientesIDs)
        {
            var clientes = clientesIDs.Split('&').ToList();
            ModelCodeFirst context = new ModelCodeFirst();
            Guid idLinha = new Guid(linhaID);

            var linha = context.Linhas.Where(x => x.LinhaID == idLinha).FirstOrDefault();

            foreach (var item in clientes)
            {
                var id = item.Split('=')[1];
                Guid idCliente = new Guid(id);

                var cliente = context.Clientes.Where(x => x.ClienteID == idCliente).FirstOrDefault();
                linha.Clientes.Add(cliente);

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
        public ActionResult SubmitRemoverClientes(string linhaID, string clientesIDs)
        {
            var clientes = clientesIDs.Split('&').ToList();
            ModelCodeFirst context = new ModelCodeFirst();
            Guid idLinha = new Guid(linhaID);

            var linha = context.Linhas.Where(x => x.LinhaID == idLinha).FirstOrDefault();

            foreach (var item in clientes)
            {
                var id = item.Split('=')[1];
                Guid idCliente = new Guid(id);

                var cliente = context.Clientes.Where(x => x.ClienteID == idCliente).FirstOrDefault();
                linha.Clientes.Remove(cliente);

            }

            context.SaveChanges();
            return Json(new
            {
                success = true,
                Message = "OK"
            },
                  JsonRequestBehavior.AllowGet);

        }


        public ActionResult ValorPorProduto(string clienteID)
        {
            ViewData["clienteID"] = clienteID;
            return View();
        }

        public ActionResult AjaxHandlerValorPorProduto(string clienteID)
        {
            try
            {
                IList<ValorPorProdutoVM> valoresProdutos = new List<ValorPorProdutoVM>();

                using (ModelCodeFirst contexto = new ModelCodeFirst())
                {
                    var produtos = contexto.Produtos.ToList();


                    foreach (var item in produtos)
                    {
                        var valor = contexto.PrecoProdutos_Clientes.Where(x => x.ClienteID == new Guid(clienteID)
                                                                                                    && x.ProdutoID == item.ProdutoID).FirstOrDefault();

                        valor = valor ?? new PrecoProdutos_Clientes();

                        valoresProdutos.Add(new ValorPorProdutoVM(valor, item));
                    }
                }

                return Json(new
                {
                    aaData = valoresProdutos,

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
        public ActionResult SubmitValorPorProduto(string clienteID, string lista)
        {
            var valores = lista.Split('&').ToList();
            using (ModelCodeFirst context = new ModelCodeFirst())
            {

                foreach (var item in valores)
                {
                    var val = item.Split('=');

                    if (!string.IsNullOrEmpty(val[1]))
                    {
                        string id = val[0];

                        Guid idcliente = new Guid(clienteID);
                        Guid idProduto = new Guid(id);

                        var valorProduto = context.PrecoProdutos_Clientes.Where(x => x.ClienteID == idcliente
                                                                                && x.ProdutoID == idProduto).FirstOrDefault();

                        if (valorProduto == null)
                        {
                            valorProduto = new PrecoProdutos_Clientes()
                            {
                                ClienteID = idcliente,
                                ProdutoID = idProduto,
                                Preco = Convert.ToDouble(val[1])
                            };

                            context.PrecoProdutos_Clientes.Add(valorProduto);
                        }
                        else
                        {
                            valorProduto.Preco = Convert.ToDouble(val[1]);
                            context.Entry(valorProduto).State = EntityState.Modified;

                        }

                    }
                }
                context.SaveChanges();
            }


            //var linha = context.Linhas.Where(x => x.LinhaID == idLinha).FirstOrDefault();

            //foreach (var item in clientes)
            //{
            //    var id = item.Split('=')[1];
            //    Guid idCliente = new Guid(id);

            //    var cliente = context.Clientes.Where(x => x.ClienteID == idCliente).FirstOrDefault();
            //    linha.Clientes.Remove(cliente);

            //}

            //context.SaveChanges();
            return Json(new
            {
                success = true,
                Message = "OK"
            },
                  JsonRequestBehavior.AllowGet);

        }

        public ActionResult CadastrarProduto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarProduto(Produto produto)
        {
            ModelCodeFirst context = new ModelCodeFirst();

            produto.ProdutoID = Guid.NewGuid();
            context.Produtos.Add(produto);

            context.SaveChanges();

            return View("ListaProdutos");
        }

        public ActionResult ListaProdutos()
        {
            ModelCodeFirst context = new ModelCodeFirst();
            return View(context.Produtos.ToList());
        }

        //Falta fazer
        public ActionResult Estoque()
        {
            return View();
        }

        public ActionResult Estoque(string caixa, string[] estoqueProdutos)
        {

            ModelCodeFirst context = new ModelCodeFirst();

            var estoque = context.Estoques.FirstOrDefault();
            if (estoque == null)
            {
                estoque = new Estoque();
                estoque.EstoqueID = Guid.NewGuid();
            }

            return Json(new
            {
                success = true,
                Message = "OK"
            },
                   JsonRequestBehavior.AllowGet);
        }

        public ActionResult Cheques()
        {
            return View();
        }

        public ActionResult ProdutosVendidos()
        {
            return View();
        }

        public ActionResult EntregasRealizadas()
        {
            return View();
        }

        public ActionResult VendasRealizadas()
        {
            return View();
        }
        */

    }
}