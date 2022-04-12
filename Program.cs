var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost",
                                              "http://localhost:3000");
                      });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var client = new HttpClient();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(MyAllowSpecificOrigins);
}

app.UseHttpsRedirection();


//TODO: Create specific models and controllers for Currency;
app.MapGet("/currencies", async () => {
    var stringTask = client.GetStringAsync("https://free.currconv.com/api/v7/convert?q=USD_EUR,EUR_USD&compact=ultra&apiKey=be2f246e873baa03563e");

    var currencies  = await stringTask;
    Console.Write(currencies);
    return currencies;
}).WithName("GetCurencies");
;

//TODO: Create specific models and controllers for Currency;
app.MapGet("/countries", async () => {
    
    client.DefaultRequestHeaders.Accept.Clear();
    var stringTask = client.GetStringAsync("https://free.currconv.com/api/v7/countries?apiKey=be2f246e873baa03563e");

    var currencies  = await stringTask;
    return currencies;
}).WithName("GetCountries");
;

app.Run();