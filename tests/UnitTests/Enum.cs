namespace UnitTests;

/// <summary>
/// A transparent wrapper around an enum type that provides value equality
/// semantics.
/// </summary>
readonly record struct Enum<T>(T Value) where T : Enum
{
    public static implicit operator Enum<T>(T value) => new(value);
    public static implicit operator T(Enum<T> value) => value.Value;

    public override string ToString() => Value.ToString();
}
