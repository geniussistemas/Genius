using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Genius.QRCode.Middleware.InterfaceAdapters.Controllers.DTO;

public class GetTicketResponse
{
    [JsonPropertyName("numeroTicket")]
    public string NumeroTicket { get; set; } = string.Empty;
    // TODO: Complementar
}
