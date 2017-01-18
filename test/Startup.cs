using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using test.Entities;
using Microsoft.EntityFrameworkCore;
using test.Repositories;

namespace test
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            /* var connectionString = @"Server=tcp:shdutt.database.windows.net;Database=ExpenseDB;
 User ID=shrutidutt@[shdutt];Password=!Zxcvb12345;Trusted_Connection=True;
 Encrypt=True;  ";

     */

            var connectionString = @"Data Source=shdutt1;Initial Catalog=ExpenseDB1;Integrated Security=True;MultipleActiveResultSets=True";
            services.AddDbContext<ExpenseInfo>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IExpenseReportRepository, ExpenseReportRepository>();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Expense, controllers.ExpenseReportDto>();
                cfg.CreateMap<controllers.TransactionReportCreationDto, Entities.Transaction>();
                cfg.CreateMap<Entities.Transaction, controllers.TransactionReportCreationDto>();
                cfg.CreateMap<Entities.Transaction, controllers.TransactionReportDto>();
                cfg.CreateMap<controllers.TransactionReportDto, Entities.Transaction>();
                cfg.CreateMap<controllers.TransactionReportCreationDto, Entities.Transaction>();
                cfg.CreateMap<controllers.ExpenseReportCreationDto, Entities.Expense>();
                cfg.CreateMap<Entities.Expense, controllers.ExpenseReportUpdateDto>();
                cfg.CreateMap<controllers.ExpenseReportUpdateDto, Entities.Expense>();
                cfg.CreateMap<controllers.ExpenseReportDto, Entities.Expense>();

            });
            app.UseCors("MyPolicy");
            app.UseMvc();


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
