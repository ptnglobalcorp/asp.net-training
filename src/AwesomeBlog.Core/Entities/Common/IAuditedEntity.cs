namespace AwesomeBlog.Core.Entities.Common
{
    public interface IAuditedEntity
    {
        string CreatedBy { get; set; }

        string LastModifiedBy { get; set;}
    }
}
