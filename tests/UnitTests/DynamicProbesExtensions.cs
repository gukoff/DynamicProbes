namespace DynamicProbes;

partial class Provider
{
#pragma warning disable CA1720 // Identifier contains type name (by-design)
    public static Provider Fake(nint ptr) => new(ptr);
#pragma warning restore CA1720 // Identifier contains type name
}
