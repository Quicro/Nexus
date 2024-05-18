using NexusEF.Extentions;
using NexusEF.Models;

namespace NexusOld.FormControllers {
    internal class UserFormController : NexusFormController<User, UserForm> {

        public UserFormController() : base(typeof(PolicyFormController)) {
            addColumn(new() { headerText = "ID", getText = p => p.Id.ToString(), getControl = form => form.textBox1 });
            addColumn(new() { headerText = "Name", getText = p => p.Name, getControl = form => form.textBox2 });

            addColumn(new() { getText = _ => "Policy ID", getControl = form => form.label1, dontShowInTheGrid = true });
            addColumn(new() { getText = _ => "Policy Name", getControl = form => form.label2, dontShowInTheGrid = true });
            addColumn(new() { getText = _ => "Policy Number", getControl = form => form.label3, dontShowInTheGrid = true });
            addColumn(new() { getText = _ => "Client Name", getControl = form => form.label4, dontShowInTheGrid = true });

            addColumn(new() { getQuery = UserExtention.getRoles, getControl = form => form.button1, getText = _ => "Roles", ctrlType = typeof(RoleFormController), dontShowInTheGrid = true });
            addColumn(new() { getQuery = UserExtention.getPermissions, getControl = form => form.button2, getText = _ => "Permissions", ctrlType = typeof(PermissionFormController) , dontShowInTheGrid = true});
        }

        public UserFormController(IQueryable<User> query) : this() {
            this.query = query;
        }
    }
}
