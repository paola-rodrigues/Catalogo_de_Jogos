using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace APICatalogoJogos.Controllers.V1
{
    [Route("api/V1[controller]")]
    [ApiController]
    public class JogosController : ControllerBase 
    {
        private readonly InterfaseJogoService _jogoService;

        public JogosController(InterfaseJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromRoute, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1,50)] int quantidade = 5)
        {
            var jogos = await _jogoService.Obter(pagina, quantidade);

            if(jogos.Count() == 0)            
                return NoContent();
            
            return Ok(jogos);
        }

    [HttpGet("idJogo:guid")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo)
        {
            var jogo = await _jogoService.Obter(idJogo);

            if (jogo == null)
                return NoContent();

            return Ok(jogo);
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = await _jogoService.Inserir(jogoInputModel);
                return Ok(jogo);
            }
            catch (JogoJaCadastradoException ex)
            {
                return UnprocessableEntity("Jogo já cadastrado para esta produtora");
            }          
        }

        // Atualização de todo o recuros
    [HttpPut("idJogo:guid")]
        public async Task<ActionResult>AtualizarJogo([FromRoute] Guid idJogo, [FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, jogoInputModel);
            }
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Jogo não cadastrado");
            }
            return Ok();
        }

        // Atualização de um item especifico do recurso
    [HttpPatch("idjogo:guid/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _jogoService.Atulizar(idJogo, preco);

                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Jogo não cadastrado");
            }
           
        }
    [HttpDelete("idJogo:guid")]
    public async Task<ActionResult>ApagarJogo([FromRoute]Guid idJogo)
        {
            try
            {
                await _jogoService.Renover(idJogo);

                return Ok();
            }
            catch (JogoNaoCadastradoException ex)
            {
                return NotFound("Jogo não cadastrado");
            }
        }

    }
}
