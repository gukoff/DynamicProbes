using DynamicProbes;
using Libstapsdt;
using UnitTests.Mocking.Methods;

namespace UnitTests;

#pragma warning disable CA1030 // Use events where appropriate

public sealed class ProbeTests : IDisposable
{
    readonly Provider provider = Provider.Fake(42);

    public void Dispose()
    {
        this.provider.Dispose();
    }

    [Fact]
    public void Fire_When_Unloaded_Has_No_Effect()
    {
        using var handlers = Libstapsdt.Libstapsdt.Handlers = new()
        {
            ProviderInit = { Handler = DefaultLibstapsdtHandlers.ProviderInit.Return(this.provider) },
            ProviderDestroy = { Handler = DefaultLibstapsdtHandlers.UnverifiedProviderDestroy },
            ProviderAddProbe = { Handler = DefaultLibstapsdtHandlers.ProviderAddProbe.Return(4242) },
        };

        var probe = Provider.Init("foo").AddProbe("bar");

        probe.Fire();
    }

    [Fact]
    public void Fire_Fires()
    {
        using var handlers = Libstapsdt.Libstapsdt.Handlers = new()
        {
            ProviderInit = { Handler = DefaultLibstapsdtHandlers.ProviderInit.Return(this.provider) },
            ProviderDestroy = { Handler = DefaultLibstapsdtHandlers.UnverifiedProviderDestroy },
            ProviderAddProbe = { Handler = DefaultLibstapsdtHandlers.ProviderAddProbe.Return(4242) },
            ProviderLoad = { Handler = DefaultLibstapsdtHandlers.ProviderLoad.Return(0) },
            ProviderUnload = { Handler = DefaultLibstapsdtHandlers.ProviderUnload.Return(0) },
            ProbeFire = { Handler = DefaultLibstapsdtHandlers.ProbeFire.Expect(new(4242, [])).Return(default) },
        };

        var provider = Provider.Init("foo");
        var probe = provider.AddProbe("bar");
        _ = provider.Load();

        probe.Fire();
    }

    [Fact]
    public void Fire_While_Provider_Is_Unloaded_Has_No_Effect()
    {
        using var handlers = Libstapsdt.Libstapsdt.Handlers = new()
        {
            ProviderInit = { Handler = DefaultLibstapsdtHandlers.ProviderInit.Return(this.provider) },
            ProviderDestroy = { Handler = DefaultLibstapsdtHandlers.UnverifiedProviderDestroy },
            ProviderAddProbe = { Handler = DefaultLibstapsdtHandlers.ProviderAddProbe.Return(4242) },
            ProviderLoad = { Handler = DefaultLibstapsdtHandlers.ProviderLoad.Return(0) },
            ProviderUnload = { Handler = DefaultLibstapsdtHandlers.ProviderUnload.Return(0) },
            ProbeFire = { Handler = DefaultLibstapsdtHandlers.ProbeFire.Expect(new(4242, [])).Return(default) },
        };

        var provider = Provider.Init("foo");
        var probe = provider.AddProbe("bar");

        var loadedProvider = provider.Load();
        probe.Fire();

        _ = loadedProvider.Unload();
        probe.Fire();

        _ = provider.Load();
        probe.Fire();

        Assert.Equal(2, Libstapsdt.Libstapsdt.Handlers.ProbeFire.InvocationCount);
    }
}
