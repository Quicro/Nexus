using NexusCore.Components.Controller;
using NexusLogging;

namespace Nexus.Blazor.Wrappers {
    public class EditorWrapper{

        EditorController controller;

        public EditorWrapper(EditorController controller) {
            this.controller = controller;
        }

        public void OnInitialized() {
            Logger.LogDebug(nameof(OnInitialized) + " " + GetType().Name);
        }
        public void OnParametersSet(string type, string id) {
            Logger.LogDebug(nameof(OnParametersSet) + " " + type + ":" + id + " " + GetType().Name);
        }
        public void Dispose() {
            Logger.LogDebug(nameof(Dispose) + " " + GetType().Name);
        }
    }
}
