using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using static Libstapsdt.Libstapsdt;

namespace DynamicProbes;

public interface ILoadedProvider : IDisposable
{
    Provider Unload();
}

public sealed partial class Provider : SafeHandle, ILoadedProvider
{
    string? name;

    public Provider() : this(0) { }

    Provider(nint ptr) : base(ptr, ownsHandle: true) { }

    public static Provider Init(string name)
    {
        var provider = ProviderInit(name);
        if (provider.IsInvalid)
            throw new ProviderException($"Provider initialization failed: {name}");
        provider.Name = name;
        return provider;
    }

    public string Name
    {
        get => this.name ?? string.Empty;
        private set => this.name = value;
    }

    public override string ToString() => Name;

    public ILoadedProvider Load() => Load(This);

    internal bool IsLoaded { get; private set; }

#pragma warning disable CA1859 // Use concrete types when possible for improved performance (by-design)
    static ILoadedProvider Load(Provider provider)
#pragma warning restore CA1859 // Use concrete types when possible for improved performance
    {
        if (provider.IsLoaded)
            return provider;

        if (ProviderLoad(provider) != 0)
            throw new ProviderException($"Provider loading failed: {provider.Name}");

        provider.IsLoaded = true;
        return provider;
    }

    Provider ILoadedProvider.Unload() => Unload(This);

    static Provider Unload(Provider provider)
    {
        if (!provider.IsLoaded)
            throw new InvalidOperationException();

        if (ProviderUnload(provider) != 0)
            throw new ProviderException($"Provider unloading failed: {provider.Name}");

        provider.IsLoaded = false;
        return provider;
    }

    public override bool IsInvalid => this.handle == 0;

    protected override bool ReleaseHandle()
    {
        if (IsLoaded)
        {
            IsLoaded = false;

            // This member is called from either "Dispose" or the finalizer, so exceptions cannot be
            // thrown and any error in unloading has to be ignored.

            if (ProviderUnload(this.handle) != 0)
                Debug.WriteLine($"Provider unloading failed: {Name}");
        }

        ProviderDestroy(this.handle);
        return true;
    }

    Provider This
    {
        get
        {
            if (IsInvalid)
                ThrowObjectDisposedException();
            return this;
        }
    }

    /// <remarks>
    /// Separating the throw into a separate method allows the JIT to optimize the method.
    /// </remarks>
    [DoesNotReturn]
    static void ThrowObjectDisposedException() =>
        throw new ObjectDisposedException(nameof(Provider));
}

public class ProviderException(string? message, Exception? inner) :
    Exception(message, inner)
{
    public ProviderException() : this(null) { }
    public ProviderException(string? message) : this(message, null) { }
}
