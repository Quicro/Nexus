using Microsoft.EntityFrameworkCore;
using NexusEF.Models.Nexus;

namespace NexusEF.Extentions;

public class ClassExtentions<CT, ET> where CT : DbContext, new() where ET : class, INexusEntity {
    protected ClassExtentions<CT, ET> extentions = new();
    CT context;

    public IQueryable<ET> getQueryable(int ID)
    {
        return context.Set<ET>().Where(x => x.Id == ID);
    }

    public ET get(int ID)
    {
        return context.Set<ET>().Single(x => x.Id == ID);
    }
}