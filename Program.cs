List<PaintColor> paintColors = new List<PaintColor>
{
    new PaintColor {Id = 1, Price = 499.99M, Color = "Silver"},
    new PaintColor {Id = 2, Price = 599.99M, Color = "Midnight Blue"},
    new PaintColor {Id = 3, Price = 699.99M, Color = "Firebrick Red"},
    new PaintColor {Id = 4, Price = 799.99M, Color = "Spring Green"}
};

List<Interior> interiors = new List<Interior>
{
    new Interior {Id = 1, Price = 349.99M, Material = "Beige Fabric"},
    new Interior {Id = 2, Price = 449.99M, Material = "Charcoal Fabric"},
    new Interior {Id = 3, Price = 549.99M, Material = "White Leather"},
    new Interior {Id = 4, Price = 649.99M, Material = "Black Leather"}
};

List<Technology> technologies = new List<Technology>
{
    new Technology {Id = 1, Price = 579.99M, Package = "Basic Package"},
    new Technology {Id = 2, Price = 679.99M, Package = "Navigation Package"},
    new Technology {Id = 3, Price = 779.99M, Package = "Visibility Package"},
    new Technology {Id = 4, Price = 1279.99M, Package = "Ultra Package"}
};

List<Wheels> wheels = new List<Wheels>
{
    new Wheels {Id = 1, Price = 429.99M, Style = "17-inch Pair Radial"},
    new Wheels {Id = 2, Price = 529.99M, Style = "17-inch Pair Radial Black"},
    new Wheels {Id = 3, Price = 629.99M, Style = "17-inch Pair Spoke Silver"},
    new Wheels {Id = 4, Price = 729.99M, Style = "17-inch Pair Spoke Black"}
};

List<Order> orders = new List<Order>
{
    new Order {Id = 1, Timestamp = new DateTime(2023, 8, 24), WheelId = 4, TechnologyId = 4, PaintId = 4, InteriorId = 4}
};

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(options =>
        {
            options.AllowAnyOrigin();
            options.AllowAnyMethod();
            options.AllowAnyHeader();
        });
}

app.UseHttpsRedirection();

/* Endpoints */

// get wheels collection
app.MapGet("/wheels", () =>
{
    return wheels;
});

// get technology collection
app.MapGet("/technologies", () =>
{
    return technologies;
});

// get interior collection
app.MapGet("/interiors", () =>
{
    return interiors;
});

// get paint colors collection
app.MapGet("/paintColors", () =>
{
    return paintColors;
});

// get orders collection
// returns a modified list which embeds any data linked by foreign key
app.MapGet("/orders", () =>
{
    
    List<Order> updatedOrders = orders.Select(o =>
    {
        return new Order
        {
            Id = o.Id,
            Timestamp = o.Timestamp,
            WheelId = o.WheelId,
            Wheels = wheels.FirstOrDefault(w => w.Id == o.WheelId),
            TechnologyId = o.TechnologyId,
            Technology = technologies.FirstOrDefault(t => t.Id == o.TechnologyId),
            PaintId = o.PaintId,
            PaintColor = paintColors.FirstOrDefault(p => p.Id == o.PaintId),
            InteriorId = o.InteriorId,
            Interior = interiors.FirstOrDefault(i => i.Id == o.InteriorId),
        };
    }).ToList();

    return updatedOrders;
});

// post new order
app.MapPost("/orders", (Order order) =>
{
    order.Id = orders.Count > 0 ? orders.Max(o => o.Id) + 1 : 1;
    order.Timestamp = DateTime.Now;
    orders.Add(order);
    return order;
});

app.Run();