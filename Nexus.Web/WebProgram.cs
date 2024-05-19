using NexusCore;

List<MenuItem> menuItem = MenuItem.getDefaultMenuStructure();

var builder = new NexusBuilder()
    .setMainForm<MyMainForm>()
    //.setViewerForm<MyViewerForm>() // This is not implemented yet
    //.setEditorForm<MyEditorForm>() // This is not implemented yet
    .setMenuConfig(menuItem)
    .setUser("Q", "")
;

NexusApp app = builder.Build();

app.Run();

app.CleanUp();