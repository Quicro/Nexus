using NexusEF.Extentions;
using NexusEF.Models;

namespace NexusOld.FormControllers {
    internal class ClientFormController : NexusFormController<Client, ClientForm> {
        public ClientFormController() : base(typeof(ClientFormController)) {
            addColumn(new() { headerText = "ID", getText = p => p.Id.ToString() ,getControl = form => form.textBox1 });
            addColumn(new() { headerText = "Name", getText = p => p.Name ,getControl = form => form.textBox2 });
            addColumn(new() { headerText = "Number", getText = p => p.Number, getControl = form => form.textBox3 });
            addColumn(new() { getText = _ => "Client ID", getControl = form => form.label1, dontShowInTheGrid = true });
            addColumn(new() { getText = _ => "Client Name", getControl = form => form.label2, dontShowInTheGrid = true });
            addColumn(new() { getText = _ => "Client Number", getControl = form => form.label3, dontShowInTheGrid = true });
            addColumn(new() { getText = _ => "ID", getControl = form => form.label1, dontShowInTheGrid = true });
            addColumn(new() { getText = _ => "Name", getControl = form => form.label2, dontShowInTheGrid = true });
            addColumn(new() { getText = _ => "Number", getControl = form => form.label3, dontShowInTheGrid = true });

            addColumn(new() { getQuery = ClientExtention.getPolicy, getControl = form => form.button1, getText = _ => "Policies", dontShowInTheGrid = true , ctrlType = typeof(PolicyFormController)});

        }

        public ClientFormController(IQueryable<Client> query) : this() {
            this.query = query;
        }
    }
}
