using NexusCore.Components.Controller;
using NexusEF.Models;
using System.Diagnostics;

namespace NexusCore {
    [DebuggerDisplay("{Text} [{Childs.Count}] [{Authorized? \"Authorized\" : \"Unuthorized\"}] [{Show? \"Shown\" : \"Hidden\"}]")]
    public class MenuItem {

        public string Text { get; set; } = string.Empty;
        public List<string> Permissions { get; set; } = [];
        public List<MenuItem> Childs { get; set; } = [];
        public bool Authorized { get; private set; }
        public bool Show { get; internal set; }
        public Packet packet { get; private set; }

        public void Click() {
            Packet? packetType = packet;

            if (packetType is not null) {

                packetType.handler = new ViewerController();
                packetType.handler.handle(packetType);
            }
        }

        /// <summary>
        /// Determines whether the menu item should be displayed on the main menu of a form
        /// based on the user's permissions.
        /// </summary>
        /// <param name="currentPermissions">A list of permissions associated with the current user.</param>
        /// <remarks>
        /// This method calculates whether the menu item should be displayed based on the count
        /// of permissions and the presence of a special "ALL" permission. The menu item will
        /// be displayed if:
        /// - The user has no permissions.
        /// - The user has permissions that intersect with the provided list of current permissions.
        /// - The user has a "ALL" permission, which grants full authorization and displays the menu item.
        /// At least one of these conditions being true will result in the menu item being displayed.
        /// </remarks>
        /// <param name="currentPermissions">The list of permissions associated with the current user.</param>
        internal void setAuthorized(IEnumerable<string>? currentPermissions) {
            int count = Permissions.Count();
            int intersectedCount = Permissions.Intersect(currentPermissions).Count();

            bool isAdmin = currentPermissions.Any(p => p == "ALL");


            Authorized = count == 0 || intersectedCount > 0 || isAdmin;
        }

        public MenuItem() {
            Authorized = false;
            Show = false;
        }

        public MenuItem(string text) {
            Authorized = false;
            Show = false;
            Text = text;
        }
        public MenuItem(string text, params string[] permissions) {
            Authorized = false;
            Show = false;
            Text = text;
            Permissions = permissions.ToList();
        }

        public static MenuItem newMenuItem<T>(string text, params string[] permissions) where T : INexusEntity, new() {
            MenuItem menuItem = new() {
                Authorized = false,
                Show = false,
                Text = text,
                packet = new PacketType(typeof(T)),
                Permissions = permissions.ToList()
            };

            return menuItem;
        }

        public static List<MenuItem> getDefaultMenuStructure() {
            return [
                new MenuItem("Beheer") {
                    Childs = [
                        newMenuItem<Policy>("Polissen", "StartMenu.Dossier.Polis"),
                        newMenuItem<Claim>("Schadegevallen", "StartMenu.Dossier.Schade"),
                        newMenuItem<Client>("Klanten", "StartMenu.Dossier.Schade", "StartMenu.Dossier.Polis")
                    ]
                },
                new MenuItem("Administrator", "StartMenu.Admin") {
                    Childs = [
                        new MenuItem("Database", "StartMenu.Database"),
                        newMenuItem<User>("Gebruikers"),
                        newMenuItem<Role>("Rollen"),
                        newMenuItem<Permission>("Permissies")
                    ]
                },
                //new MenuItem("Test (niet auth)", "Unknown.Permission"),
                //new MenuItem("Test (leeg)"),
                //new MenuItem("Test (3 lagen)")  {
                //    Childs = new List<MenuItem> {
                //        new MenuItem("SubTest (3 lagen)")  {
                //            Childs = new List<MenuItem> {
                //                new MenuItem("SubSubTest")
                //            }
                //        }
                //    }
                //}
            ];
        }
    }
}
