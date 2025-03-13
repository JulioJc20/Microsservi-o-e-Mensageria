using System.Text.Json.Serialization;

namespace API_Publicador.Model.Dto
{
    public class EnderecoDto
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }

    }
}
