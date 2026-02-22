
using ControleGastos.Data;
using ControleGastos.Interfaces;
using ControleGastos.Repositories;
using ControleGastos.Services;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)
                ));

            // Injeção de dependências 
            builder.Services.AddScoped<PessoaService>();
            builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();

            builder.Services.AddScoped<TransacaoService>();
            builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();

            builder.Services.AddScoped<CategoriaService>();
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
