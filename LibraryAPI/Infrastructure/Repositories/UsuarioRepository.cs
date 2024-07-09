using Dapper;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Repositories.Interfaces;
using Microsoft.Data.Sqlite;
using System.Data;

namespace LibraryAPI.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Library") ?? throw new ArgumentNullException(nameof(configuration));
        }

        private IDbConnection Connection => new SqliteConnection(_connectionString);

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            using (var dbConnection = Connection)
            {
                const string query = "SELECT * FROM Usuario";
                return await dbConnection.QueryAsync<Usuario>(query);
            }
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            using (var dbConnection = Connection)
            {
                const string query = "SELECT * FROM Usuario WHERE Id = @Id";
                var usuario = await dbConnection.QuerySingleOrDefaultAsync<Usuario>(query, new { Id = id });
                return usuario ?? throw new InvalidOperationException("Usuario não encontrado");
            }
        }

        public async Task AddAsync(Usuario usuario)
        {
            using (var dbConnection = Connection)
            {
                const string query = "INSERT INTO Usuario (Nome, Telefone, Endereco, CPF) VALUES (@Nome, @Telefone, @Endereco, @CPF)";
                await dbConnection.ExecuteAsync(query, usuario);
            }
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            using (var dbConnection = Connection)
            {
                const string query = "UPDATE Usuario SET Nome = @Nome, Telefone = @Telefone, Endereco = @Endereco, CPF = @CPF WHERE Id = @Id";
                await dbConnection.ExecuteAsync(query, usuario);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var dbConnection = Connection)
            {
                const string query = "DELETE FROM Usuario WHERE Id = @Id";
                await dbConnection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}

