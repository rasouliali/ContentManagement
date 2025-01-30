using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Persistence.Contexts;

public class CMContextInitialiser
{
    private readonly ILogger<CMContextInitialiser> _logger;
    private readonly CMContext _context;

    public CMContextInitialiser(ILogger<CMContextInitialiser> logger, CMContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    //public async Task SeedAsync()
    //{
    //    try
    //    {
    //        await TrySeedAsync();
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "An error occurred while seeding the database.");
    //        throw;
    //    }
    //}

    //public async Task TrySeedAsync()
    //{
    //}
}
