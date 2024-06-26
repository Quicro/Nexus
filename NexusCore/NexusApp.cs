using NexusCore.Interfaces;
using NexusCore.Interfaces.AggregrateInterfaces.Forms;
using NexusEF.Models;
using NexusLogging;

namespace NexusCore {
    public class NexusApp {
        public Type mainFormType;
        public Type viewerFormType;
        public Type editorFormType;
        public List<MenuItem> menuItems;
        public User currentUser;
        public IMainForm mainForm;

        public NexusApp() {
        }

        public void CleanUp() {
            try {
                mainForm.Close();

                Logger.ApplicationEnded();
            } catch (Exception e) {
                Logger.LogError(e.Message);
                Logger.ApplicationCrashed();
                throw;
            }
        }

        public void Run() {
            mainForm = (IMainForm)Activator.CreateInstance(mainFormType);
            mainForm.nexusApp = this;

            mainForm.Open();

            Logger.ApplicationStarted();

        }
    }
}
