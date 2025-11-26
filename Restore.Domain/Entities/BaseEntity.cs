namespace Restore.Domain.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public Guid Xid { get; set; } = Guid.NewGuid();
    public bool IsDeleted { get; set; } = false;
}
