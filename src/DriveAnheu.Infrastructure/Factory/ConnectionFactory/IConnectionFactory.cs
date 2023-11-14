using MySqlConnector;

namespace DriveAnheu.Infrastructure.Factory.ConnectionFactory
{
    public interface IConnectionFactory
    {
        MySqlConnection ObterMySqlConnection();
        string ObterStringConnection();
    }
}