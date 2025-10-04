using Genius.Api.InterfaceAdapters.Controllers.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata.Ecma335;
using Genius.Common.Api.DTO;
using System.Runtime.CompilerServices;
using Genius.Infraestructure.Frameworks.Logging;
using Genius.Common.Abstractions;

namespace Genius.Api.InterfaceAdapters.Controllers;

public class TicketsController(ILoggerAdapter log) : ITicketsController
{
    public async Task<Response<GetTicketResponse?>> GetTicketByNumeroTicket(string numeroTicket)
    {
        GetTicketResponse? response = null!;

        log.Information($"Consulta do ticket `{numeroTicket}`");

        try
        {
            // TODO: Chamar UseCase para pesquisar ticket

            // TODO: Retirar código "mock". Os dados devem vir do UseCase
            response = await Task.Run(() =>
            {
                var responseData = new GetTicketResponse() { NumeroTicket = numeroTicket };
                return responseData;
            });


        }
        catch (Exception ex)
        {
            log.Error(ex, $"Erro ao efetuar consulta do ticket `{numeroTicket}`");
            response = null;
            // return new Response<GetTicketResponse?>(null, StatusCodes.Status500InternalServerError, "Erro ao efetuar consulta do ticket");
        }

        return response is not null
            ? new Response<GetTicketResponse?>(response, StatusCodes.Status200OK, "Consulta do ticket efetuada com sucesso")
            : new Response<GetTicketResponse?>(null, StatusCodes.Status500InternalServerError, "Erro ao efetuar consulta do ticket");
    }

}