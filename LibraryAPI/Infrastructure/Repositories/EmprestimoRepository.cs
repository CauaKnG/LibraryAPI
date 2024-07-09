using Dapper;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Repositories.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data;

namespace LibraryAPI.Infrastructure.Repositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly string _connectionString;

        public EmprestimoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Library") ?? throw new ArgumentNullException(nameof(configuration));
        }

        private IDbConnection Connection => new SqliteConnection(_connectionString);

        public async Task<IEnumerable<Emprestimo>> GetAllAsync()
        {
            using (var dbConnection = Connection)
            {
                const string query = "SELECT * FROM Emprestimo";
                return await dbConnection.QueryAsync<Emprestimo>(query);
            }
        }

        public async Task<Emprestimo?> GetByIdAsync(int id)
        {
            using (var dbConnection = Connection)
            {
                const string query = "SELECT * FROM Emprestimo WHERE Id = @Id";
                var emprestimo = await dbConnection.QuerySingleOrDefaultAsync<Emprestimo>(query, new { Id = id });
                return emprestimo;
            }
        }

        public async Task AddAsync(Emprestimo emprestimo)
        {
            using (var dbConnection = Connection)
            {
                const string query = "INSERT INTO Emprestimo (LivroId, UsuarioId, DataEmprestimo, DataDevolucao) VALUES (@LivroId, @UsuarioId, @DataEmprestimo, @DataDevolucao)";
                await dbConnection.ExecuteAsync(query, emprestimo);
            }
        }

        public async Task UpdateAsync(Emprestimo emprestimo)
        {
            using (var dbConnection = Connection)
            {
                const string query = "UPDATE Emprestimo SET LivroId = @LivroId, UsuarioId = @UsuarioId, DataEmprestimo = @DataEmprestimo, DataDevolucao = @DataDevolucao WHERE Id = @Id";
                await dbConnection.ExecuteAsync(query, emprestimo);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var dbConnection = Connection)
            {
                const string query = "DELETE FROM Emprestimo WHERE Id = @Id";
                await dbConnection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
