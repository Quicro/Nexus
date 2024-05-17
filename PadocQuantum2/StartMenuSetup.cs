using System.Diagnostics;

namespace PadocQuantum2 {
    [DebuggerDisplay("{Text} [{Childs.Count}] [{Authorized? \"Authorized\" : \"Unuthorized\"}] [{Show? \"Shown\" : \"Hidden\"}]")]
    public class MenuItem {

        public string Text { get; set; } = string.Empty;
        public List<string> Permissions { get; set; } = new List<string>();
        public List<MenuItem> Childs { get; set; } = new List<MenuItem>();
        public bool Authorized { get; private set; }
        public bool Show { get; internal set; }
        public void Click()
        {
            throw new NotImplementedException();
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
            var count = Permissions.Count();
            var intersectedCount = Permissions.Intersect(currentPermissions).Count();

            var isAdmin = currentPermissions.Any(p => p == "ALL");


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

        public static List<MenuItem> getDefaultMenuStructure() {
            return new List<MenuItem>() {
                new MenuItem("Beheer") {
                    Childs = new List<MenuItem> {
                        new MenuItem("Polissen", "StartMenu.Dossier.Polis"),
                        new MenuItem("Schadegevallen", "StartMenu.Dossier.Schade"),
                        new MenuItem("Klanten", "StartMenu.Dossier.Schade", "StartMenu.Dossier.Polis")
                    }
                },
                new MenuItem("Administrator", "StartMenu.Admin") {
                    Childs = new List<MenuItem> {
                        new MenuItem("Database", "StartMenu.Database"),
                        new MenuItem("Gebruikers"),
                        new MenuItem("Rollen"),
                        new MenuItem("Permissies")
                    }
                }
            };
        }
    }
}
