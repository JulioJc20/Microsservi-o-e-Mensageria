using Microsoft.AspNetCore.Mvc;
using API_Publicador.Domain;
using RabbitMQ.Client;
using System.Threading.Channels;
using API_Publicador.Publicadora;
using System.Text.Json;
using API_Publicador.Services;
using System.Net.Http;

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
        public async Task<IActionResult> ChamarApi(string nome)
        {
            var resultado = await _apiService.ChamarApiConsumidora(nome);

            if (resultado.StartsWith("Erro"))
            {
                return StatusCode(500, resultado);
            }

            return Ok(resultado);
        }

        [HttpGet("Chamar_API_CEP")]
        public async Task<IActionResult> ChamarApiCEP(string cep)
        {
            var resultado = await _apiService.ChamarApiCep(cep);

            if (resultado.StartsWith("Error"))
            {
                return StatusCode(500, resultado);
            }
            return Ok(resultado);
        }

        [HttpPost("EnviarPessoas")] 
        public Task<IActionResult> Criar([FromBody] Pessoa pessoa)
        {
            var publicarFila = new PublicarFila();
            var message = JsonSerializer.Serialize(pessoa);
            publicarFila.PublicarMensagem("pessoa", message);

            return Task.FromResult<IActionResult>(Ok("Pessoa publicada na fila com sucesso"));
        }

        [HttpDelete("DeletarPessoas")]
        public Task<IActionResult> Deletar([FromBody] Pessoa pessoa )
        {
            var deletarFila = new DeletarFila();
            var message = JsonSerializer.Serialize(pessoa);
            deletarFila.DeletarMensagem("deletarPessoa", message);
            
         
             return Task.FromResult<IActionResult>(Ok("Pessoa deletada da fila com sucesso"));
           
        }
        [HttpPut("AtualizarPessoas")]
        public Task<IActionResult> Atualizar([FromBody]Pessoa pessoa)
        {
            var atualizarFila = new AtualizarFila();
            var message = JsonSerializer.Serialize(pessoa);
            atualizarFila.AtualizarMensagem("atualizarPessoa", message);

            return Task.FromResult<IActionResult>(Ok("Pessoa atualizada na fila com sucesso"));
        }
       
    }
}
