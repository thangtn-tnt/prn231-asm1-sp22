using DataAccess.Dto;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    // Static details
    public static class SD
    {
        public static string SecretKey
        {
            get
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfiguration config = builder.Build();
                return config.GetSection("ApiSettings")["Secret"];
            }
        }

        public static LoginRequestDTO DefaultAccount
        {
            get
            {
                var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                var configuration = builder.Build();

                var defaultAccount = new
                {
                    Email = configuration.GetSection("DefaultAccount")["Email"],
                    Password = configuration.GetSection("DefaultAccount")["Password"]
                };

                return new LoginRequestDTO { Email = defaultAccount.Email, Password = defaultAccount.Password };
            }
        }

    }
}
