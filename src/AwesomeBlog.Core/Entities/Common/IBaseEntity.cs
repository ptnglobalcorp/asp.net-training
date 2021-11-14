namespace AwesomeBlog.Core.Entities.Common
{
    public interface IBaseEntity
    {
        public string Id { get; set; }

        public int ClusteredKey { get; set; }
    }
}
