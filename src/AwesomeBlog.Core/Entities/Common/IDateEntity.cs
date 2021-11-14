namespace AwesomeBlog.Core.Entities.Common
{
    public interface IDateEntity
    {
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? LastModifiedAt { get; set; }
    }
}
