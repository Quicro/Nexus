using NexusCore;
using NexusCore.Interfaces.AggregrateInterfaces.Forms;

public class MyMainForm : IMainForm
{
    public static MyMainForm singleton { get; private set; }
    public NexusApp app { get; set; }

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
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        webapp = builder.Build();

        if (!webapp.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            webapp.UseHsts();
        }

        webapp.UseHttpsRedirection();

        webapp.UseStaticFiles();

        webapp.UseRouting();

        webapp.MapBlazorHub();
        webapp.MapFallbackToPage("/_Host");
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
