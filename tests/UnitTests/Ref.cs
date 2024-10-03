namespace UnitTests;

sealed class Ref<T>(T value)
{
    public T Value { get; set; } = value;
}

static class Ref
{
    public static Ref<T> Create<T>(T value) => new(value);
}
