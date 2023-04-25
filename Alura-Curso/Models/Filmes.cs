using System.ComponentModel.DataAnnotations;

namespace Alura_Curso.Models;

public class Filmes
{
    [Required]
    [Key]
    public int Id { get; set; }
    
    [Required (ErrorMessage ="Campo obrigatorio")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "Campo obrigatorio")]
    [MaxLength(50, ErrorMessage = "Tamanho máximo")]
    public string Genero { get; set; }
    
    [Required(ErrorMessage = "Campo obrigatorio")]
    [MaxLength(100, ErrorMessage ="Tamanho máximo")]
    public string Descricao { get; set; }
    
}

