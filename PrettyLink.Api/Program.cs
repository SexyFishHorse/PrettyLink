namespace PrettyLink.Api
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    public static class Program
    {
        private static IWebHost BuildWebHost(string[] args)
        {
            return WebHost
                .CreateDefaultBuilder(args)
                .UseKestrel(opt => opt.AddServerHeader = false)
                .UseStartup<Startup>()
                .Build();
        }

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
    }
}
