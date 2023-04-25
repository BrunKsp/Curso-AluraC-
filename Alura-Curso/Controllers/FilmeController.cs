using Microsoft.AspNetCore.Mvc;
using Alura_Curso.Models;
using Alura_Curso.Data;
using Alura_Curso.Data.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace Alura_Curso.Controllers
{

    [ApiController]
    [Route("Filme/controller")]
    public class FilmeController : Controller
    {
        //private static List<Filmes> filmes = new List<Filmes>();
        //private static int id = 0;

        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
           
        }

        /// <summary>
        /// Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="filmedto">Objeto com os campos necessários para criação de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>


        [HttpPost]
        public IActionResult Adicionar ([FromBody]CreateFilmeDto filmedto)
        {
            Filmes filme = _mapper.Map<Filmes>(filmedto);
            _context.Filme.Add(filme);
            _context.SaveChanges(); 
            return CreatedAtAction(nameof(BuscaFilmeID), new  {id = filme.Id }, filme );
        }



        /// <summary>
        /// Lista Todos Os Filmes do Banco
        /// </summary>
        ///  <param>Digite um valor de para pular ou deixe vazio</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a busca seja feita com sucesso</response>



        [HttpGet]

        public IEnumerable<Filmes> ListarFilme([FromQuery]int skip = 0, [FromQuery] int take = 10)
        {
            
            //pula 50 primeiros  e pega os 10 proximos 
            return _context.Filme.Skip(skip).Take(take);
        }
        // busca filme pelo id 

        /// <summary>
        /// Lista um  filmes especifico do banco
        /// </summary>
        /// <param name="id">Objeto  necessário para a busca de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a busca seja feita com sucesso</response>


        [HttpGet("{id}")]
        public IActionResult BuscaFilmeID (int id)
        {
            
            var filme = _context.Filme.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound("Não encontrado");
            return Ok(filme);
        }



        /// <summary>
        /// Edição de um  filme especifico do banco
        /// </summary>
        /// <param name="id">Objeto  necessário para a busca de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a busca seja feita com sucesso</response>


        [HttpPut ("{id}")]

        public IActionResult EditFilme (int id, [FromBody] UpdateFilmeDto filmeDto )
        {
            var filme = _context.Filme.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound("Não encontrado");
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
            return NoContent();


        }

        /// <summary>
        /// Edição de um  filme especifico do banco
        /// </summary>
        /// <param name="id">Objeto  necessário para a busca de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a busca seja feita com sucesso</response>



        [HttpPatch("{id}")]

        public IActionResult AttFilme(int id, JsonPatchDocument<UpdateFilmeDto>patch )
        {
            var filme = _context.Filme.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound("Não encontrado");

            var attfilme = _mapper.Map<UpdateFilmeDto>(filme);
            patch.ApplyTo(attfilme, ModelState);
            if (!TryValidateModel(attfilme))
                return ValidationProblem(ModelState);

            
            _mapper.Map(attfilme, filme);
            _context.SaveChanges();
            return NoContent();


        }



        /// <summary>
        /// Deletar um  filme especifico do banco
        /// </summary>
        /// <param name="id">Objeto  necessário para a exclusçao de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a busca seja feita com sucesso</response>



        [HttpDelete ("{id}")]

        public IActionResult DeleteFilme (int id)
        {
            var filme = _context.Filme.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound("Não encontrado");
            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();

        }
    
    }




}
