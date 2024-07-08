
using NexusEF.Models.Nexus;

namespace NexusCore {
    internal class TableExtention {
        internal Func<U, List<T>> getFunction<U, T>() where U : INexusEntity, new() where T : INexusEntity, new() {
            throw new NotImplementedException();
        }

        internal void getFunction<T>() {
            throw new NotImplementedException();
        }
    }
}