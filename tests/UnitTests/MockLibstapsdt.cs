using DynamicProbes;
using UnitTests;
using UnitTests.Mocking.Methods;

#pragma warning disable IDE0130 // Namespace does not match folder structure (by-design)
namespace Libstapsdt;
#pragma warning restore IDE0130 // Namespace does not match folder structure

sealed record ProviderInitArgs(string Name);
sealed record ProviderDestroyArgs(nint Provider);
sealed record ProviderLoadArgs(Provider Provider);
sealed record ProviderUnloadArgs(nint Provider);
sealed record ProbeIsEnabledArgs(nint Probe);
sealed record ProviderAddProbeArgs(Provider Provider, string Name, EquatableArray<Enum<ArgType>> Args);
sealed record ProbeFireArgs(nint Probe, EquatableArray<long> Args);

sealed class LibstapsdtHandlers
{
    public HandlerCell<ProviderInitArgs, Provider> ProviderInit { get; init; } = new(nameof(ProviderInit));
    public HandlerCell<ProviderDestroyArgs, Unit> ProviderDestroy { get; init; } = new(nameof(ProviderDestroy));

    public HandlerCell<ProviderLoadArgs, int> ProviderLoad { get; init; } = new(nameof(ProviderLoad));
    public HandlerCell<ProviderUnloadArgs, int> ProviderUnload { get; init; } = new(nameof(ProviderUnload));

    public HandlerCell<ProbeIsEnabledArgs, bool> ProbeIsEnabled { get; init; } = new(nameof(ProbeIsEnabled));
    public HandlerCell<ProviderAddProbeArgs, nint> ProviderAddProbe { get; init; } = new(nameof(ProviderAddProbe));
    public HandlerCell<ProbeFireArgs, Unit> ProbeFire { get; init; } = new(nameof(ProbeFire));
}

static class DefaultLibstapsdtHandlers
{
    public static readonly INeverHandler<ProviderInitArgs, Provider> ProviderInit = Handler.Never<ProviderInitArgs, Provider>(nameof(ProviderInit));

    public static readonly INeverHandler<ProviderDestroyArgs, Unit> ProviderDestroy = Handler.Never<ProviderDestroyArgs, Unit>(nameof(ProviderDestroy));

    public static readonly INeverHandler<ProviderLoadArgs, int> ProviderLoad = Handler.Never<ProviderLoadArgs, int>(nameof(ProviderLoad));
    public static readonly INeverHandler<ProviderUnloadArgs, int> ProviderUnload = Handler.Never<ProviderUnloadArgs, int>(nameof(ProviderUnload));

    public static readonly INeverHandler<ProbeIsEnabledArgs, bool> ProbeIsEnabled = Handler.Never<ProbeIsEnabledArgs, bool>(nameof(ProbeIsEnabled));
    public static readonly INeverHandler<ProviderAddProbeArgs, nint> ProviderAddProbe = Handler.Never<ProviderAddProbeArgs, nint>(nameof(ProviderAddProbe));
    public static readonly INeverHandler<ProbeFireArgs, Unit> ProbeFire = Handler.Never<ProbeFireArgs, Unit>(nameof(ProbeFire));
}

static class Libstapsdt
{
    [ThreadStatic]
    static LibstapsdtHandlers? handlers;

    public static LibstapsdtHandlers Handlers
    {
        get => handlers ??= new();
        set => handlers = value;
    }

    public static Provider ProviderInit(string name) => Handlers.ProviderInit.Invoke(new(name));
    public static void ProviderDestroy(nint provider) => Handlers.ProviderDestroy.Invoke(new(provider));

    public static int ProviderUnload(nint provider) => Handlers.ProviderUnload.Invoke(new(provider));
    public static int ProviderUnload(Provider provider) => Handlers.ProviderUnload.Invoke(new(provider.DangerousGetHandle()));
    public static int ProviderLoad(Provider provider) => Handlers.ProviderLoad.Invoke(new(provider));

    public static bool ProbeIsEnabled(nint probe) => Handlers.ProbeIsEnabled.Invoke(new(probe));

    public static nint ProviderAddProbe(Provider provider, string name) =>
        Handlers.ProviderAddProbe.Invoke(new(provider, name, []));
    public static nint ProviderAddProbe(Provider provider, string name, ArgType arg1) =>
        Handlers.ProviderAddProbe.Invoke(new(provider, name, [arg1]));
    public static nint ProviderAddProbe(Provider provider, string name, ArgType arg1, ArgType arg2) =>
        Handlers.ProviderAddProbe.Invoke(new(provider, name, [arg1, arg2]));
    public static nint ProviderAddProbe(Provider provider, string name, ArgType arg1, ArgType arg2, ArgType arg3) =>
        Handlers.ProviderAddProbe.Invoke(new(provider, name, [arg1, arg2, arg3]));
    public static nint ProviderAddProbe(Provider provider, string name, ArgType arg1, ArgType arg2, ArgType arg3, ArgType arg4) =>
        Handlers.ProviderAddProbe.Invoke(new(provider, name, [arg1, arg2, arg3, arg4]));
    public static nint ProviderAddProbe(Provider provider, string name, ArgType arg1, ArgType arg2, ArgType arg3, ArgType arg4, ArgType arg5) =>
        Handlers.ProviderAddProbe.Invoke(new(provider, name, [arg1, arg2, arg3, arg4, arg5]));
    public static nint ProviderAddProbe(Provider provider, string name, ArgType arg1, ArgType arg2, ArgType arg3, ArgType arg4, ArgType arg5, ArgType arg6) =>
        Handlers.ProviderAddProbe.Invoke(new(provider, name, [arg1, arg2, arg3, arg4, arg5, arg6]));


    public static void ProbeFire(nint probe) =>
        Handlers.ProbeFire.Invoke(new(probe, []));
    public static void ProbeFire(nint probe, long arg1) =>
        Handlers.ProbeFire.Invoke(new(probe, [arg1]));
    public static void ProbeFire(nint probe, long arg1, long arg2) =>
        Handlers.ProbeFire.Invoke(new(probe, [arg1, arg2]));
    public static void ProbeFire(nint probe, long arg1, long arg2, long arg3) =>
        Handlers.ProbeFire.Invoke(new(probe, [arg1, arg2, arg3]));
    public static void ProbeFire(nint probe, long arg1, long arg2, long arg3, long arg4) =>
        Handlers.ProbeFire.Invoke(new(probe, [arg1, arg2, arg3, arg4]));
    public static void ProbeFire(nint probe, long arg1, long arg2, long arg3, long arg4, long arg5) =>
        Handlers.ProbeFire.Invoke(new(probe, [arg1, arg2, arg3, arg4, arg5]));
    public static void ProbeFire(nint probe, long arg1, long arg2, long arg3, long arg4, long arg5, long arg6) =>
        Handlers.ProbeFire.Invoke(new(probe, [arg1, arg2, arg3, arg4, arg5, arg6]));
}
