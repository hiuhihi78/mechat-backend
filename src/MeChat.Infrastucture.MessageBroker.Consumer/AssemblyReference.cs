using System.Reflection;

namespace MeChat.Infrastucture.MessageBroker.Consumer;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
