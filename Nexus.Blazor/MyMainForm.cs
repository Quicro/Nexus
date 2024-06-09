using Nexus.Blazor.Components;
using NexusCore;
using NexusCore.Components.Controller;
using NexusCore.Components.Forms;
using NexusCore.Interfaces.AggregrateInterfaces.Forms;
using NexusLogging;

public class MyMainForm : IMainForm {
    public static MyMainForm? singleton { get; private set; }
    public NexusApp nexusApp { get; set; }

    public WebApplication webapp;

    public event EventHandler<Packet> OnPacket;
    public event EventHandler OnOpen;
    public event EventHandler OnClose;

    public void Close() {
        singleton = null;
    }

    public void Stop() {

    }

    public void Open() {
        singleton = singleton is not null ? throw new Exception("Cannot open a new MainForm, because there is already one open") : this;

        WebApplicationBuilder builder = WebApplication.CreateBuilder();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        builder.Services.AddSingleton(nexusApp);
        builder.Services.AddScoped<ViewerController>();
        builder.Services.AddScoped<EditorController>(); 
        builder.Services.AddSingleton<Logger>();


        webapp = builder.Build();

        // Configure the HTTP request pipeline.
        if (!webapp.Environment.IsDevelopment()) {
            webapp.UseExceptionHandler("/Error");
        }

        webapp.UseStaticFiles();
        webapp.UseAntiforgery();

        webapp.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
    }

    public void Start() {
        SetUpStartMenu(nexusApp.menuItems);
        webapp.Run();
    }

    public bool SetUpStartMenu(List<MenuItem> setup) {
        //throw new NotImplementedException();
        return false;
    }
}
