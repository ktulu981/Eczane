using Eczane.Core.Repositories;
using Eczane.Data;
using Eczane.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows.Forms;

namespace Eczane
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool ok;
            object m = new System.Threading.Mutex(true, "Eczane", out ok);

            if (!ok)
            {
               
                MessageBox.Show("Program zaten çalışıyor!");
                return;
            }

          
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<Form1>());
        }

        public static IServiceProvider ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddTransient<DataContext>();
                    services.AddTransient<SQLiteConfiguration>();
                    services.AddScoped<IILAC_AMBALAJRepository, ILAC_AMBALAJRepository>();
                    services.AddTransient<Form1>();
                });
        }
    }

   

}
