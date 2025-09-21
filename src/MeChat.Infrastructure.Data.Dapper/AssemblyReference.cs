using System.Reflection;

namespace MeChat.Infrastructure.Dapper;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
