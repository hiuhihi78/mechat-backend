namespace MeChat.Domain.Abstractions.Enitites;
public abstract class EntityBase<TKey> : Entity, IEntityBase<TKey>
{
    public TKey Id { get; set; }
}
