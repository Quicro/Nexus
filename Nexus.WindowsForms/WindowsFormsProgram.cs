using NexusCore;
using NexusCore.Components.Widget;
using NexusCore.Forms;
using NexusCore.Interfaces;
using NexusCore.Interfaces.AggregrateInterfaces.Forms;
using NexusCore.Interfaces.Widgets;
using NexusCore.Widgets;
using NexusLogging;
using System.Diagnostics;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

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
                .setUser("Q", "");

            NexusApp app = builder.Build();

            app.Run();

            app.CleanUp();

        }

        private class MyViewerForm : Form, IViewerForm {
            public required IController controller { get; set; }
            public required List<IWidget> widgets { get; set; }
            public required NexusApp nexusApp { get; set; }

            public event EventHandler OnOpen;
            public event EventHandler OnClose;
            public event EventHandler<Packet> sent;

            public void Open() {
                MyMainForm myMainForm = (MyMainForm)nexusApp.mainForm;

                myMainForm.IsMdiContainer = true;
                MdiParent = myMainForm;

                foreach (IWidget widget in widgets) {



                    if (widget is WidgetTable table && table.Rows.Count > 0) {
                        Text = table.Title;
                        // DataGridView aanmaken
                        DataGridView dataGridView = new() {
                            Dock = DockStyle.Fill,
                            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                        };

                        if (table.HeaderRow != null) {
                            foreach (WidgetTable.WidgetTableHeaderCell headerCell in table.HeaderRow.Cells) {
                                _ = dataGridView.Columns.Add(headerCell.name, headerCell.name);
                            }
                        }

                        // Voeg rijen toe aan de DataGridView
                        foreach (WidgetTable.WidgetTableRow row in table.Rows) {
                            DataGridViewRow dataGridViewRow = new();



                            var rowPacketsHandlerEnums = row.Cells.Select(c => c.packet.handlerEnum).ToList();


                            foreach (WidgetTable.WidgetTableCell cell in row.Cells) {

                                var packet = cell.packet;

                                DataGridViewTextBoxCell dataGridViewCell = new() {
                                   Value = cell?.textItem ?? string.Empty,
                                    //Value = cell?.packet.handlerEnum,
                                    Tag = cell?.packet
                                };

                                // Stel de cel kleur in
                                dataGridViewCell.Style.ForeColor = cell?.foreColor ?? Color.Black;
                                dataGridViewCell.Style.Font = cell?.fontItem ?? new Font("Arial", 10);

                                dataGridViewRow.Cells.Add(dataGridViewCell);
                            }

                            dataGridView.Rows.Add(dataGridViewRow);
                        }


                        // Voeg de DataGridView toe aan het formulier
                        dataGridView.CellMouseClick += view_Click;

                        Controls.Add(dataGridView);
                    }
                }

                Show();
            }

            public void view_Click(object sender, MouseEventArgs e) {
                DataGridView listview = (DataGridView)sender;
                Point mousePositionInListView = listview.PointToClient(MousePosition);
                DataGridView.HitTestInfo hitTest = listview.HitTest(mousePositionInListView.X, mousePositionInListView.Y);

                DataGridViewCell dataGridViewCell = listview.Rows[ hitTest.RowIndex ].Cells[ hitTest.ColumnIndex ];

                Packet packet = (Packet)dataGridViewCell.Tag;
                packet?.execute(nexusApp);

            }

        }



        private class MyEditorForm : Form, IEditorForm {
            public required IController controller { get; set; }
            public required List<IWidget> widgets { get; set; }
            public required NexusApp nexusApp { get; set; }

            public event EventHandler OnOpen;
            public event EventHandler OnClose;
            public event EventHandler<Packet> sent;

            public void Open() {

                MyMainForm myMainForm = (MyMainForm)nexusApp.mainForm;

                myMainForm.IsMdiContainer = true;
                MdiParent = myMainForm;

                foreach (IWidget widget in widgets) {

                    Control control = null;
                    if (widget is LabelWidget labelWidget) { control = labelWidget.control; }
                    if (widget is ButtonWidget buttonWidget) { control = buttonWidget.control; }
                    if (widget is TextBoxWidget textBoxWidget) { control = textBoxWidget.control; }
                    if (widget is ListViewWidget listViewWidget) { control = listViewWidget.control; }
                    if (control is null) {
                        var type = widget.GetType();
                        Debugger.Break();
                    } else {
                        Controls.Add(control);
                    }
                }

                Show();
            }
        }


        private class MyMainForm : Form, IMainForm {
            private MenuStrip menuStrip1;
            private readonly ToolStripMenuItem adresToolStripMenuItem;

            public required NexusApp nexusApp { get; set; }
            public List<IWidget> widgets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public event EventHandler<Packet> OnPacket;
            public event EventHandler OnOpen;
            public event EventHandler OnClose;
            public event EventHandler<Packet> sent;

            public new void Close() {
            }

            public void Open() {
                _ = SetUpStartMenu(nexusApp.menuItems);
                Application.Run(this);
            }

            private void click(object? sender, EventArgs e) {
                ToolStripMenuItem? toolStripMenuItem = sender as ToolStripMenuItem;
                MenuItem? menuItem = toolStripMenuItem.Tag as MenuItem;

                menuItem?.Click(nexusApp);
            }

            public void SetUpStartMenuItem(MenuItem menuItem, ToolStripMenuItem toolStripMenuItem) {
                foreach (MenuItem item in menuItem.Childs) {
                    if (!item.Show) {
                        continue;
                    }

                    ToolStripMenuItem toolStripMenuSubItem = new();

                    _ = toolStripMenuItem.DropDownItems.Add(toolStripMenuSubItem);
                    toolStripMenuSubItem.Text = item.Text;
                    toolStripMenuSubItem.Enabled = item.Authorized;
                    toolStripMenuSubItem.Tag = item;
                    toolStripMenuSubItem.Click += click;



                    SetUpStartMenuItem(item, toolStripMenuSubItem);
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
                    _ = menuStrip1.Items.Add(toolStripMenuItem);
                    toolStripMenuItem.Text = menuItem.Text;
                    toolStripMenuItem.Enabled = menuItem.Authorized;
                    toolStripMenuItem.HideDropDown();

                    SetUpStartMenuItem(menuItem, toolStripMenuItem);
                }

                return true;
            }
        }
    }
}
