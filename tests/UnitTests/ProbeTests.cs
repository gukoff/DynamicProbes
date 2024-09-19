using DynamicProbes;
using Libstapsdt;
using UnitTests.Mocking.Methods;

namespace UnitTests;

#pragma warning disable CA1030 // Use events where appropriate

public class ProbeTests
{
    [Fact]
    public void Fire_When_Unloaded_Has_No_Effect()
    {
        Libstapsdt.Libstapsdt.Handlers = new()
        {
#pragma warning disable CA2000 // Dispose objects before losing scope (unmocked)
            ProviderInit = { Handler = DefaultLibstapsdtHandlers.ProviderInit.Return(Provider.Fake(42)) },
#pragma warning disable CA2000 // Dispose objects before losing scope
            ProviderAddProbe = { Handler = DefaultLibstapsdtHandlers.ProviderAddProbe.Return(4242) },
        };

#pragma warning disable CA2000 // Dispose objects before losing scope (unmocked)
        var provider = Provider.Init("foo");
#pragma warning restore CA2000 // Dispose objects before losing scope

        var probe = provider.AddProbe("bar");
        probe.Fire();
    }

    [Fact]
    public void Fire_Fires()
    {
        Libstapsdt.Libstapsdt.Handlers = new()
        {
#pragma warning disable CA2000 // Dispose objects before losing scope (unmocked)
            ProviderInit = { Handler = DefaultLibstapsdtHandlers.ProviderInit.Return(Provider.Fake(42)) },
#pragma warning disable CA2000 // Dispose objects before losing scope
            ProviderAddProbe = { Handler = DefaultLibstapsdtHandlers.ProviderAddProbe.Return(4242) },
            ProviderLoad = { Handler = DefaultLibstapsdtHandlers.ProviderLoad.Return(0) },
            ProbeFire = { Handler = DefaultLibstapsdtHandlers.ProbeFire.Expect(new(4242, [])).Return(default) },
        };

#pragma warning disable CA2000 // Dispose objects before losing scope (unmocked)
        var provider = Provider.Init("foo");
#pragma warning restore CA2000 // Dispose objects before losing scope
        var probe = provider.AddProbe("bar");
        _ = provider.Load();

        probe.Fire();
    }

    [Fact]
    public void Fire_While_Provider_Is_Unloaded_Has_No_Effect()
    {
        Libstapsdt.Libstapsdt.Handlers = new()
        {
#pragma warning disable CA2000 // Dispose objects before losing scope (unmocked)
            ProviderInit = { Handler = DefaultLibstapsdtHandlers.ProviderInit.Return(Provider.Fake(42)) },
#pragma warning disable CA2000 // Dispose objects before losing scope
            ProviderAddProbe = { Handler = DefaultLibstapsdtHandlers.ProviderAddProbe.Return(4242) },
            ProviderLoad = { Handler = DefaultLibstapsdtHandlers.ProviderLoad.Return(0) },
            ProviderUnload = { Handler = DefaultLibstapsdtHandlers.ProviderUnload.Return(0) },
            ProbeFire = { Handler = DefaultLibstapsdtHandlers.ProbeFire.Expect(new(4242, [])).Return(default) },
        };

#pragma warning disable CA2000 // Dispose objects before losing scope (unmocked)
        var provider = Provider.Init("foo");
#pragma warning restore CA2000 // Dispose objects before losing scope
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
