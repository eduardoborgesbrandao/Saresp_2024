using AppSaresp_2024.Models;
using AppSaresp_2024.Repository.Contract;
using System.Data;
using MySql.Data.MySqlClient;


namespace AppSaresp_2024.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly string _conexaoMySQL;

        public AlunoRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
        public void Atualizar(Aluno aluno)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("call sp_updateAluno(@idAluno, @nome, @email, @telefone, @serie, @turma, @data_nasc)", conexao);
                cmd.Parameters.Add("@IdAluno", MySqlDbType.VarChar).Value = aluno.IdAluno;
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = aluno.NomeAluno;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = aluno.Email;
                cmd.Parameters.Add("@data_nasc", MySqlDbType.VarChar).Value = aluno.DataNascimentoAluno.ToString("yyyy/MM/dd");
                cmd.Parameters.Add("@turma", MySqlDbType.VarChar).Value = aluno.Turma;
                cmd.Parameters.Add("@serie", MySqlDbType.VarChar).Value = aluno.Serie;
                cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = aluno.TelefoneAluno;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Cadastrar(Aluno aluno)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("call sp_insertAluno(@nome, @email, @telefone, @serie, @turma, @data_nasc)", conexao);
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = aluno.NomeAluno;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = aluno.Email;
                cmd.Parameters.Add("@data_nasc", MySqlDbType.VarChar).Value = aluno.DataNascimentoAluno.ToString("yyyy/MM/dd");
                cmd.Parameters.Add("@turma", MySqlDbType.VarChar).Value = aluno.Turma;
                cmd.Parameters.Add("@serie", MySqlDbType.VarChar).Value = aluno.Serie;
                cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = aluno.TelefoneAluno;

                cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("call sp_deleteAluno(@IdAluno)", conexao);
                cmd.Parameters.AddWithValue("@IdAluno", id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public IEnumerable<Aluno> ObterTodosAlunos()
        {
            List<Aluno> AlunoList = new List<Aluno>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbAluno", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Clone();

                foreach (DataRow dr in dt.Rows)
                {
                    AlunoList.Add(
                        new Aluno
                        {
                            IdAluno = Convert.ToInt32(dr["IdAluno"]),
                            NomeAluno = Convert.ToString(dr["nome"]),
                            Email = Convert.ToString(dr["email"]),
                            Turma = Convert.ToString(dr["turma"]),
                            Serie = Convert.ToString(dr["serie"]),
                            DataNascimentoAluno = Convert.ToDateTime(dr["data_nasc"]),
                            TelefoneAluno = Convert.ToDecimal(dr["telefone"]),
                        });
                }
                return AlunoList;
            }
        }

        public Aluno ObterAluno(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from tbAluno " +
                                                    "where IdAluno = @IdAluno", conexao);
                cmd.Parameters.AddWithValue("@IdAluno", id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Aluno aluno = new Aluno();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    aluno.IdAluno = Convert.ToInt32(dr["IdAluno"]);
                    aluno.NomeAluno = Convert.ToString(dr["nome"]);
                    aluno.Email = Convert.ToString(dr["email"]);
                    aluno.Turma = Convert.ToString(dr["turma"]);
                    aluno.Serie = Convert.ToString(dr["serie"]);
                    aluno.DataNascimentoAluno = Convert.ToDateTime(dr["data_nasc"]);
                    aluno.TelefoneAluno = Convert.ToDecimal(dr["telefone"]);
                }
                return aluno;
            }
        }
    }
}