using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NexusCore;
using NexusCore.Components.Controller;
using NexusEF.Models;
using NexusEF.Models.Context;
using NexusLogging;

using NexusLogging;
using System.Reflection;

namespace Nexus.Blazor.Wrappers {
    public class ViewerWrapper {
        ViewerController controller;

        public ViewerWrapper(ViewerController controller) {
            this.controller = controller;
        }

        public static Type Convert(string entityName) {
            // Get the current assembly
            var assembly = typeof(NexusOldContext).Assembly;

            // Find the type in the assembly
            var type = assembly.GetTypes().FirstOrDefault(t => t.FullName == entityName);

            if (type == null) {
                throw new ArgumentException($"Entity type '{entityName}' not found.");
            }

            return type;
        }

        public void OnInitialized() {
            Logger.LogDebug(nameof(OnInitialized) + " " + GetType().Name);
        }
        public void OnParametersSet(string type) {
            Type realType = Convert(type);

            PacketType packet = new PacketType(realType);
            controller.handle(packet);

            Logger.LogDebug(nameof(OnParametersSet) + " " + type + " " + GetType().Name);
        }
        public void Dispose() {
            Logger.LogDebug(nameof(Dispose) + " " + GetType().Name);
        }
    }
}
