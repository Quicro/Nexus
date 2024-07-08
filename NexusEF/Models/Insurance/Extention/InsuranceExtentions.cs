using NexusEF.DatabaseManager;
using NexusEF.Extentions;


namespace NexusEF.Models.Insurance.Extention {

    /// <summary>
    /// The Extentions class provides a set of static methods for querying entities in the database.
    /// These methods are used to retrieve related entities based on the type of the entity and the method name.
    /// </summary>
    public class InsuranceExtentions : DatabaseManagerExtention<InsuranceDatabaseManager> {

        /*public sealed class ClaimExtention : ClassExtentions<Claim> {
            public static IQueryable<Policy> getPolicyQueryable(Claim claim) {
                IQueryable<Policy> queryable = AppDatabaseManager.context.Claim
                    .Where(c => c == claim)
                    .Include(c => c.Policy)
                    .Select(c => c.Policy);

                return queryable;
            }

            public static IQueryable<Policy> getPolicyQueryable(int claimID) {
                IQueryable<Policy> queryable = AppDatabaseManager.context.Claim
                    .Where(c => c.Id == claimID)
                    .Include(c => c.Policy)
                    .Select(c => c.Policy);

                return queryable;
            }
        }

        public sealed class ClientExtention : ClassExtentions<Client> {
            public static IQueryable<Policy> getPolicyQueryable(Client client) {
                IQueryable<Policy> queryable = AppDatabaseManager.context.Policy
                    .Where(p => p.Client == client);
                return queryable;
            }
        }

        public sealed class PolicyExtention : Extentions<Policy> {
            public static IQueryable<Client> getClientQueryable(Policy policy) {
                IQueryable<Client> queryable = AppDatabaseManager.context.Policy
                    .Include(policy => policy.Client)
                    .Where(p => p == policy)
                    .Select(policy => policy.Client);

                return queryable;
            }

            public static IQueryable<Claim> getClaimsQueryable(Policy policy) {
                IQueryable<Claim> queryable = AppDatabaseManager.context.Claim
                    .Where(c => c.Policy == policy);

                return queryable;
            }
        }*/
    }
}