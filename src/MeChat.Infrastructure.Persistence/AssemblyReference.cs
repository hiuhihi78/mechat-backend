using System.Reflection;

namespace MeChat.Infrastructure.Persistence;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
