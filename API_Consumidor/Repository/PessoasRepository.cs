using System.Data.SqlClient;
using API_Publicador.Domain;
using API_Publicador.Model.Dto;
using Dapper;

namespace API_Consumidor.Repository
{
    public class PessoasRepository
    {

        private readonly string _connectionString;

        public PessoasRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("API_Consumidor");
        }

        public void AtualizarPessoaBanco(Pessoa pessoa)
        {
            using var conexao = new SqlConnection(_connectionString);

            string query = @"UPDATE Pessoas
                SET Nome = @Nome, CPF = @Cpf, Sexo = @Sexo, DataNascimento = @DataNascimento 
                WHERE Cpf = @Cpf";

            conexao.Execute(query, new {pessoa.Nome, pessoa.Cpf, pessoa.Sexo, pessoa.DataNascimento});
        }

        public void DeletarPessoaBanco(Pessoa pessoa)
        {
            using var conexao = new SqlConnection(_connectionString);
            string query = @"DELETE FROM Pessoas
                            Where Cpf =  @Cpf";
            conexao.Execute(query, new { pessoa.Cpf});

        }

        public void InserirPessoaBanco(PessoaDto pessoa)
        {
            using var conexao = new SqlConnection (_connectionString);

            string queryEndereco = @"
                INSERT INTO 
                Endereco
                (Cep, Logradouro, Bairro, Localidade, UF)
                output inserted.IdEndereco
                VALUES 
                (@Cep, @Logradouro, @Bairro, @Localidade, @UF)";
            
            conexao.Execute(queryEndereco, new
            {   
                Cep = pessoa.Endereco.Cep,
                Logradouro = pessoa.Endereco.Logradouro,
                Bairro = pessoa.Endereco.Bairro, 
                Localidade = pessoa.Endereco.Localidade, 
                UF = pessoa.Endereco.Uf,
            
            });

            var idEndereco = conexao.ExecuteScalar<int>(queryEndereco, pessoa.Endereco);

            string queryPessoa = @"INSERT INTO 
                     Pessoas
                    (Nome, CPF, RG, DataNascimento, Email, Telefone, Sexo, IdEndereco) 
                    VALUES
                    (@Nome, @CPF, @Rg, @DataNascimento, @Email, @Telefone, @Sexo, @IdEndereco)";

            conexao.Execute(queryPessoa, new
            {
                Nome = pessoa.Nome,
                CPF = pessoa.Cpf,
                RG = pessoa.Rg,
                DataNascimento = pessoa.DataNascimento,
                Email = pessoa.Email,
                Telefone = pessoa.Telefone,
                Sexo = pessoa.Sexo,
                idEndereco = idEndereco,

            });
        }
        public async Task<IEnumerable<Pessoa>> BuscarPorNome(string nome)
        { 
            using var conexao = new SqlConnection(_connectionString);

            string query = @"SELECT Nome, Cpf, Sexo, DataNascimento, Rg, Email, Telefone 
                            FROM dbo.Pessoas WHERE Nome Like @Nome";

            return await conexao.QueryAsync<Pessoa>(query, new { Nome = "%" + nome + "%"});
        }
    }
}
