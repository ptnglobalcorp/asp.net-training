using System;

namespace AwesomeBlog.Core.Entities.Common
{
    public abstract class DateEntity : BaseEntity, IDateEntity
    {
        public DateTimeOffset CreatedAt { get; set; }
        
        public DateTimeOffset? LastModifiedAt { get; set; }
    }
}
