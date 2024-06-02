using NexusCore;

namespace Nexus.Blazor {
    public class WebProgram {
        public static void Main(string[] args) {

            List<MenuItem> menuItem = MenuItem.getDefaultMenuStructure();

            NexusBuilder builder = new NexusBuilder()
                .setMainForm<MyMainForm>()
                //.setViewerForm<MyViewerForm>() // This is not implemented yet
                //.setEditorForm<MyEditorForm>() // This is not implemented yet
                .setMenuConfig(menuItem)
                .setUser("Q", "")
            ;

            NexusApp app = builder.Build();

            app.Run();

            app.CleanUp();
        }
    }
}
