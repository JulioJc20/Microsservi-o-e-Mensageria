using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.Serialization;
using static API_Publicador.Services.ApiService;

namespace API_Publicador.Domain
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Sexo  { get; set; }
        public DateTime DataNascimento{ get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

    }


}
