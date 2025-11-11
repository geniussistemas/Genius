using Genius.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Genius.Api.Infraestructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : CommonAppDbContext(options) { }
