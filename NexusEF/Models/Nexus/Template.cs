//WILL NOT BE COMPILED
using NexusEF.Attributes;

namespace NexusEF.Models.Nexus;

[Nexus]
namespace NexusEF.Models.Nexus.Template
{
    public class Template
    {
        //Id
        public int Id { get; set; }
        public int Name { get; set; }

        //fielf
        public float? Price { get; set; }

        //one to many
        public int? OrderId { get; set; }
        public virtual Order? Order { get; set; }

        //many to one
        public virtual ICollection<Claim> Claim { get; set; } = new List<Claim>();
    }
}