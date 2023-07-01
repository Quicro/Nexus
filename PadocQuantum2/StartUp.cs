using Microsoft.EntityFrameworkCore.Storage;
using PadocEF;
using PadocEF.Extentions;
using PadocEF.Models;
using PadocQuantum2.BigForms;
using PadocQuantum2.Controllers;
using PadocQuantum2.Interfaces;
using PadocQuantum2.Logging;

namespace PadocQuantum2
{
    internal static class StartUp {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            string dateTime = $"{DateTime.Now:HH:mm:ss dd/MM/yy}";

            Logger.logTitle("Padoc");
            Logger.logHeader($"Started at {dateTime}    \n");

            Logger.ApplicationStarted();

            try {
                StartUpApp();
            } catch (Exception exception) {
                Logger.ApplicationCrashed();
                throw;
            }

            Logger.ApplicationEnded();
        }

        private static void StartUpApp() {
            ApplicationConfiguration.Initialize();
            Application.Run(PadocMDIForm.singleton);


        }

        public static void doMyFirstPacket() {
            var sender = new PacketSender();
            IPacketReceiver viewerController = (IPacketReceiver)Activator.CreateInstance(typeof(ViewerController));
            PacketType packet = new PacketType(typeof(Policy));


            sender.send(viewerController, packet);
        }

        public static void doMySecondPacket(int policyID) {
            var sender = new PacketSender();
            IPacketReceiver editorController = (IPacketReceiver)Activator.CreateInstance(typeof(EditorController));
            Packet packet = (Packet)Activator.CreateInstance(typeof(PacketSingle));

            packet.query = DatabaseManager.context.Policy.Where(p => p.Id == policyID);


            sender.send(editorController, packet);
        }
    }
}