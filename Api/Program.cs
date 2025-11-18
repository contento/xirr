var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
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

app.MapPost("/xirr", (List<CashFlow> cashFlows) =>
{
    try
    {
        double xirr = XIRREngine.XIRRCalculator.CalculateXIRRWithFallback(
            cashFlows.Select(cf => (cf.Date, cf.Amount)).ToList()
        );
        return Results.Ok(new { XIRR = xirr });
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(new { Error = ex.Message });
    }
})
.WithName("CalculateXIRR")
.WithOpenApi();

app.Run();

record CashFlow(DateTime Date, double Amount);
