using NexusCore;
using NexusCore.AggregrateInterfaces.Forms;
using NexusCore.Interfaces;
using NexusCore.Interfaces.AggregrateInterfaces.Forms;

public class MainForm : IMainForm {
    public static MainForm Instance { get; private set; }
    public IController controller { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    WebApplication app;
    public List<MenuItem> menusetup;

    public event EventHandler<Packet> OnPacket;
    public event EventHandler OnOpen;
    public event EventHandler OnClose;

    public void Close() {
        Instance = null;
    }

    public void End() {

    }

    public void Open() {
        if (Instance is not null) {
            throw new Exception("Cannot open a new MainForm, because there is already one open");
        } else {
            Instance = this;
        }

        var builder = WebApplication.CreateBuilder();
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        app = builder.Build();

        if (!app.Environment.IsDevelopment()) {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");
    }

    public bool SetUpStartMenu(List<MenuItem> setup) {
        this.menusetup = setup;

        return false;
    }

    public void Start(List<MenuItem> menu) {
        app.Run();
    }
}
