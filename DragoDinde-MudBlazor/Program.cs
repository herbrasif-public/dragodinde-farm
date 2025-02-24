using MudBlazor.Services;
using DragoDinde_MudBlazor.Components;
using DragoDinde_MudBlazor.Repositories;
using DragoDinde_MudBlazor.Business;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<DragodindeRepository>();

builder.Services.AddSingleton<StringComparerCustom>();
builder.Services.AddSingleton<DragodindeGenealogic>();
builder.Services.AddSingleton<DragodindeReproduction>();
builder.Services.AddSingleton<DragodindeManager>();

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
