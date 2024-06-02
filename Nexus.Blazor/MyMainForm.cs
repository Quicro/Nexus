using Nexus.Blazor.Components;
using NexusCore;
using NexusCore.Interfaces.AggregrateInterfaces.Forms;

public class MyMainForm : IMainForm {
    public static MyMainForm? singleton { get; private set; }
    public  NexusApp nexusApp { get; set; }

    private  WebApplication webapp;

    public event EventHandler<Packet> OnPacket;
    public event EventHandler OnOpen;
    public event EventHandler OnClose;

    public void Close() {
        singleton = null;
    }

    public void End() {

    }

    public void Open() {
        singleton = singleton is not null ? throw new Exception("Cannot open a new MainForm, because there is already one open") : this;

        WebApplicationBuilder builder = WebApplication.CreateBuilder();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();
        builder.Services.AddSingleton(nexusApp);

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
