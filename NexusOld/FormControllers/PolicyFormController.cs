using NexusEF.Extentions;
using NexusEF.Models;
using System.Security.Policy;

namespace NexusOld.FormControllers {
    internal class PolicyFormController : NexusFormController<Policy, PolicyForm> {

        public PolicyFormController() : base(typeof(PolicyFormController)) {
            addColumn(new() { headerText = "ID", getText = p => p.Id.ToString(), getControl = form => form.textBox1 });
            addColumn(new() { headerText = "Name", getText = p => p.Name, getControl = form => form.textBox2 });

            addColumn(new() { getText = _ => "ID", getControl = form => form.label1, dontShowInTheGrid = true });
            addColumn(new() { getText = _ => "Name", getControl = form => form.label2, dontShowInTheGrid = true });

            addColumn(new() { getQuery = PolicyExtention.getClaims, getControl = form => form.button1, getText = _ => "Claims", ctrlType = typeof(ClaimFormController), dontShowInTheGrid = true });
            addColumn(new() { getQuery = PolicyExtention.getClient, getControl = form => form.button2, getText = _ => "Client", ctrlType = typeof(ClientFormController), dontShowInTheGrid = true });
        }

        public PolicyFormController(IQueryable<Policy> query) : this() {
            this.query = query;
        }
    }
}
