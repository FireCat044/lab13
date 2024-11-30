using MailKit.Security;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Email;
using Serilog.Sinks.Seq;

using System.Net;

namespace lab13
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
             .WriteTo.Email(
                 from: "andreigorbunov044@gmail.com",  
                 to: "andreigorbunov077@gmail.com",   
                 host: "smtp.gmail.com",
                 port: 587,
                 connectionSecurity: SecureSocketOptions.StartTls,
                 credentials: new NetworkCredential("andreigorbunov044@gmail.com", "wdzkjarphowpprsp"),  
                 subject: "Critical Log Alert",
                 restrictedToMinimumLevel: LogEventLevel.Error
             )
             .WriteTo.Seq("http://localhost:5000")
             .Enrich.WithExceptionDetails()
             .CreateLogger();


            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseSerilogRequestLogging();

            app.Run();
        }
    }
}