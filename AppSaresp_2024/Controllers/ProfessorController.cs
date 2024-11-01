using AppSaresp_2024.Models;
using AppSaresp_2024.Repository;
using AppSaresp_2024.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace AppSaresp_2024.Controllers
{

    public class ProfessorController : Controller
    {
        private IProfessorRepository _professorRepository;
        public ProfessorController(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public IActionResult Index()
        {
            return View(_professorRepository.ObterTodosProfessores());
        }
        [HttpGet]
        public IActionResult CadastrarProfessor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastrarProfessor(Professor professor)
        {
            if (ModelState.IsValid)
            {
                _professorRepository.Cadastrar(professor);
            }
            return View();
        }
        [HttpGet]
        public IActionResult AtualizarProfessor(int id)
        {
            return View(_professorRepository.ObterProfessor(id));
        }
        [HttpPost]
        public IActionResult AtualizarProfessor(Professor professor)
        {
            _professorRepository.Atualizar(professor);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult DetalhesProfessor(int id)
        {
            return View(_professorRepository.ObterProfessor(id));
        }
        [HttpPost]
        public IActionResult DetalhesProfessor(Professor professor)
        {
            _professorRepository.Atualizar(professor);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult ExcluirProfessor(int id)
        {
            _professorRepository.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}