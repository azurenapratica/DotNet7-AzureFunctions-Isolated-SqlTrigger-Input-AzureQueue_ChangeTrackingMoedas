using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using FunctionAppMoedasMonitor.Models;

namespace FunctionAppMoedasMonitor;

public class HistoricoCotacoes
{
    private readonly ILogger _logger;

    public HistoricoCotacoes(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<HistoricoCotacoes>();
    }

    [Function(nameof(HistoricoCotacoes))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
        [SqlInput(commandText: "SELECT * FROM dbo.Cotacoes WHERE Excluir = 0 ORDER BY Id DESC",
            connectionStringSetting: "BaseMoedas")] IEnumerable<HistoricoCotacao> historicos)
    {
        _logger.LogInformation("Consultando historico de cotacoes cadastradas...");
        _logger.LogInformation($"Numero de cotacoes ativas = {historicos.Count()}");

        var response = req.CreateResponse();
        response.StatusCode = HttpStatusCode.OK;
        await response.WriteAsJsonAsync(historicos);
        return response;
    }
}
