using Microsoft.EntityFrameworkCore;
using Alura_Curso.Models;

namespace Alura_Curso.Data;

public class FilmeContext : DbContext
{

    public FilmeContext(DbContextOptions<FilmeContext> options):base(options)
    { 
    }

    public DbSet<Filmes> Filme { get; set; }  


}

