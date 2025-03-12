using System.Threading.Channels;
using API_Consumidor.Repository;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace API_Consumidor.Controller
{
    public class PessoasController : ControllerBase
    {

        private readonly PessoasRepository _pessoaRepository;

        public PessoasController(IConfiguration configuration)
        {
            _pessoaRepository = new PessoasRepository(configuration);
        }


        [HttpGet("pessoa/{nome}")]
        public async Task<IActionResult> BuscarPessoa(string nome)
        {
            var pessoas = await _pessoaRepository.BuscarPorNome(nome);

            if (pessoas != null)
            {
                return Ok(pessoas);
            }
            else
            {
                return NotFound("Pessoa não encontrada");
            }
             
        }
      
    }
}

