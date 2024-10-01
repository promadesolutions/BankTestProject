using Microsoft.EntityFrameworkCore;
using RamaBankProj.Model;
using RamaBankProj.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<BankDbContext>(options => options.UseInMemoryDatabase(databaseName: "BankTestDb"));

// Register AccountServices
builder.Services.AddScoped<IBankAccountService, BankAccountService>();
builder.Services.AddScoped<ICurrentAccountService, CurrentAccountService>();
builder.Services.AddScoped<ISavingsAccountService, SavingsAccountService>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bank API V1"));
}

app.UseRouting();
app.UseHttpsRedirection();

// Use CORS policy
app.UseCors("AllowAll");
app.MapControllers();
app.UseAuthorization();

app.Run();
