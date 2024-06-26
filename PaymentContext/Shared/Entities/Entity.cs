using Flunt.Notifications;

namespace PaymentContext.Shared.Entites;

public abstract class Entity : Notifiable
{
    public Entity()
    {
        ID = Guid.NewGuid();
    }
    public Guid ID { get; private set; }
}
