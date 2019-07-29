using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //importando
using System.Configuration; //importando
using Projeto.Entities; //importando
using Dapper; //importando

namespace Projeto.DAL
{
    public class ClienteRepository
    {
        //atributo
        private string connectionString;

        //construtor
        public ClienteRepository()
        {
            connectionString = ConfigurationManager
                .ConnectionStrings["projeto"].ConnectionString;
        }

        public void Insert(Cliente cliente)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "insert into Cliente(Nome, Email, DataCriacao) "
                             + "values(@Nome, @Email, GETDATE())";

                conn.Execute(query, cliente);
            }
        }

        public void Update(Cliente cliente)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "update Cliente set Nome = @Nome, Email = @Email "
                             + "where IdCliente = @IdCliente";

                conn.Execute(query, cliente);
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "delete from Cliente where IdCliente = @IdCliente";

                conn.Execute(query, new { IdCliente = id });
            }
        }

        public List<Cliente> SelectAll()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "select * from Cliente";

                return conn.Query<Cliente>(query).ToList();
            }
        }

        public Cliente SelectById(int id)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "select * from Cliente where IdCliente = @IdCliente";

                return conn.QuerySingleOrDefault<Cliente>(query, 
                        new { IdCliente = id });
            }
        }

        public bool HasEmail(string email)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "select count(Email) from Cliente "
                             + "where Email = @Email";

                return conn.QuerySingleOrDefault<int>(query,
                        new { Email = email }) > 0;
            }
        }
    }
}
