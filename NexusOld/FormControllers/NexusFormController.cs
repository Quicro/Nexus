using NexusOld.Controls;
using NexusOld.Logging;
using PatdocQuantum;

namespace NexusOld.FormControllers {
    internal partial class NexusFormController<T, F> : NexusFormControllerBase where F : Form, new() {
        public F form;
        public NexusGrid NexusGrid;
        public IQueryable<T> query;
        public List<NexusColumn<T, F>> columns;
        Type derivedType;

        public NexusFormController(Type derivedType) {
            list.Add(this);
            ID = list.Count;

            this.derivedType = derivedType;
            LoggerBla.FormControllerStarted(derivedType, ID);

            form = new F() {
                MdiParent = NexusMDIForm.mdiForm,
                StartPosition = FormStartPosition.CenterParent
            };
            form.FormClosed += Form_FormClosed;

            derivedType = derivedType;
            columns = new();
            NexusGrid = (NexusGrid)typeof(F).GetField("NexusGrid").GetValue(form);
        }

        private void Form_FormClosed(object? sender, FormClosedEventArgs e) {
            LoggerBla.FormControllerClosed(derivedType, ID);
        }

        public NexusFormController<T, F> addColumn(NexusColumn<T, F> column) {
            columns.Add(column);
            return this;
        }

        public virtual void loadGrid() {
            NexusGrid.loadGrid(
                query,
                onDataLoaded,
                onCellClick
            );

        }

        public virtual void onCellClick(object sender, DataGridViewCellEventArgs e) {
            var controlledColumns = columns.Where(col => col.getControl is not null);

            try {
                DataGridView dgv = (DataGridView)sender;
                object? tag = dgv.CurrentCell.Tag;

                if (tag == null) {
                    foreach (NexusColumn<T, F> col in controlledColumns) {
                        col.getControl(form).Text = "???";
                    }
                } else {
                    foreach (NexusColumn<T, F> col in controlledColumns) {
                        string textControl = col.getText.Invoke((T)tag)?.ToString();
                        col.getControl(form).Text = textControl;
                        col.getControl(form).Visible = true;

                        if (col.getQuery is not null) {
                            IQueryable query = col.getQuery.Invoke((T)tag);
                            Control control = col.getControl(form);
                            Type type = GetType();
                            control.Tag = new NexusTag() { query = query, type = col.ctrlType };
                        }
                    }
                }
            } catch (Exception) {
                foreach (NexusColumn<T, F> col in controlledColumns) {
                    col.getControl(form).Text = "ERROR";
                    col.getControl(form).Visible = true;
                }
            }
        }

        public virtual void onDataLoaded(List<T> list) {
            var griddableColums = columns.Where(col => col.dontShowInTheGrid == false);

            if (list == null) {
                NexusGrid.lblStatus.Invoke(() => {
                    NexusGrid.lblStatus.Text = "Fout!";
                    NexusGrid.btnCancel.Visible = false;
                });
                return;
            }

            NexusGrid.btnCancel.Invoke(() => {
                NexusGrid.btnCancel.Visible = false;
            });

            NexusGrid.lblStatus.Invoke(() => {
                NexusGrid.lblStatus.Text = "Geladen!";
                NexusGrid.lblStatus.Visible = false;
            });

            NexusGrid.Invoke(() => {
                foreach (NexusColumn<T, F> col in griddableColums) {
                    NexusGrid.theGrid.Columns.Add(new DataGridViewTextBoxColumn() {
                        HeaderText = col.headerText
                    });
                }

                foreach (T entity in list) {
                    DataGridViewRow row = new DataGridViewRow();

                    foreach (NexusColumn<T, F> col in griddableColums) {
                        string value;
                        try {
                            var Getter = col.getText;
                            value = Getter.Invoke(entity);
                        } catch (Exception) {
                            value = "ERROR";
                        }

                        //TODO: andere soorten cellen ook kunnen aanmaken zoals ButtonCell
                        var cell = new DataGridViewTextBoxCell() {
                            Value = value,
                            Tag = entity
                        };
                        row.Cells.Add(cell);
                    }

                    if (griddableColums.Count() > 0)
                        NexusGrid.theGrid.Rows.Add(row);
                }

            });


            form.Invoke(form.Show);
            form.Invoke(() => form.Tag = this);

            NexusGrid.Invoke(() => {
                if (list.Count != 0 && griddableColums.Count() != 0) {
                    onCellClick(NexusGrid.theGrid, new(1, 1));
                }
            });
        }
    }

    internal class NexusFormControllerBase {
        public static List<NexusFormControllerBase> list = new();
        public int ID;
    }
}
