﻿using System.Reflection;

namespace PadocQuantum {
    public partial class PadocForm : Form {
        public PadocForm() {
            InitializeComponent();

            Text = GetType().Name;
        }

        public static void buttonClick(object sender, EventArgs e) {
            Control control = (Control)sender;

            if (control.Tag is not null) {
                PadocTag padocTag = (PadocTag)control.Tag;

                IQueryable query = padocTag.query; //is IQueryable<Client>
                Type formType = padocTag.type;

                object? controller = Activator.CreateInstance(formType);
                Type controllerType = controller.GetType();
                FieldInfo queryField = controllerType.GetField("query", BindingFlags.Instance | BindingFlags.Public);
                queryField.SetValue(controller, query);
                MethodInfo loadGridMethod = controllerType.GetMethod("loadGrid", BindingFlags.Instance | BindingFlags.Public);
                loadGridMethod.Invoke(controller, null);
            }
        }

        public static void onLoad(GroupBox groupBox) {
            foreach (var fieldValue in groupBox.Controls) {
                if (fieldValue is Button || fieldValue is Label || fieldValue is TextBox || fieldValue is PictureBox) {
                    ((Control)fieldValue).Visible = false;
                }
            }
        }

        private void onLoad2(object sender, EventArgs e) {
            onLoad(groupBox);
        }

        public void buttonClick2(object sender, EventArgs e) {
            buttonClick(sender, e);
        }


    }
}
