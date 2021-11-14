using AwesomeBlog.Core.Entities.Common;

namespace AwesomeBlog.Core.Entities
{
    public class Post : AuditedEntity, ISoftDelete
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public virtual Category Category { get; set; }

        public string CategoryId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
