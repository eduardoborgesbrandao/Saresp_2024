using AppSaresp_2024.Models;
using AppSaresp_2024.Repository.Contract;
using MySql.Data.MySqlClient;
using System.Data;

namespace AppSaresp_2024.Repository
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly string _conexaoMySQL;

        public ProfessorRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }
        public void Atualizar(Professor professor)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("call sp_updateProfessor(@IdProfessor, @nome, @cpf, @rg, @telefone, @data_nasc)", conexao);

                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = professor.nome;
                cmd.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = professor.cpf;
                cmd.Parameters.Add("@rg", MySqlDbType.VarChar).Value = professor.rg;
                cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = professor.telefone;
                cmd.Parameters.Add("@data_nasc", MySqlDbType.VarChar).Value = professor.data_nasc.ToString("yyyy/MM/dd");
                cmd.Parameters.Add("@IdProfessor", MySqlDbType.VarChar).Value = professor.IdProfessor;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Cadastrar(Professor professor)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("call sp_insertProfessor(@nome, @cpf, @rg, @telefone, @data_nasc)", conexao);
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = professor.nome;
                cmd.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = professor.cpf;
                cmd.Parameters.Add("@rg", MySqlDbType.VarChar).Value = professor.rg;
                cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = professor.telefone;
                cmd.Parameters.Add("@data_nasc", MySqlDbType.VarChar).Value = professor.data_nasc.ToString("yyyy/MM/dd");

                cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("call sp_deleteProfessor(@IdProfessor)", conexao);
                cmd.Parameters.AddWithValue("@IdProfessor", id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public IEnumerable<Professor> ObterTodosProfessores()
        {
            List<Professor> ProfessorList = new List<Professor>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbProfessorAplicador", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Clone();

                foreach (DataRow dr in dt.Rows)
                {
                    ProfessorList.Add(
                        new Professor
                        {
                            IdProfessor = Convert.ToInt32(dr["IdProfessor"]),
                            nome = Convert.ToString(dr["nome"]),
                            cpf = Convert.ToDecimal(dr["cpf"]),
                            rg = Convert.ToString(dr["rg"]),
                            telefone = Convert.ToDecimal(dr["telefone"]),
                            data_nasc = Convert.ToDateTime(dr["data_nasc"])
                        });
                }
                return ProfessorList;
            }
        }

        public Professor ObterProfessor(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from tbProfessorAplicador " +
                                                    "where IdProfessor = @IdProfessor", conexao);
                cmd.Parameters.AddWithValue("@IdProfessor", id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Professor professor = new Professor();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    professor.IdProfessor = Convert.ToInt32(dr["IdProfessor"]);
                    professor.nome = (string)(dr["nome"]);
                    professor.cpf = Convert.ToDecimal(dr["cpf"]);
                    professor.rg = Convert.ToString(dr["rg"]);
                    professor.telefone = Convert.ToDecimal(dr["telefone"]);
                    professor.data_nasc = Convert.ToDateTime(dr["data_nasc"]);
                }
                return professor;
            }
        }
    }
}
