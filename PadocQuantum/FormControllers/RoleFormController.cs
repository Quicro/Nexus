using PadocEF.Extentions;
using PadocEF.Models;

namespace PadocQuantum.FormControllers {
    internal class RoleFormController : PadocFormController<Role, ClaimForm> {

        public RoleFormController() : base(typeof(ClaimFormController)) {
            addColumn(new() { headerText = "ID", getText = p => p.Id.ToString(), getControl = form => form.textBox1 });
            addColumn(new() { headerText = "Name", getText = p => p.Name, getControl = form => form.textBox2 });

            addColumn(new() { getText = _ => "Claim ID", getControl = form => form.label1, dontShowInTheGrid = true });
            addColumn(new() { getText = _ => "Claim Name", getControl = form => form.label2, dontShowInTheGrid = true });
            addColumn(new() { getText = _ => "Claim Number", getControl = form => form.label3, dontShowInTheGrid = true });

            addColumn(new() { getQuery = RoleExtention.getPermissions, getControl = form => form.button1, getText = _ => "Permissions", ctrlType = typeof(PermissionFormController), dontShowInTheGrid = true });
            addColumn(new() { getQuery = RoleExtention.getUsers, getControl = form => form.button2, getText = _ => "Users", ctrlType = typeof(UserFormController), dontShowInTheGrid = true });

        }

        public RoleFormController(IQueryable<Role> query) : this() {
            this.query = query;
        }
    }
}