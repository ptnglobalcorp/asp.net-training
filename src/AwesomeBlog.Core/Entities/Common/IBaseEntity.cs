namespace AwesomeBlog.Core.Entities.Common
{
    public interface IBaseEntity
    {
        string Id { get; set; }

        int ClusteredKey { get; set; }
    }
}
