using System.Reflection;

namespace MeChat.Infrastructure.DistributedCache;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
