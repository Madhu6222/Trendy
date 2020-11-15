using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendy.Database;
using Trendy.Entities;

namespace Trendy.Services
{
    public class ConfigurationsService
    {
        public Config GetConfig(string Key) 
        {
            using (TrendyDbContext context = new TrendyDbContext()) 
            {
                return context.Configurations.Find(Key);
            }
        }
    }
}
