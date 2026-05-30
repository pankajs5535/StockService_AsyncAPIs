using Microsoft.EntityFrameworkCore;
using StockService_AsyncAPI.Data;
using StockService_AsyncAPI.Repository;
using StockService_AsyncAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// --- PHASE 1: REGISTER SERVICES ---

builder.Services.AddControllers();

// 1. ADD CORS POLICIES (This was missing!)
builder.Services.AddCors(options =>
{
    // Policy for local development (React/Vue/Angular on port 3000)
    options.AddPolicy("DevPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // No trailing slash!
              .AllowAnyHeader()
              .AllowAnyMethod();
    });

    // Policy for production
    options.AddPolicy("NoLocalhost", policy =>
    {
        policy.WithOrigins("https://your-production-domain.com")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Database
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories & Services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- PHASE 2: BUILD THE APP ---

var app = builder.Build();

// --- PHASE 3: CONFIGURE PIPELINE (The Order is Critical) ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 2. MIDDLEWARE ORDER: UseRouting must come BEFORE UseCors
app.UseRouting();

// 3. APPLY THE POLICY: Choose policy based on environment
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevPolicy");
}
else
{
    app.UseCors("NoLocalhost");
}

// 4. UseAuthorization must come AFTER UseCors
app.UseAuthorization();

// 5. Map routes should be near the end
app.MapControllers();

// --- PHASE 4: START THE APP ---
app.Run();
