using Microsoft.EntityFrameworkCore;
using NexusCore.BigForms;
using NexusCore.Controllers;
using NexusCore.Interfaces;
using NexusLogging;
using NexusEF;
using NexusEF.Extentions;
using NexusEF.Models;

namespace NexusCore
{
    public class NexusBuilder
    {
        IMainForm mainform;
        public List<MenuItem> menuItems;
        public User currentUser;
        public List<Permission>? currentPermissions;
        private IQueryable<User> userQuery;

        public NexusApp Build()
        {
            try
            {
                if (userQuery == null)
                {
                    currentUser = DatabaseManager.context.User
                         .Where(u => u.Name == "Q")
                         .Include(u => u.UserRole)
                         .ThenInclude(ur => ur.Role)
                         .ThenInclude(r => r.RolePermission)
                         .ThenInclude(rp => rp.Permission)
                         .Single();
                }
                else
                {
                    currentUser = userQuery.Single();
                }

                currentPermissions = UserExtention.getPermissions(currentUser);

                menuItems = RemoveUnauthorizedMenuItems(currentPermissions.Select(p => p.Name), menuItems);

                string dateTime = $"{DateTime.Now:HH:mm:ss dd/MM/yy}";

                //Logger.logTitle("Nexus");
                Logger.logHeader($"Started at {dateTime}    \n");

                mainform.Open();
                Logger.ApplicationStarted();

                mainform.SetUpStartMenu(menuItems);

                mainform.Start(menuItems);


            }
            catch (Exception e)
            {
                Logger.error(e.Message);
                Logger.ApplicationCrashed();
                throw e;
            }

            return new NexusApp();
        }

        /// <summary>
        /// Hides unauthorized menu items from a list of menu items based on the user's permissions.
        /// </summary>
        /// <param name="currentPermissions">A list of permissions associated with the current user.</param>
        /// <param name="menuItems">The list of menu items to filter based on permissions.</param>
        /// <param name="IncludeUnauthorizedToo">Indicates whether to include unauthorized menu items in the result.</param>
        /// <returns>A filtered list of menu items that the user is authorized to see.</returns>
        /// <remarks>
        /// This method iterates through a list of menu items and checks their authorization status
        /// based on the user's permissions. Menu items that are unauthorized are removed from the list.
        /// The authorization status of each menu item is determined by calling the <see cref="MenuItem.setAuthorized"/>
        /// method. By default, only authorized menu items are included in the result. Set <paramref name="IncludeUnauthorizedToo"/>
        /// to true if you want to include unauthorized menu items in the result.
        /// </remarks>
        /// <param name="currentPermissions">The list of permissions associated with the current user.</param>
        /// <param name="menuItems">The list of menu items to filter based on permissions.</param>
        /// <param name="IncludeUnauthorizedToo">Indicates whether to include unauthorized menu items in the result.</param>
        /// <returns>A filtered list of menu items that the user is authorized to see.</returns>
        private List<MenuItem> RemoveUnauthorizedMenuItems(IEnumerable<string>? currentPermissions, List<MenuItem> menuItems, bool IncludeUnauthorizedToo = false)
        {
            List<MenuItem> returnList = new List<MenuItem>();

            foreach (MenuItem menuItem in menuItems)
            {
                menuItem.setAuthorized(currentPermissions);

                if (menuItem.Authorized || IncludeUnauthorizedToo)
                {
                    RemoveUnauthorizedMenuItems(currentPermissions, menuItem.Childs);
                    menuItem.Show = menuItem.Childs.Any(mi => mi.Authorized) || menuItem.Authorized;
                }

                returnList.Add(menuItem);
            }

            return returnList;
        }

        public void EndApp()
        {
            try
            {
                mainform.End();
                mainform.Close();

                Logger.ApplicationEnded();
            }
            catch (Exception e)
            {
                Logger.error(e.Message);
                Logger.ApplicationCrashed();
                throw e;
            }
        }

        public static void Crash(Exception exception)
        {
            throw exception;
        }

        public static void doMyFirstPacket()
        {
            var sender = new PacketSender();
            IPacketReceiver viewerController = (IPacketReceiver)Activator.CreateInstance(typeof(ViewerController));
            PacketType packet = new PacketType(typeof(Policy));


            sender.send(viewerController, packet);
        }

        public static void doMySecondPacket(int policyID)
        {
            var sender = new PacketSender();
            IPacketReceiver editorController = (IPacketReceiver)Activator.CreateInstance(typeof(EditorController));
            Packet packet = (Packet)Activator.CreateInstance(typeof(PacketSingle));

            packet.query = DatabaseManager.context.Policy.Where(p => p.Id == policyID);


            sender.send(editorController, packet);
        }

        public NexusBuilder setMainForm(IMainForm mainForm)
        {

            this.mainform = mainForm;

            return this;
        }

        public NexusBuilder setViewerController(object viewerController)
        {  //WIP

            //this.viewerController = viewerController;

            return this;
        }

        public NexusBuilder setEditorController(object editorController)
        {  //WIP

            //this.editorController = editorController;

            return this;
        }

        public NexusBuilder setMenuConfig(List<MenuItem> menuItem)
        {

            this.menuItems = menuItem;

            return this;
        }

        public NexusBuilder setUser(string username, string password)
        {

            this.userQuery = DatabaseManager.context.User
                         .Where(user => user.Name == username && user.Password == password)
                         .Include(u => u.UserRole)
                         .ThenInclude(ur => ur.Role)
                         .ThenInclude(r => r.RolePermission)
                         .ThenInclude(rp => rp.Permission);
            return this;
        }
    }
}
