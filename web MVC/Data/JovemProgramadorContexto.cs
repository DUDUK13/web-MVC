using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_MVC.Data.Mapeamento;
using web_MVC.Models;

namespace web_MVC.Data
{
    public class JovemProgramadorContexto: DbContext
    {
        public JovemProgramadorContexto(DbContextOptions<JovemProgramadorContexto>options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)

        {
            modelbuilder.ApplyConfiguration(new AlunoMapping());
        }

        public DbSet<AlunoModel> Aluno { get; set; }
    }
}
