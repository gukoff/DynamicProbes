namespace UnitTests;

/// <summary>
/// Represents the absence of a specific value.
/// </summary>
readonly record struct Unit
{
    public override string ToString() => "()";
}
