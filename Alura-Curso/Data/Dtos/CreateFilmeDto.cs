using System.ComponentModel.DataAnnotations;

namespace Alura_Curso.Data.Dtos
{
    public class CreateFilmeDto
    {
       
          

            [Required(ErrorMessage = "Campo obrigatorio")]
            public string Titulo { get; set; }

            [Required(ErrorMessage = "Campo obrigatorio")]
            [StringLength(50, ErrorMessage = "Tamanho máximo")]
            public string Genero { get; set; }

            [Required(ErrorMessage = "Campo obrigatorio")]
            [MaxLength(100, ErrorMessage = "Tamanho máximo")]
            public string Descricao { get; set; }

        
    }
}
