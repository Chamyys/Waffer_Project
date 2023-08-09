using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatHubSpace
{
   
        public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Receive", message);
        }
    }
/*
        public static IWebHostBuilder CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .ConfigureKestrel(options =>
                {
                    var cert = new X509Certificate2("/Certificates/certificate.pfx", "1203200121");
                    options.ListenAnyIP(7001, listenOptions =>
                    {
                        listenOptions.UseHttps(new HttpsConnectionAdapterOptions
                        {
                            ServerCertificate = cert
                        });
                    });
                });
    }

*/







    }

