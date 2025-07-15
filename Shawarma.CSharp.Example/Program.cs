using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Shawarma.CSharp.Example;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.ConfigureKestrel(options => options.AllowSynchronousIO = true);
        webBuilder.UseStartup<Startup>();
    })
    .Build()
    .Run();
