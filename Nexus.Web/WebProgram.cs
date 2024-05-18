

using NexusCore;



List<MenuItem> menuItem = MenuItem.getDefaultMenuStructure();
MainForm mainForm = new MainForm();

var NexusBuilder = new NexusBuilder()
    .setMainForm(mainForm)
    .setMenuConfig(menuItem)
    .setUser("Q", "")
;

NexusBuilder.Build();

