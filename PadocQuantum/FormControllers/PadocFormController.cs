using PadocQuantum.Controls;
using PadocQuantum.Logging;
using PatdocQuantum;

namespace PadocQuantum.FormControllers {
    internal partial class PadocFormController<T, F> : PadocFormControllerBase where F : Form, new() {
        public F form;
        public PadocGrid padocGrid;
        public IQueryable<T> query;
        public List<PadocColumn<T, F>> columns;
        Type derivedType;

        public PadocFormController(Type derivedType) {
            list.Add(this);
            ID = list.Count;

            this.derivedType = derivedType;
            LoggerBla.FormControllerStarted(derivedType, ID);

            form = new F() {
                MdiParent = PadocMDIForm.mdiForm,
                StartPosition = FormStartPosition.CenterParent
            };
            form.FormClosed += Form_FormClosed;

            derivedType = derivedType;
            columns = new();
            padocGrid = (PadocGrid)typeof(F).GetField("padocGrid").GetValue(form);
        }

        private void Form_FormClosed(object? sender, FormClosedEventArgs e) {
            LoggerBla.FormControllerClosed(derivedType, ID);
        }

        public PadocFormController<T, F> addColumn(PadocColumn<T, F> column) {
            columns.Add(column);
            return this;
        }

        public virtual void loadGrid() {
            padocGrid.loadGrid(
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
                    foreach (PadocColumn<T, F> col in controlledColumns) {
                        col.getControl(form).Text = "???";
                    }
                } else {
                    foreach (PadocColumn<T, F> col in controlledColumns) {
                        string textControl = col.getText.Invoke((T)tag)?.ToString();
                        col.getControl(form).Text = textControl;
                        col.getControl(form).Visible = true;

                        if (col.getQuery is not null) {
                            IQueryable query = col.getQuery.Invoke((T)tag);
                            Control control = col.getControl(form);
                            Type type = GetType();
                            control.Tag = new PadocTag() { query = query, type = col.ctrlType };
                        }
                    }
                }
            } catch (Exception) {
                foreach (PadocColumn<T, F> col in controlledColumns) {
                    col.getControl(form).Text = "ERROR";
                    col.getControl(form).Visible = true;
                }
            }
        }

        public virtual void onDataLoaded(List<T> list) {
            var griddableColums = columns.Where(col => col.dontShowInTheGrid == false);

            if (list == null) {
                padocGrid.lblStatus.Invoke(() => {
                    padocGrid.lblStatus.Text = "Fout!";
                    padocGrid.btnCancel.Visible = false;
                });
                return;
            }

            padocGrid.btnCancel.Invoke(() => {
                padocGrid.btnCancel.Visible = false;
            });

            padocGrid.lblStatus.Invoke(() => {
                padocGrid.lblStatus.Text = "Geladen!";
                padocGrid.lblStatus.Visible = false;
            });

            padocGrid.Invoke(() => {
                foreach (PadocColumn<T, F> col in griddableColums) {
                    padocGrid.theGrid.Columns.Add(new DataGridViewTextBoxColumn() {
                        HeaderText = col.headerText
                    });
                }

                foreach (T entity in list) {
                    DataGridViewRow row = new DataGridViewRow();

                    foreach (PadocColumn<T, F> col in griddableColums) {
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
                        padocGrid.theGrid.Rows.Add(row);
                }

            });


            form.Invoke(form.Show);
            form.Invoke(() => form.Tag = this);

            padocGrid.Invoke(() => {
                if (list.Count != 0 && griddableColums.Count() != 0) {
                    onCellClick(padocGrid.theGrid, new(1, 1));
                }
            });
        }
    }

    internal class PadocFormControllerBase {
        public static List<PadocFormControllerBase> list = new();
        public int ID;
    }
}
