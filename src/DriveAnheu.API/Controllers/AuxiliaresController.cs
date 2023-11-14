using DriveAnheu.Infrastructure.Factory.ConnectionFactory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveAnheu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuxiliaresController(IConnectionFactory _connectionFactory) : Controller
    {
        [HttpGet("obterStatusBanco")]
        [AllowAnonymous]
        public async Task<ActionResult<(bool, string)>> ObterStatusBanco(CancellationToken cancellationToken = default)
        {
            bool isOk = true;
            string desc = string.Empty;

            try
            {
                var connection = _connectionFactory.ObterMySqlConnection();

                await connection.OpenAsync(cancellationToken);
                return Ok(new { isOk, desc });
            }
            catch (Exception ex)
            {
                isOk = false;
                return Ok(new { isOk, ex.Message });
            }
        }
    }
}