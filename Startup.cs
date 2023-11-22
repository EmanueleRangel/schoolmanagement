using Microsoft.EntityFrameworkCore;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        try{
            services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString(@"Server=WINAPLDCOFJ0HBR;Database=College;Trusted_Connection=True;")));
        
        services.AddHttpContextAccessor();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddControllers();
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }
        catch(Exception ex){
            Console.WriteLine(ex.ToString());
            throw;
        }
        
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
         if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });

    }
}
