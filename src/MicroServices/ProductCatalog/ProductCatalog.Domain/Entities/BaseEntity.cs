namespace ProductCatalog.Domain.Entities;

internal abstract class BaseEntity : Base
{
    public int Id { get; set; }

    protected BaseEntity(int id)
    {
        Id = id;
    }
}
