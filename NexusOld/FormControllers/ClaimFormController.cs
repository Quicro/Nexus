
using NexusEF.Extentions;
using NexusEF.Models;

namespace NexusOld.FormControllers {
    internal class ClaimFormController : NexusFormController<Claim, ClaimForm> {

        public ClaimFormController() : base(typeof(ClaimFormController)) {
            addColumn(new() { headerText = "ID", getText = p => p.Id.ToString(), getControl = form => form.textBox1 });
            addColumn(new() { headerText = "Name", getText = p => p.Name, getControl = form => form.textBox2 });
            addColumn(new() { headerText = "Number", getText = p => p.Number, getControl = form => form.textBox3 });

            addColumn(new() { getText = _ => "Claim ID", getControl = form => form.label1, dontShowInTheGrid = true });
            addColumn(new() { getText = _ => "Claim Name", getControl = form => form.label2, dontShowInTheGrid = true });
            addColumn(new() { getText = _ => "Claim Number", getControl = form => form.label3, dontShowInTheGrid = true });

            addColumn(new() { getQuery = ClaimExtention.getPolicy, getControl = form => form.button1, getText = _ => "Policies", ctrlType = typeof(PolicyFormController), dontShowInTheGrid = true });

        }

        public ClaimFormController(IQueryable<Claim> query) : this() {
            this.query = query;
        }
    }
}
