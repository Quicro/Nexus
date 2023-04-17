using Microsoft.EntityFrameworkCore;
using PadocEF.Models;

namespace PadocEF.Extentions {
    public static class UserExtentions {
        public static User Insert(string name, string email) {
            User newUser = new() {
                Name = name,
                Email = email
            };

            return newUser;
        }

        public static User Get(int id) {
            return DatabaseManager.context.User.SingleOrDefault(u => u.Id == id);
        }

        public static List<User> GetByName(string name) {
            return DatabaseManager.context.User.Where(u => u.Name == name).ToList();
        }
        public static User? validate(string username, string password) {
            User? user = DatabaseManager.context.User.FirstOrDefault(u =>
                u.Name == username &&
                u.Password == password
            );
            return user;
        }

        public static bool userHasPermissions(User user, params string[] permissionNames) {
            IQueryable<UserRole> queryable = DatabaseManager.context.UserRole
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .ThenInclude(r => r.RolePermission)
                .ThenInclude(rp => rp.Permission)
                .Where(
                    ur => ur.User == user &&
                    ur.Role.RolePermission.Where(
                        rp => permissionNames.Contains(rp.Permission.Name)
                    ).Any()
                );

            /*var queryString = queryable.ToQueryString();

            var userRoles = queryable.ToList();*/

            return queryable.Any();
        }
    }

    public static class ClientExtentions {
        public static Client Insert(string name, string number) {
            Client newClient = new() {
                Name = name,
                Number = number
            };

            return newClient;
        }

        public static Client get(int ID) => DatabaseManager.context.Client.Single(obj => obj.Id == ID);
        public static List<Client> get(string name) => DatabaseManager.context.Client.Where(obj => obj.Name == name).ToList();
    }

    public static class PolicyExtensions {
        public static Policy CreatePolicy(string number = "", string name = "", Client client = null) {
            Policy newPolicy = new Policy {
                Number = number,
                Name = name,
                Client = client
            };

            return newPolicy;
        }

        public static Policy? GetPolicyById(int id, bool includeClient = false) {
            var queryable = DatabaseManager.context.Policy.AsQueryable();

            if (includeClient)
                queryable = queryable.Include(p => p.Client);
                
            return queryable.SingleOrDefault(p => p.Id == id);
        }

        public static List<Policy> GetPoliciesByClient(Client client, bool includeClient = false) {
            var queryable = DatabaseManager.context.Policy.AsQueryable();

            if (includeClient)
                queryable = queryable.Include(p => p.Client);

            return queryable.Where(p => p.Client == client).ToList();
        }

        public static List<Policy> GetPoliciesByClaim(Claim claim, bool includeClaim = false) {
            var queryable = DatabaseManager.context.Policy.AsQueryable();

            if (includeClaim)
                queryable = queryable.Include(p => p.Claim);

            return queryable.Where(p => p.Claim == claim).ToList();
        }
    }


}