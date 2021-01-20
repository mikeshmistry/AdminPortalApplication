using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(AdminPortal.UI.Areas.Identity.IdentityHostingStartup))]
namespace AdminPortal.UI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}