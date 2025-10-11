using Genius.Infraestructure.Persistence;
using Genius.QRCode.Middleware.Domain.Models;
using Genius.QRCode.Middleware.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Genius.QRCode.Middleware.Infraestructure.Persistence;

public class AppDbContext : CommonAppDbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
