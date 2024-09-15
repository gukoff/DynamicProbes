using DynamicProbes;
using Libstapsdt;
using static Libstapsdt.DefaultLibstapsdtHandlers;

namespace UnitTests;

#pragma warning disable CA2000 // Dispose objects before losing scope (unmocked)

public class ProviderTests
{
    [Fact]
    public void Init_Succeeds_When_ProviderInit_Indicates_Success()
    {
        Libstapsdt.Libstapsdt.Handlers = new()
        {
            ProviderInit = { Handler = ProviderInit.Expect(new("foo")).Return(42) },
        };

        var provider = Provider.Init("foo");

        Assert.NotNull(provider);
    }

    [Fact]
    public void Init_Throws_When_ProviderInit_Fails()
    {
        Libstapsdt.Libstapsdt.Handlers = new()
        {
            ProviderInit = { Handler = ProviderInit.Expect(new("foo")).Return(0) },
        };

        static void Act() => Provider.Init("foo");

        var ex = Assert.Throws<ProviderException>(Act);
        Assert.Equal("Provider initialization failed: foo", ex.Message);
    }

    [Fact]
    public void Init_Returns_Unloaded_Provider()
    {
        Libstapsdt.Libstapsdt.Handlers = new()
        {
            ProviderInit = { Handler = ProviderInit.Return(42) },
        };

        var provider = Provider.Init("foo");
        Assert.False(provider.IsLoaded);
    }

    [Fact]
    public void Init_Returns_Provider_With_Initialized_Name()
    {
        Libstapsdt.Libstapsdt.Handlers = new()
        {
            ProviderInit = { Handler = ProviderInit.Return(42) },
        };

        var provider = Provider.Init("foo");
        Assert.Equal("foo", provider.Name);
    }

    [Fact]
    public void ToString_Returns_Name()
    {
        Libstapsdt.Libstapsdt.Handlers = new()
        {
            ProviderInit = { Handler = ProviderInit.Return(42) },
        };

        var provider = Provider.Init("foo");
        Assert.Equal("foo", provider.ToString());
    }

    [Fact]
    public void Dispose_Destroys()
    {
        Libstapsdt.Libstapsdt.Handlers = new()
        {
            ProviderInit = { Handler = ProviderInit.Return(42) },
            ProviderDestroy = { Handler = ProviderDestroy.Expect(new(42)).Return(default) },
        };

        Provider.Init("foo").Dispose();
    }

    [Fact]
    public void Disposed_Destroys_Once()
    {
        LibstapsdtHandlers handlers;
        Libstapsdt.Libstapsdt.Handlers = handlers = new()
        {
            ProviderInit = { Handler = ProviderInit.Return(42) },
            ProviderDestroy = { Handler = ProviderDestroy.Expect(new(42)).Return(default) },
        };

        var provider = Provider.Init("foo");
        provider.Dispose();
        provider.Dispose();

        Assert.Equal(1, handlers.ProviderDestroy.InvocationCount);
    }

    [Fact]
    public void Load_Loads_Provider()
    {
        Libstapsdt.Libstapsdt.Handlers = new()
        {
            ProviderInit = { Handler = ProviderInit.Return(42) },
            ProviderLoad = { Handler = ProviderLoad.Return(0) },
        };

        var provider = Provider.Init("foo");
        var result = provider.Load();

        Assert.Same(provider, result);
    }

    [Fact]
    public void Unload_Unloads_Provider()
    {
        Libstapsdt.Libstapsdt.Handlers = new()
        {
            ProviderInit = { Handler = ProviderInit.Return(42) },
            ProviderLoad = { Handler = ProviderLoad.Return(0) },
            ProviderUnload = { Handler = ProviderUnload.Expect(new(42)).Return(0) },
        };

        var provider = Provider.Init("foo");
        var result = provider.Load().Unload();

        Assert.Same(provider, result);
    }

    [Fact]
    public void Unload_Fails_When_Unloaded()
    {
        Libstapsdt.Libstapsdt.Handlers = new()
        {
            ProviderInit = { Handler = ProviderInit.Return(42) },
            ProviderLoad = { Handler = ProviderLoad.Return(0) },
            ProviderUnload = { Handler = ProviderUnload.Expect(new(42)).Return(0) },
        };

        var provider = Provider.Init("foo");
        var loadedProvider = provider.Load();
        _ = loadedProvider.Unload();

        void Act() => loadedProvider.Unload();

        _ = Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact]
    public void AddProbe_Returns_Initialized_Probe()
    {
        Libstapsdt.Libstapsdt.Handlers = new()
        {
            ProviderInit = { Handler = ProviderInit.Return(42) },
            ProviderAddProbe = { Handler = ProviderAddProbe.Expect(new(42, "bar", [])).Return(4242) },
        };

        var provider = Provider.Init("foo");
        var probe = provider.AddProbe("bar");

        Assert.Equal("bar", probe.Name);
        Assert.Equal("foo:bar", probe.ToString());
    }

    [Fact]
    public void AddProbe_With_1_Arg_Returns_Initialized_Probe()
    {
        Libstapsdt.Libstapsdt.Handlers = new()
        {
            ProviderInit = { Handler = ProviderInit.Return(42) },
            ProviderAddProbe = { Handler = ProviderAddProbe.Expect(new(42, "bar", [ArgType.Int32])).Return(4242) },
        };

        var provider = Provider.Init("foo");
        var probe = provider.AddProbe<Int32Arg>("bar");

        Assert.Equal("bar", probe.Name);
        Assert.Equal("foo:bar", probe.ToString());
    }
}
