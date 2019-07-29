using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper; //importando
using Projeto.BLL; //importando
using Projeto.Entities; //importando
using Projeto.Services.Models; //importando

namespace Projeto.Services.Controllers
{
    [RoutePrefix("api/Cliente")]
    public class ClienteController : ApiController
    {
        //atributo
        private ClienteBusiness business;

        //construtor -> ctor + 2x[tab]
        public ClienteController()
        {
            business = new ClienteBusiness();
        }
     
        [HttpPost]
        public HttpResponseMessage Post(ClienteCadastroViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var cliente = Mapper.Map<Cliente>(model);
                    business.CadastrarCliente(cliente);

                    //retornar um status de erro HTTP 200 (OK)
                    return Request.CreateResponse(HttpStatusCode.OK,
                        $"Cliente {cliente.Nome}, cadastrado com sucesso");
                }
                catch(Exception e)
                {
                    //retornar um status de erro HTTP 500 (InternalServerError)
                    return Request.CreateResponse(HttpStatusCode.InternalServerError,
                                            "Erro interno de servidor: " + e.Message);
                }
            }
            else
            {
                //retornar um status de erro HTTP 400 (BadRequest)
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                                        "Ocorreram erros de validação.");
            }
        }


        [HttpPut]
        public HttpResponseMessage Put(ClienteEdicaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cliente = Mapper.Map<Cliente>(model);
                    business.AtualizarCliente(cliente);

                    //retornar um status de erro HTTP 200 (OK)
                    return Request.CreateResponse(HttpStatusCode.OK,
                        $"Cliente {cliente.Nome}, atualizado com sucesso");
                }
                catch (Exception e)
                {
                    //retornar um status de erro HTTP 500 (InternalServerError)
                    return Request.CreateResponse(HttpStatusCode.InternalServerError,
                                            "Erro interno de servidor: " + e.Message);
                }
            }
            else
            {
                //retornar um status de erro HTTP 400 (BadRequest)
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                                        "Ocorreram erros de validação.");
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                //excluindo o cliente
                business.ExcluirCliente(id);

                //retornando sucesso..
                return Request.CreateResponse(HttpStatusCode.OK,
                                "Cliente excluído com sucesso.");
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    "Erro interno de servidor: " + e.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                //buscar os clientes na camada de negócio..
                var clientes = business.ConsultarTodos();

                //convertendo para uma lista da classe de modelo..
                var lista = Mapper.Map<List<ClienteConsultaViewModel>>(clientes);

                //retornando a lista
                return Request.CreateResponse(HttpStatusCode.OK, lista);
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    "Erro interno de servidor: " + e.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                //buscando o cliente pelo id..
                var cliente = business.ConsultarPorId(id);

                //convertendo o objeto para -> ClienteConsultaViewModel
                var model = Mapper.Map<ClienteConsultaViewModel>(cliente);

                //retornando o resultado
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                    "Erro interno de servidor: " + e.Message);
            }
        }

    }
}
