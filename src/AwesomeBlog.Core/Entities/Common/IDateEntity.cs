namespace AwesomeBlog.Core.Entities.Common
{
    public interface IDateEntity
    {
        DateTimeOffset CreatedAt { get; set; }

        DateTimeOffset? LastModifiedAt { get; set; }
    }
}
