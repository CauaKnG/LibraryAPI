using Dapper;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Repositories.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data;


namespace LibraryAPI.Infrastructure.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly string _connectionString;

        public LivroRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Library") ?? throw new ArgumentNullException(nameof(configuration));
        }

        private IDbConnection Connection => new SqliteConnection(_connectionString);

        public async Task<IEnumerable<Livro>> GetAllAsync()
        {
            using (var dbConnection = Connection)
            {
                const string query = "SELECT * FROM Livro";
                return await dbConnection.QueryAsync<Livro>(query);
            }
        }

        public async Task<Livro> GetByIdAsync(int id)
        {
            using (var dbConnection = Connection)
            {
                const string query = "SELECT * FROM Livro WHERE Id = @Id";
                var livro = await dbConnection.QuerySingleOrDefaultAsync<Livro>(query, new { Id = id });
                return livro ?? throw new InvalidOperationException("Livro não encontrado");
            }
        }

        public async Task AddAsync(Livro livro)
        {
            using (var dbConnection = Connection)
            {
                const string query = "INSERT INTO Livro (Titulo, Autor, Editora, Genero, Disponivel) VALUES (@Titulo, @Autor, @Editora, @Genero, @Disponivel)";
                await dbConnection.ExecuteAsync(query, livro);
            }
        }

        public async Task UpdateAsync(Livro livro)
        {
            using (var dbConnection = Connection)
            {
                const string query = "UPDATE Livro SET Titulo = @Titulo, Autor = @Autor, Editora = @Editora, Genero = @Genero, Disponivel = @Disponivel WHERE Id = @Id";
                await dbConnection.ExecuteAsync(query, livro);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var dbConnection = Connection)
            {
                const string query = "DELETE FROM Livro WHERE Id = @Id";
                await dbConnection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
