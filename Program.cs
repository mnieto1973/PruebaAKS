using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
List<string> targetHostIpAddresses = new List<string> ();
targetHostIpAddresses.Add("82.98.172.3");
builder.Services.AddHealthChecks().AddUrlGroup (new Uri ("https://gitdoc.cadema.es"),"API",Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
new string[]{"API"});




builder.Services.AddHealthChecksUI(s=>
{
    s.AddHealthCheckEndpoint ("API","/healthUI/apis");
    s.SetEvaluationTimeInSeconds (300);
}).AddSqliteStorage ("Data Source = /data/healthchecksPortal.db)");
builder.Services.AddDbContext <AppDbContext>(options=>options.UseSqlite ("Data Source=/data/appTodo.db"))                    ;
var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(point=>{
  point.MapHealthChecks ("healthUI/apis",new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions {
            Predicate= check=>check.Tags.Contains("API")
,ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse 
  });
});
app.UseHealthChecksUI ();
app.Run();
