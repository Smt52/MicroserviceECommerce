namespace Messaging.Events;

public record IntegrationEvent
{
    public Guid Id { get; set; }
    public DateTime OccuredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName!;
}