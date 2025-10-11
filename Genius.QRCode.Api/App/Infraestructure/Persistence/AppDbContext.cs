using Genius.Infraestructure.Persistence;
using Genius.QRCode.Api.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Genius.QRCode.Api.App.Infraestructure.Persistence;

public class AppDbContext : CommonAppDbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}

