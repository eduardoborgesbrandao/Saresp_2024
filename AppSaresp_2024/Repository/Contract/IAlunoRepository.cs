using AppSaresp_2024.Models;

namespace AppSaresp_2024.Repository.Contract
{
    public interface IAlunoRepository
    {
        //CRUD

        IEnumerable<Aluno> ObterTodosAlunos();

        void Cadastrar(Aluno aluno);
        void Atualizar(Aluno aluno);

        Aluno ObterAluno(int id);
        void Excluir(int id);
    }
}
