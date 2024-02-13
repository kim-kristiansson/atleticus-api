using Microsoft.EntityFrameworkCore;

namespace AtleticusAPI.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :DbContext(options)
    {
    }
}
