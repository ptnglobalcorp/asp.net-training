namespace AwesomeBlog.Core.Entities.Common
{
    public abstract class AuditedEntity : DateEntity, IAuditedEntity
    {
        public string CreatedBy { get; set; }

        public string LastModifiedBy { get; set; }
    }
}
