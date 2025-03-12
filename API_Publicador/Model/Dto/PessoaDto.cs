using API_Publicador.Domain;

namespace API_Publicador.Model.Dto
{
    public class PessoaDto
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public EnderecoDto Endereco { get; set; }
       
    }
}
