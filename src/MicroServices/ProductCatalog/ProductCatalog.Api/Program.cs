using FastEndpoints;
using ProductCatalog.Domain.Abstractions;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IProductRepository, FakeProductRepository>();
builder.Services.AddSingleton<Context>(sp =>
{
    List<Product> products =[
        new Product(1, "Popular Product", 80.00m),
        new Product(2, "Special Item", 80.00m, 50m),
        new Product(3, "Extra Product", 80.00m),
        new Product(4, "Bonus Product", 80.00m, 70m),
        new Product(5, "Fancy Product", 80.00m, 70m),
        new Product(6, "Smart Product", 99.99m),
        new Product(7, "Old-school Product", 199.00m),
        new Product(8, "Future Product", 1.00m)
    ];

    return new Context { Products = products.ToDictionary(p => p.Id) };
});


builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
{
    policy.WithOrigins("https://localhost:7108");
    policy.WithMethods("GET");
    policy.AllowAnyMethod();
}));


builder.Services.AddFastEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

//app.MapGet("api/products", async (IProductRepository repository) => await repository.GetAllAsync());
// app.MapGet("api/products/{id:int}", async(int id, IProductRepository repository) => await repository.GetAsync(id));

app.MapFastEndpoints();

app.Run();

