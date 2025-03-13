using Microsoft.AspNetCore.Mvc;
using API_Publicador.Domain;
using RabbitMQ.Client;
using System.Threading.Channels;
using API_Publicador.Publicadora;
using System.Text.Json;
using API_Publicador.Services;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using API_Publicador.Model.Dto;

namespace API_Publicador.Controllers
{
    [Route("Pessoas")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly ApiService _apiService;

        public PessoasController(ApiService apiService)
        {
            _apiService = apiService;
        }


        [HttpGet("Chamar_APIConsumidora/{nome}")]
        public async Task<IActionResult> ChamarApiConsumidora(string nome)
        {
            var resultado = await _apiService.ChamarApiConsumidora(nome);

            if (resultado.StartsWith("Erro"))
            {
                return StatusCode(500, resultado);
            }

            return Ok(resultado);
        }

        [HttpGet("Validar_CEP")]
        public async Task<IActionResult> ChamarApiCEP(string cep)
        {
            var resposta = await _apiService.ChamarApiCep(cep);

            if (resposta.StartsWith("Error"))
            {
                return StatusCode(500, resposta);
            }
            return Ok(resposta);
        }

        [HttpPost("EnviarPessoas")] 
        public async Task<IActionResult> Criar([FromBody] PessoaDto pessoa) 
        {
            var resposta = await _apiService.ChamarApiCep(pessoa.Endereco.Cep);
            var retornoApi = JsonSerializer.Deserialize<EnderecoResponseDto>(resposta);

            if (retornoApi == null)
            {
                return BadRequest("CEP inválido ou não encontrado");
            }
            var pessoaResposta = new
            {

                Nome = pessoa.Nome,
                Cpf = pessoa.Cpf,
                Rg = pessoa.Rg,
                Sexo = pessoa.Sexo,
                DataNascimento = pessoa.DataNascimento,
                Email = pessoa.Email,
                Telefone = pessoa.Telefone,
                Endereco = retornoApi,
            };


            var publicarFila = new PublicarFila();
            var message = JsonSerializer.Serialize(pessoaResposta);
            publicarFila.PublicarMensagem("Criar", message);

            return Ok("Pessoa publicada na fila com sucesso");
        }

        [HttpDelete("DeletarPessoas")]
        public Task<IActionResult> Deletar([FromBody] DeletarPessoaDto pessoa )
        {
            var deletarFila = new DeletarFila();
            var message = JsonSerializer.Serialize(pessoa);
            
            deletarFila.DeletarMensagem("Deletar", message);
            
             return Task.FromResult<IActionResult>(Ok("Pessoa deletada da fila com sucesso"));
           
        }
        [HttpPut("AtualizarPessoas")]
        public Task<IActionResult> Atualizar([FromBody]Pessoa pessoa) 
        {
            var atualizarFila = new AtualizarFila();
            var message = JsonSerializer.Serialize(pessoa);
            atualizarFila.AtualizarMensagem("Atualizar", message);

            return Task.FromResult<IActionResult>(Ok("Pessoa atualizada na fila com sucesso"));
        }
       
    }
}
