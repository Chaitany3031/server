using Microsoft.EntityFrameworkCore;

namespace server.Data
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions options):base(options)
        {

        }
    }
}
