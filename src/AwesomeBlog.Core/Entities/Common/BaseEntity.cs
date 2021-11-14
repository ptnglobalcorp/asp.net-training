namespace AwesomeBlog.Core.Entities.Common
{
    public abstract class BaseEntity : IBaseEntity
    {
        public string Id { get; set; }

        public int ClusteredKey { get; set; }
    }
}
