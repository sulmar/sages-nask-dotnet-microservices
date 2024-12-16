namespace ProductCatalog.Domain.Entities;

internal class Category : BaseEntity 
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Category(int id, string name, string description)
        :base(id)
    {
        Name = name;
        Description = description;
    }
}
