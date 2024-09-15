using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using static Libstapsdt.Libstapsdt;

namespace DynamicProbes;

public interface ILoadedProvider : IDisposable
{
    Provider Unload();
}

public sealed partial class Provider : ILoadedProvider
{
    nint ptr;

    Provider(nint ptr, string name)
    {
        this.ptr = ptr;
        Name = name;
    }

    public static Provider Init(string name)
    {
        var ptr = ProviderInit(name);
        if (ptr == 0)
            throw new ProviderException($"Provider initialization failed: {name}");

        try
        {
            return new Provider(ptr, name);
        }
        catch
        {
            ProviderDestroy(ptr);
            throw;
        }
    }

    public string Name { get; }

    public override string ToString() => Name;

    public ILoadedProvider Load() => Load(This);

    internal bool IsLoaded { get; private set; }

#pragma warning disable CA1859 // Use concrete types when possible for improved performance (by-design)
    static ILoadedProvider Load(Provider provider)
#pragma warning restore CA1859 // Use concrete types when possible for improved performance
    {
        if (provider.IsLoaded)
            return provider;

        if (ProviderLoad(provider.ptr) != 0)
            throw new ProviderException($"Provider loading failed: {provider.Name}");

        provider.IsLoaded = true;
        return provider;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Destroy();
    }

    Provider ILoadedProvider.Unload() => Unload(This);

    static Provider Unload(Provider provider)
    {
        if (!provider.IsLoaded)
            throw new InvalidOperationException();

        if (ProviderUnload(provider.ptr) != 0)
            throw new ProviderException($"Provider unloading failed: {provider.Name}");

        provider.IsLoaded = false;
        return provider;
    }

    ~Provider() => Destroy();

    void Destroy()
    {
        if (this.ptr == 0)
            return;

        if (IsLoaded)
        {
            IsLoaded = false;

            // This member is called from either "Dispose" or the finalizer, so exceptions cannot be
            // thrown and any error in unloading has to be ignored.

            if (ProviderUnload(this.ptr) != 0)
                Debug.WriteLine($"Provider unloading failed: {Name}");
        }

        ProviderDestroy(this.ptr);
        this.ptr = 0;
    }

    Provider This
    {
        get
        {
            if (this.ptr == 0)
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
