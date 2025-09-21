using System.Reflection;

namespace MeChat.Infrastructure.MessageBroker.Consumer;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
