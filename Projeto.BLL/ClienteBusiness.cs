using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Entities;
using Projeto.DAL;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace Projeto.BLL
{
    public class ClienteBusiness
    {
        //atributo..
        private ClienteRepository repository;

        //construtor -> ctor + 2x[tab]
        public ClienteBusiness()
        {
            repository = new ClienteRepository();
        }

        //método para cadastrar cliente
        public void CadastrarCliente(Cliente cliente)
        {
            //verificar se o email informado já está cadastrado
            if(repository.HasEmail(cliente.Email))
            {
                throw new Exception($"O Email {cliente.Email} já está cadastrado.");
            }
            else
            {
                repository.Insert(cliente);
                EnviarEmailDeBoasVindas(cliente);
            }            
        }

        //método para atualizar cliente
        public void AtualizarCliente(Cliente cliente)
        {
            repository.Update(cliente);
        }

        //método para excluir cliente
        public void ExcluirCliente(int id)
        {
            repository.Delete(id);
        }

        //método para listar todos os clientes
        public List<Cliente> ConsultarTodos()
        {
            return repository.SelectAll();
        }

        //método para consultar cliente por id
        public Cliente ConsultarPorId(int id)
        {
            return repository.SelectById(id);
        }

        //método para enviar um email de boas-vindas para o cliente
        private void EnviarEmailDeBoasVindas(Cliente cliente)
        {
            //capturando os dados mapeados no web.config.xml
            string conta = ConfigurationManager.AppSettings["CONTA"];
            string senha = ConfigurationManager.AppSettings["SENHA"];
            string smtp  = ConfigurationManager.AppSettings["SMTP"];
            string porta = ConfigurationManager.AppSettings["PORTA"];

            //passo 1) criando o email
            MailMessage message = new MailMessage(conta, cliente.Email);

            message.IsBodyHtml = true;
            message.Subject = "Conta de cliente cadastrada com sucesso!";
            message.Body = $"Seja bem vindo <strong>{cliente.Nome}</strong>"
                         + $"<br/><br/>"
                         + $"Sua conta de cliente foi criada com sucesso"
                         + $"<br/><br/>"
                         + $"Att<br/>Sistema de Controle de Clientes";

            //passo 2) enviando o email
            SmtpClient client = new SmtpClient(smtp, int.Parse(porta));
            client.EnableSsl = true; //habilitar criptografia do email
            client.Credentials = new NetworkCredential(conta, senha);
            client.Send(message); //enviando o email!
        }
    }
}
