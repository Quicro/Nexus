using Nexus.Blazor.Components;
using NexusCore;
using NexusCore.Interfaces.AggregrateInterfaces.Forms;

public class MyMainForm : IMainForm
{
    public static MyMainForm singleton { get; private set; }
    public NexusApp app { get; set; }
    public NexusApp nexusApp { get; set; }

    WebApplication webapp;

    public event EventHandler<Packet> OnPacket;
    public event EventHandler OnOpen;
    public event EventHandler OnClose;

    public void Close()
    {
        singleton = null;
    }

    public void End()
    {

    }

    public void Open()
    {
        if (singleton is not null)
        {
            throw new Exception("Cannot open a new MainForm, because there is already one open");
        }
        else
        {
            singleton = this;
        }

        var builder = WebApplication.CreateBuilder();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var webapp = builder.Build();

        // Configure the HTTP request pipeline.
        if (!webapp.Environment.IsDevelopment())
        {
            webapp.UseExceptionHandler("/Error");
        }

        webapp.UseStaticFiles();
        webapp.UseAntiforgery();

        webapp.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        webapp.Run();
    }

    public void Start()
    {
        SetUpStartMenu(app.menuItems);
        webapp.Run();
    }

    public bool SetUpStartMenu(List<MenuItem> setup)
    {
        //throw new NotImplementedException();
        return false;
    }
}
