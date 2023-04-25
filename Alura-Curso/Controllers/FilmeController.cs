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

        [HttpPost]
        public IActionResult Adicionar ([FromBody]CreateFilmeDto filmedto)
        {
            Filmes filme = _mapper.Map<Filmes>(filmedto);
            _context.Filme.Add(filme);
            _context.SaveChanges(); 
            return CreatedAtAction(nameof(BuscaFilmeID), new  {id = filme.Id }, filme );
        }


        [HttpGet]

        public IEnumerable<Filmes> ListarFilme([FromQuery]int skip = 0, [FromQuery] int take = 10)
        {
            
            //pula 50 primeiros  e pega os 10 proximos 
            return _context.Filme.Skip(skip).Take(take);
        }
            // busca filme pelo id 
        [HttpGet("{id}")]
        public IActionResult BuscaFilmeID (int id)
        {
            
            var filme = _context.Filme.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound("Não encontrado");
            return Ok(filme);
        }



        [HttpPut ("{id}")]

        public IActionResult EditFilme (int id, [FromBody] UpdateFilmeDto filmeDto )
        {
            var filme = _context.Filme.FirstOrDefault(filme => filme.Id == id);
            if (filme == null) return NotFound("Não encontrado");
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
            return NoContent();


        }

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
