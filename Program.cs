using Microsoft.EntityFrameworkCore;
using ServerCsharpPool.Account;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseSqlServer(connection));
var app = builder.Build();

app.MapGet("/getOperator/{*nickname}", async (ApplicationContext db, string nickname) => {
    Console.WriteLine($"\n\n\n{nickname}\n\n\n");
    return JsonConvert.SerializeObject(await db.Operators.FirstOrDefaultAsync(
        o => o.Nickname == nickname));
});

app.MapPost("/sendOprtr", async (ApplicationContext db, Operator oprtr) => {
    Console.WriteLine($"\n\n\n{oprtr.Nickname}\n\n\n");
    await db.Operators.AddAsync(oprtr);
    await db.SaveChangesAsync();
    return "done";
});

app.Run();


public class ApplicationContext : DbContext {
    public DbSet<Operator> Operators { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Operator>().HasData(
            new Operator { Id = 1, Nickname = "bek", Password = "123" });
    }
}
