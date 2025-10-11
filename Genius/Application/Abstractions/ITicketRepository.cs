using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genius.Domain.Entities;

namespace Application.Abstractions;

public interface ITicketRepository
{
    Task<Ticket?> GetByIdAsync();    
}