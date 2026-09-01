// This file provides polyfills for compiler-support types not available on net472: IsExternalInit (needed for
// this project's `record` test-fixture types) and ModuleInitializerAttribute (needed by TUnit's own generated
// module-initializer code, which is compiled as part of this project).

#if !NET5_0_OR_GREATER
namespace System.Runtime.CompilerServices
{
    /// <summary>Polyfill of <see cref="IsExternalInit" />, introduced in .NET 5.0, needed to support init/record syntax.</summary>
    internal static class IsExternalInit;

    /// <summary>Polyfill of <see cref="ModuleInitializerAttribute" />, introduced in .NET 5.0.</summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    internal sealed class ModuleInitializerAttribute : Attribute;
}
#endif