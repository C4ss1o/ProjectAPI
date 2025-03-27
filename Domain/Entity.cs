using ProjectAPI.Domain.Products;

namespace ProjectAPI.Domain;

public abstract class Entity
{
    public Entity()
    {
        Id = Guid.NewGuid();

    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
    public bool HasStock { get; set; }
    public string CreateBy { get; set; }
    public DateTime CreateOn { get; set; }
    public string EditeBy { get; set; }
    public DateTime EditeOn { get; set; }
}
