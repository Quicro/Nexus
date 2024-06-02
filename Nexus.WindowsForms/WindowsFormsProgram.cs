using NexusCore;
using NexusCore.Interfaces;
using NexusCore.Interfaces.AggregrateInterfaces.Forms;

namespace Nexus.WindowsForms {
    internal static class WindowsFormsProgram {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main() {
            ApplicationConfiguration.Initialize();

            List<MenuItem> menuItem = MenuItem.getDefaultMenuStructure();

            NexusBuilder builder = new NexusBuilder()
                .setMainForm<MyMainForm>()
                .setViewerForm<MyViewerForm>()
                .setEditorForm<MyEditorForm>()
                .setMenuConfig(menuItem)
                .setUser("Q", "")
            ;

            NexusApp app = builder.Build();

            app.Run();

            app.CleanUp();

        }

        private class MyViewerForm : Form, IViewerForm {
            public IController controller { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public event EventHandler OnDataLoading;
            public event EventHandler OnDataLoaded;
            public event EventHandler OnDataLoadCancelled;
            public event EventHandler OnOpen;
            public event EventHandler OnClose;

            public void End() {
                throw new NotImplementedException();
            }

            public void LoadData() {
                throw new NotImplementedException();
            }

            public void Open() {
                throw new NotImplementedException();
            }

            public void Start() {
                throw new NotImplementedException();
            }
        }

        private class MyEditorForm : Form, IEditorForm {
            public IController controller { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public event EventHandler OnDataLoading;
            public event EventHandler OnDataLoaded;
            public event EventHandler OnDataLoadCancelled;
            public event EventHandler OnOpen;
            public event EventHandler OnClose;

            public void End() {
                throw new NotImplementedException();
            }

            public void LoadData() {
                throw new NotImplementedException();
            }

            public void Open() {
                throw new NotImplementedException();
            }


            public void Start() {
                throw new NotImplementedException();
            }
        }

        private class MyMainForm : Form, IMainForm {
            private MenuStrip menuStrip1;
            private readonly ToolStripMenuItem adresToolStripMenuItem;

            public NexusApp nexusApp { get; set; }

            public event EventHandler<Packet> OnPacket;
            public event EventHandler OnOpen;
            public event EventHandler OnClose;

            public new void Close() {
            }

            public void End() {
            }

            public void Open() {
            }

            private void click(object? sender, EventArgs e) {
                ToolStripMenuItem? toolStripMenuItem = sender as ToolStripMenuItem;
                MenuItem? menuItem = toolStripMenuItem.Tag as MenuItem;

                menuItem?.Click();
            }

            public void SetUpStartMenuItem(MenuItem menuItem, ToolStripMenuItem toolStripMenuItem) {
                foreach (MenuItem item in menuItem.Childs) {
                    if (!item.Show) {
                        continue;
                    }

                    ToolStripMenuItem toolStripMenuSuubItem = new();

                    toolStripMenuItem.DropDownItems.Add(toolStripMenuSuubItem);
                    toolStripMenuSuubItem.Text = item.Text;
                    toolStripMenuSuubItem.Enabled = item.Authorized;
                    toolStripMenuSuubItem.Tag = item;
                    toolStripMenuSuubItem.Click += click;



                    SetUpStartMenuItem(item, toolStripMenuSuubItem);
                }
            }

            public bool SetUpStartMenu(List<MenuItem> setup) {
                menuStrip1 = new MenuStrip();
                Controls.Add(menuStrip1);
                MainMenuStrip = menuStrip1;

                foreach (MenuItem menuItem in setup) {
                    if (!menuItem.Show) {
                        continue;
                    }

                    ToolStripMenuItem toolStripMenuItem = new();
                    menuStrip1.Items.Add(toolStripMenuItem);
                    toolStripMenuItem.Text = menuItem.Text;
                    toolStripMenuItem.Enabled = menuItem.Authorized;
                    toolStripMenuItem.HideDropDown();

                    SetUpStartMenuItem(menuItem, toolStripMenuItem);
                }

                return true;
            }
            public void Start() {
                SetUpStartMenu(nexusApp.menuItems);
                Application.Run(this);
            }
        }
    }
}
