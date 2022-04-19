using Microsoft.EntityFrameworkCore;
using Northwind.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NorthwindContext>(
		option => option
        .UseSqlServer(builder.Configuration.GetConnectionString("NorthwindConnectionString"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("data", async (NorthwindContext db) =>
{
	var sampleData = await db.Products
    .Take(100)
    .ToListAsync();

	return sampleData;
});

app.Run();
