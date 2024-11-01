using AppSaresp_2024.Models;

namespace AppSaresp_2024.Repository.Contract
{
    public interface IProfessorRepository
    {
        //CRUD

        IEnumerable<Professor> ObterTodosProfessores();

        void Cadastrar(Professor professor);
        void Atualizar(Professor professor);

        Professor ObterProfessor(int id);
        void Excluir(int id);
    }
}