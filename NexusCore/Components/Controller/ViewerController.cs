using Microsoft.EntityFrameworkCore;
using NexusCore.Interfaces.AggregrateInterfaces.Controller;
using NexusCore.Interfaces.AggregrateInterfaces.Forms;
using NexusCore.Widgets;
using NexusLogging;
using System.Reflection;

namespace NexusCore.Components.Controller {
    /// <summary>
    /// Controller for viewing entities and handling packets in the viewer.
    /// </summary>
    public class ViewerController : IViewerController {
        private const int PageSize = 50;

        protected List<PropertyInfo> columns;
        public IViewerForm viewerForm { get; set; }
        public NexusApp nexusApp { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewerController"/> class.
        /// </summary>
        public ViewerController() {
        }
        public ViewerController(NexusApp nexusApp) {
            Type type = nexusApp.mainForm.nexusApp.viewerFormType;
            viewerForm = (IViewerForm)Activator.CreateInstance(type);
            viewerForm.controller = this;
            viewerForm.widgets = [];
            this.nexusApp = nexusApp;
            viewerForm.nexusApp = nexusApp;
        }

        /// <summary>
        /// Handles the specified packet.
        /// </summary>
        /// <param name="packet">The packet to handle.</param>
        public void handle(Packet packet) {
            if (!packet.hasNexusEntities) {
                Logger.ViewerPacketHasNoEntitiesError();
                throw new Exception();
            }

            packet.entities = packet.getEntities();

            PropertyInfo[] fields = packet.packetType.GetProperties();
            columns = fields
                .Where(field => !fields.Select(f => f.Name).Contains(field.Name + "Id"))
                .ToList();

            WidgetTable table = new(packet.packetType.Name,columns.Count, PageSize);
            table.BeginUpdate();
            table.Headers(columns);
            table.FillIn(columns, packet.entities, packet.packetType);
            viewerForm.widgets.Add(table);


            table.EndUpdate();
            Start();
        }

        public void Start() {
            viewerForm.Open();
        }

        public void Stop() {
            throw new NotImplementedException();
        }
    }
}
