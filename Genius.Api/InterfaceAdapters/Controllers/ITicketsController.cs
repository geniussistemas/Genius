using Genius.Api.InterfaceAdapters.Controllers.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Genius.Common.Api.DTO;

namespace Genius.Api.InterfaceAdapters.Controllers;

public interface ITicketsController
{
    Task<Response<GetTicketResponse?>> GetTicketByNumeroTicket(string numeroTicket);
}
