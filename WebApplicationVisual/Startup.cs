using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WebApplicationVisual;

public class Startup
{
    public IConfigurationRoot _confString;

    public Startup(IHostingEnvironment hostingEnvironment)
    {
        _confString = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath)
            .AddJsonFile("dbsettings.json").Build();
    }

    public Startup()
    {
      
    }
}