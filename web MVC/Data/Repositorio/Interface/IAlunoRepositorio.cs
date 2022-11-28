using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using web_MVC.Models;

namespace web_MVC.Data.Repositorio.Interface
{
    public interface IAlunoRepositorio
    {
        void InserirAluno(AlunoModel alunos);
        List<AlunoModel> BuscarAlunos();
        AlunoModel BuscarId(int id);
        void ExcluirAluno(AlunoModel alunos);
        void EditarAluno(AlunoModel alunos);
    }
}
