namespace MVCEmpty;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();


        var app = builder.Build();
        app.UseHttpsRedirection();

        //app.MapDefaultControllerRoute();

        app.MapControllerRoute("default",
            "{Controller=Pizza}/{Action=Index}/{id?}");
        //app.MapGet("/bb-104", () => Results.Content("<h1>Nurlan</h1>",("text/html")));
        //app.MapGet("/home", () => new
        //{
        //    Name = "Nurlan",
        //    Surname = "Haciyev"
        //});
        //app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}
