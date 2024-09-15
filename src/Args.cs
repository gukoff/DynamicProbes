namespace DynamicProbes;

public enum ArgType // NOTE! Keep in sync with Libstapsdt.ArgType!
{
    NoArg  = Libstapsdt.ArgType.NoArg,
#pragma warning disable CA1720 // Identifier contains type name (by-design for familiarity)
    UInt8  = Libstapsdt.ArgType.UInt8,
    Int8   = Libstapsdt.ArgType.Int8,
    UInt16 = Libstapsdt.ArgType.UInt16,
    Int16  = Libstapsdt.ArgType.Int16,
    UInt32 = Libstapsdt.ArgType.UInt32,
    Int32  = Libstapsdt.ArgType.Int32,
    UInt64 = Libstapsdt.ArgType.UInt64,
    Int64  = Libstapsdt.ArgType.Int64,
#pragma warning disable CA1720 // Identifier contains type name
}

public interface IArgType
{
    static abstract ArgType ArgType { get; }
}

public interface IFireArgLong
{
    long UncheckedValue { get; }
}

#pragma warning disable CA2225 // Operator overloads have named alternates (not needed)

/// <summary>
/// <see langword="byte"/> as <see cref="ArgType.UInt8"/>.
/// </summary>
public readonly record struct UInt8Arg(byte Value) : IArgType, IFireArgLong
{
    public static ArgType ArgType => ArgType.UInt8;
    public static implicit operator UInt8Arg(byte value) => new(value);
    long IFireArgLong.UncheckedValue => Value;
    public override string ToString() => $"{Value}";
}

/// <summary>
/// <see langword="sbyte"/> as <see cref="ArgType.Int8"/>.
/// </summary>
public readonly record struct Int8Arg(sbyte Value) : IArgType, IFireArgLong
{
    public static ArgType ArgType => ArgType.Int8;
    public static implicit operator Int8Arg(sbyte value) => new(value);
    long IFireArgLong.UncheckedValue => Value;
    public override string ToString() => $"{Value}";
}

/// <summary>
/// <see langword="ushort"/> as <see cref="ArgType.UInt16"/>.
/// </summary>
public readonly record struct UInt16Arg(ushort Value) : IArgType, IFireArgLong
{
    public static ArgType ArgType => ArgType.UInt16;
    public static implicit operator UInt16Arg(ushort value) => new(value);
    long IFireArgLong.UncheckedValue => Value;
    public override string ToString() => $"{Value}";
}

/// <summary>
/// <see langword="short"/> as <see cref="ArgType.Int16"/>.
/// </summary>
public readonly record struct Int16Arg(short Value) : IArgType, IFireArgLong
{
    public static ArgType ArgType => ArgType.Int16;
    public static implicit operator Int16Arg(short value) => new(value);
    long IFireArgLong.UncheckedValue => Value;
    public override string ToString() => $"{Value}";
}

/// <summary>
/// <see langword="uint"/> as <see cref="ArgType.UInt32"/>.
/// </summary>
public readonly record struct UInt32Arg(uint Value) : IArgType, IFireArgLong
{
    public static ArgType ArgType => ArgType.UInt32;
    public static implicit operator UInt32Arg(uint value) => new(value);
    long IFireArgLong.UncheckedValue => Value;
    public override string ToString() => $"{Value}";
}

/// <summary>
/// <see langword="int"/> as <see cref="ArgType.Int32"/>.
/// </summary>
public readonly record struct Int32Arg(int Value) : IArgType, IFireArgLong
{
    public static ArgType ArgType => ArgType.Int32;
    public static implicit operator Int32Arg(int value) => new(value);
    long IFireArgLong.UncheckedValue => Value;
    public override string ToString() => $"{Value}";
}

/// <summary>
/// <see langword="ulong"/> as <see cref="ArgType.UInt64"/>.
/// </summary>
public readonly record struct UInt64Arg(ulong Value) : IArgType, IFireArgLong
{
    public static ArgType ArgType => ArgType.UInt64;
    public static implicit operator UInt64Arg(ulong value) => new(value);
    long IFireArgLong.UncheckedValue => unchecked((long)Value);
    public override string ToString() => $"{Value}";
}

/// <summary>
/// <see langword="long"/> as <see cref="ArgType.Int64"/>.
/// </summary>
public readonly record struct Int64Arg(long Value) : IArgType, IFireArgLong
{
    public static ArgType ArgType => ArgType.Int64;
    public static implicit operator Int64Arg(long value) => new(value);
    long IFireArgLong.UncheckedValue => Value;
    public override string ToString() => $"{Value}";
}
