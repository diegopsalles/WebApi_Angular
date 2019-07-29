using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
    public class Cliente
    {
        //propriedades -> prop + 2x[tab]
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; }

        //construtor default -> ctor + 2x[tab]
        public Cliente()
        {
            //vazio
        }

        //sobrecarga de métodos (OVERLOADING)
        public Cliente(int idCliente, string nome, string email, DateTime dataCriacao)
        {
            IdCliente = idCliente;
            Nome = nome;
            Email = email;
            DataCriacao = dataCriacao;
        }

        //sobrescrita do método ToString()
        public override string ToString()
        {
            return $"Id: {IdCliente}, Nome: {Nome}, Email: {Email}, Data Criação: {DataCriacao}";
        }
    }
}
