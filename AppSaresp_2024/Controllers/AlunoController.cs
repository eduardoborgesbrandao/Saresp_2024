using AppSaresp_2024.Models;
using AppSaresp_2024.Repository;
using AppSaresp_2024.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace AppSaresp_2024.Controllers
{

    public class AlunoController : Controller
    {
        private IAlunoRepository _alunoRepository;
        public AlunoController(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public IActionResult Index()
        {
            return View(_alunoRepository.ObterTodosAlunos());
        }
        [HttpGet]
        public IActionResult CadastrarAluno()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastrarAluno(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _alunoRepository.Cadastrar(aluno);
            }
            return View();
        }
        [HttpGet]
        public IActionResult AtualizarAluno(int id)
        {
            return View(_alunoRepository.ObterAluno(id));
        }
        [HttpPost]
        public IActionResult AtualizarAluno(Aluno aluno)
        {
            _alunoRepository.Atualizar(aluno);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult DetalhesAluno(int id)
        {
            return View(_alunoRepository.ObterAluno(id));
        }
        [HttpPost]
        public IActionResult DetalhesAluno(Aluno aluno)
        {
            _alunoRepository.Atualizar(aluno);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult ExcluirAluno(int id)
        {
            _alunoRepository.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}