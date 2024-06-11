using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelloworldWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            try
            {
                var registryURL = Environment.GetEnvironmentVariable("DOCKER_REGISTRY") ?? "N/A";
                Console.WriteLine($"DOCKER_REGISTRY eq {registryURL}");
                ViewData["REGISTRYURL"] = registryURL;
                if (registryURL != $"{registryURL}.azurecr.io")
                {
                    var hostEntry = await System.Net.Dns.GetHostEntryAsync(registryURL);
                    ViewData["HOSTENTRY"] = hostEntry.HostName;

                    string region = hostEntry.HostName.Split('.')[1];
                    ViewData["REGION"] = region;

                    var registryIp = System.Net.Dns.GetHostAddresses(registryURL)[0].ToString();
                    ViewData["REGISTRYIP"] = registryIp;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
