using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using web_MVC.Data.Repositorio;
using web_MVC.Data.Repositorio.Interface;
using web_MVC.Models;

namespace web_MVC.Controllers
{
    public class AlunosController : Controller
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IConfiguration _configuration;
        public AlunosController(IAlunoRepositorio alunoRepositorio, IConfiguration configuracion) 
        {
            _alunoRepositorio = alunoRepositorio;
            _configuration = configuracion;
        }

        public IActionResult Index()
        {
            try
            {
                var alunos = _alunoRepositorio.BuscarAlunos();
                return View(alunos);
            }
            catch (System.Exception)
            {
                TempData["MensagemErro"] = "Cuidado, Erro na conexão de dados!";
                return View();
            }
            
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            try
            {
                var aluno = _alunoRepositorio.BuscarId(id);
                return View("Editar", aluno);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public IActionResult ExcluirAluno(AlunoModel alunos)
        {
            _alunoRepositorio.ExcluirAluno(alunos);
            return RedirectToAction("Index");
        }

        public IActionResult Excluir(AlunoModel alunos)
        {
            try
            {
                _alunoRepositorio.ExcluirAluno(alunos);

                TempData["MensagemSucesso"] = "Aluno Excluido Com Sucesso!";

                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {

                throw;
            }
           
        }

        public IActionResult InserirAluno(AlunoModel aluno)
        {
            try
            {
                _alunoRepositorio.InserirAluno(aluno);

                TempData["MensagemSucesso"] = "Aluno Adicionado Com Sucesso!";

                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public IActionResult EditarAluno(AlunoModel aluno)
        {
            TempData["MensagemSucesso"] = "Aluno Editado Com Sucesso!";
            _alunoRepositorio.EditarAluno(aluno);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BuscarEndereco(string cep)
        {
            try
            {
                cep = cep.Replace("-", "");

                EnderecoModel enderecoModel = new();

                using var client = new HttpClient();

                var result = await client.GetAsync(_configuration.GetSection("ApiCep")["BaseUrl"] + cep + "/json");

                if (result.IsSuccessStatusCode)
                {
                    enderecoModel = JsonSerializer.Deserialize<EnderecoModel>(
                        await result.Content.ReadAsStringAsync(), new JsonSerializerOptions() { });
                }
                return View("Endereco", enderecoModel);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
