using System.Collections.Generic;
using AwesomeBlog.Core.Entities.Common;

namespace AwesomeBlog.Core.Entities
{
    public class Category : DateEntity, ISoftDelete
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}
