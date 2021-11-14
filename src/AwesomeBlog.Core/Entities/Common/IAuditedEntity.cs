namespace AwesomeBlog.Core.Entities.Common
{
    public interface IAuditedEntity
    {
        public string CreatedBy { get; set; }

        public string LastModifiedBy { get; set;}
    }
}
