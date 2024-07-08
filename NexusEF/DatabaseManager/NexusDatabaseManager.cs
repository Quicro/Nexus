using Microsoft.EntityFrameworkCore;
using NexusEF.Models.Insurance.Context;
using NexusEF.Models.Mevino.Context;
using NexusEF.Models.Nexus.Context;

namespace NexusEF.DatabaseManager {

    public enum DatabaseManagerType { 
        Nexus, App
    }

    public class MevinoDatabaseManager : DatabaseManager<MevinoContextInMemory>, IAppDatabaseManager<MevinoDatabaseManager> {
        public Helper Helper { get { return GenericHelper; } }
        public Helper<MevinoDatabaseManager> GenericHelper { get; }
    }

    public class InsuranceDatabaseManager : DatabaseManager<InsuranceContext>, IAppDatabaseManager<InsuranceDatabaseManager> {
        public Helper<InsuranceDatabaseManager> GenericHelper { get; }

        public Helper Helper { get { return GenericHelper; } }
    }

    public class NexusDatabaseManager : DatabaseManager<NexusContext>, INexusDatabaseManager<NexusDatabaseManager> {
        public Helper<NexusDatabaseManager> GenericHelper { get; }

        public Helper Helper { get { return GenericHelper; } }
    }

    public interface IDatabaseManager {
        public DbContext context { get; set; }
        public Helper Helper { get; }

    }


    public interface IAppDatabaseManager : IDatabaseManager {
    }
    public interface IAppDatabaseManager<T> : IAppDatabaseManager where T : IDatabaseManager, new () {
        public Helper<T> GenericHelper { get; }
    }

    public interface INexusDatabaseManager : IDatabaseManager {
    }
    public interface INexusDatabaseManager<T> : INexusDatabaseManager where T : IDatabaseManager, new() {
        public Helper<T> GenericHelper { get; }
    }
}
