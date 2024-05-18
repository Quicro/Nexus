using NexusEF.Extentions;
using NexusEF.Models;

namespace NexusOld.FormControllers {
    internal class PermissionFormController : NexusFormController<Permission, PermissionForm> {

        public PermissionFormController() : base(typeof(PolicyFormController)) {
            addColumn(new() { headerText = "ID", getText = p => p.Id.ToString(), getControl = form => form.textBox1 });
            addColumn(new() { headerText = "Name", getText = p => p.Name, getControl = form => form.textBox2 });

            addColumn(new() { getText = _ => "Policy ID", getControl = form => form.label1, dontShowInTheGrid = true });
            addColumn(new() { getText = _ => "Policy Name", getControl = form => form.label2, dontShowInTheGrid = true });

            addColumn(new() { getQuery = PermissionExtention.getRoles, getControl = form => form.button1, getText = _ => "Roles", ctrlType = typeof(RoleFormController), dontShowInTheGrid = true });
            addColumn(new() { getQuery = PermissionExtention.getUsers, getControl = form => form.button2, getText = _ => "Users", ctrlType = typeof(UserFormController) , dontShowInTheGrid = true});
        }

        public PermissionFormController(IQueryable<Permission> query) : this() {
            this.query = query;
        }
    }
}
