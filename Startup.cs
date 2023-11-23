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
            services.AddMvc();
            services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(
           "Server=(localdb)\\mssqllocaldb;Database=StudentManagement;Trusted_Connection=True;MultipleActiveResultSets=true"));
            services.AddSingleton<IStudentRepository, StudentRepository>();
            services.AddControllers();
            services.AddControllers();
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
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
