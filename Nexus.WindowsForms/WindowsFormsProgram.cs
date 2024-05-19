using NexusCore;
using NexusCore.Components.AggregrateInterfaces.Forms;
using NexusCore.Interfaces;
using NexusCore.Interfaces.AggregrateInterfaces.Forms;

namespace Nexus.WindowsForms
{
    internal static class WindowsFormsProgram
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            MyMainForm mainForm = new MyMainForm();
            MyViewerForm viewerForm = new MyViewerForm();
            MyEditorForm editorForm = new MyEditorForm();
            List<MenuItem> menuItem = MenuItem.getDefaultMenuStructure();

            var builder = new NexusBuilder()
                .setMainForm(mainForm)
                .setViewerForm(viewerForm) //WIP
                .setEditorForm(editorForm) //WIP
                .setMenuConfig(menuItem)
                .setUser("Q", "")
            ;

            NexusApp app = builder.Build();

            app.Destruct();

        }
        class MyViewerForm : Form, IViewerForm
        {
            public IController controller { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public event EventHandler OnDataLoading;
            public event EventHandler OnDataLoaded;
            public event EventHandler OnDataLoadCancelled;
            public event EventHandler OnOpen;
            public event EventHandler OnClose;

            public void End()
            {
                throw new NotImplementedException();
            }

            public void LoadData()
            {
                throw new NotImplementedException();
            }

            public void Open()
            {
                throw new NotImplementedException();
            }

            public void Start(List<MenuItem> menu)
            {
                throw new NotImplementedException();
            }
        }

        class MyEditorForm : Form, IEditorForm
        {
            public IController controller { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public event EventHandler OnDataLoading;
            public event EventHandler OnDataLoaded;
            public event EventHandler OnDataLoadCancelled;
            public event EventHandler OnOpen;
            public event EventHandler OnClose;

            public void End()
            {
                throw new NotImplementedException();
            }

            public void LoadData()
            {
                throw new NotImplementedException();
            }

            public void Open()
            {
                throw new NotImplementedException();
            }

            public void Start(List<MenuItem> menu)
            {
                throw new NotImplementedException();
            }
        }

        class MyMainForm : Form, IMainForm
        {
            private MenuStrip menuStrip1;
            private ToolStripMenuItem adresToolStripMenuItem;

            public event EventHandler<Packet> OnPacket;
            public event EventHandler OnOpen;
            public event EventHandler OnClose;

            public void Close()
            {
            }

            public void End()
            {
            }

            public void Open()
            {
            }

            private void click(object? sender, EventArgs e)
            {
                ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
                MenuItem menuItem = toolStripMenuItem.Tag as MenuItem;

                menuItem?.Click();
            }

            public void SetUpStartMenuItem(MenuItem menuItem, ToolStripMenuItem toolStripMenuItem)
            {
                foreach (MenuItem item in menuItem.Childs)
                {
                    if (!item.Show)
                        continue;

                    var toolStripMenuSuubItem = new ToolStripMenuItem();

                    toolStripMenuItem.DropDownItems.Add(toolStripMenuSuubItem);
                    toolStripMenuSuubItem.Text = item.Text;
                    toolStripMenuSuubItem.Enabled = item.Authorized;
                    toolStripMenuSuubItem.Tag = item;
                    toolStripMenuSuubItem.Click += click;



                    SetUpStartMenuItem(item, toolStripMenuSuubItem);
                }
            }

            public bool SetUpStartMenu(List<MenuItem> setup)
            {
                menuStrip1 = new MenuStrip();
                Controls.Add(menuStrip1);
                MainMenuStrip = menuStrip1;

                foreach (MenuItem menuItem in setup)
                {
                    if (!menuItem.Show)
                        continue;

                    var toolStripMenuItem = new ToolStripMenuItem();
                    menuStrip1.Items.Add(toolStripMenuItem);
                    toolStripMenuItem.Text = menuItem.Text;
                    toolStripMenuItem.Enabled = menuItem.Authorized;
                    toolStripMenuItem.HideDropDown();

                    SetUpStartMenuItem(menuItem, toolStripMenuItem);
                }

                return true;
            }

            public void Start(List<MenuItem> menu)
            {

                Application.Run(this);
            }
        }
    }
}
