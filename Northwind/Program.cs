using LinqToDB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Northwind.Entities;
using System.Linq.Expressions;

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

app.MapPut("update", async (NorthwindContext db) =>
{
    var users = db.Employees
                .Where(e => e.HireDate > new DateTime(2021, 6, 1))
                .ToList();

    foreach (var user in users)
    {
        user.Notes = "New employee";
    }

    db.SaveChanges();
});

app.MapPut("updateLinq2Db", async (NorthwindContext db) =>
{
    var employees = db.Employees
            .Where(e => e.HireDate > new DateTime(2021, 6, 1));

    await LinqToDB.LinqExtensions.UpdateAsync(employees.ToLinqToDB(), x => new Employee
    {
        Notes = "New employee"
    });
});

app.MapGet("getOrderDetails", async (NorthwindContext db) =>
{
    Order order = await GetOrder(46, db, o => o.OrderDetails);
    return new { OrderId = order.OrderId, Details = order.OrderDetails};
});

app.MapGet("getOrderWithShipper", async (NorthwindContext db) =>
{
    Order order = await GetOrder(46, db, o => o.ShipViaNavigation);
    return new { OrderId = order.OrderId, ShipVia = order.ShipVia, Shipper = order.ShipViaNavigation };
});

app.MapGet("getOrderWithCustomer", async (NorthwindContext db) =>
{
    Order order = await GetOrder(46, db, o => o.Customer);
    return new { OrderId = order.OrderId, Customer = order.Customer};
});

app.Run();

async Task<Order> GetOrder(int orderId, NorthwindContext db, params Expression<Func<Order, object>>[] includes)
{

    var baseQuery = db.Orders
        .Where(o => o.OrderId == orderId);

    if (includes.Any())
    {
        foreach (var include in includes)
        {
            baseQuery = baseQuery.Include(include);
        }
    }

    var order = await baseQuery
        .FirstAsync();

    return order;
}