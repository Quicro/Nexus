

using PadocQuantum2;



List<MenuItem> menuItem = MenuItem.getDefaultMenuStructure();
MainForm mainForm = new MainForm();

var padocBuilder = new PadocBuilder()
    .setMainForm(mainForm)
    .setMenuConfig(menuItem)
    .setUser("Q", "")
;

padocBuilder.Build();

