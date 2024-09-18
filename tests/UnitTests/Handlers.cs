namespace UnitTests;

sealed class Ref<T>(T value)
{
    public T Value { get; set; } = value;
}

static class Ref
{
    public static Ref<T> Create<T>(T value) => new(value);
}

/// <summary>
/// Represents the context of a <see cref="IHandler{TArgs,TResult}.Handle"/> call.
/// </summary>
sealed class CallContext<TArgs, TResult>
{
    public List<(TArgs, TResult)> Calls { get; } = [];
}

/// <summary>
/// Represents a call handler/implementation.
/// </summary>
interface IHandler<TArgs, TResult>
{
    TResult Handle(CallContext<TArgs, TResult> context, TArgs args);
}

/// <summary>
/// Represents a partial call handler/implementation.
/// </summary>
interface IPartialHandler<TArgs, TResult>
{
    IPartialHandler<TArgs, TResult> Do(Action<TArgs> action);
    IHandler<TArgs, TResult> Return(TResult result);
}

/// <summary>
/// Represents a call handler that never returns when there is no
/// implementation and therefore throws <see cref="NotImplementedException"/>.
/// </summary>
interface INeverHandler<TArgs, TResult> :
    IHandler<TArgs, TResult>,
    IPartialHandler<TArgs, TResult>;

/// <summary>
/// Represents a call handler that captures the arguments and results into
/// the call context.
/// </summary>
interface ICapturingHandler<TArgs, TResult> : IHandler<TArgs, TResult>;

/// <summary>
/// Provides a set of methods to create and manipulate handlers.
/// </summary>
static class Handler
{
    /// <summary>
    /// Creates a handler from the specified function.
    /// </summary>
    public static IHandler<TArgs, TResult>
        Create<TArgs, TResult>(Func<CallContext<TArgs, TResult>, TArgs, TResult> func) =>
        new DelegatingHandler<TArgs, TResult>(func);

    sealed class DelegatingHandler<TArgs, TResult>(Func<CallContext<TArgs, TResult>, TArgs, TResult> func) :
        IHandler<TArgs, TResult>
    {
        public TResult Handle(CallContext<TArgs, TResult> context, TArgs args) =>
            func(context, args);
    }

    /// <summary>
    /// Creates a handler that throw <see cref="NotImplementedException"/>.
    /// </summary>
    public static INeverHandler<TArgs, TResult>
        Never<TArgs, TResult>(string? name = null) =>
        name is { } someName
        ? new NeverHandler<TArgs, TResult>($"The handler for \"{someName}\" is not implemented.")
        : NeverHandler<TArgs, TResult>.Instance;

    sealed class NeverHandler<TArgs, TResult>(string? message = null) : INeverHandler<TArgs, TResult>
    {
        public static readonly NeverHandler<TArgs, TResult> Instance = new();

        public TResult Handle(CallContext<TArgs, TResult> context, TArgs args) =>
            throw new NotImplementedException(message);

        public IPartialHandler<TArgs, TResult> Do(Action<TArgs> action) =>
            new PartialHandler<TArgs, TResult>(action);

        public IHandler<TArgs, TResult> Return(TResult result) =>
            Create<TArgs, TResult>((_, _) => result);
    }

    sealed class PartialHandler<TArgs, TResult>(Action<TArgs> action) :
        IPartialHandler<TArgs, TResult>
    {
        readonly Action<TArgs> action = action;

        public IPartialHandler<TArgs, TResult> Do(Action<TArgs> action) =>
            new PartialHandler<TArgs, TResult>(args =>
            {
                this.action(args);
                action(args);
            });

        public IHandler<TArgs, TResult> Return(TResult result) =>
            Create<TArgs, TResult>((_, args) =>
            {
                this.action(args);
                return result;
            });
    }

    /// <summary>
    /// Creates a handler that asserts whether call arguments are equal to the
    /// expected value.
    /// </summary>
    public static IPartialHandler<TArgs, TResult>
        Expect<TArgs, TResult>(this IPartialHandler<TArgs, TResult> handler, TArgs expected)
            where TArgs : IEquatable<TArgs> =>
        handler.Do(args => Assert.Equal(expected, args));

    /// <summary>
    /// Creates a handler that asserts whether call arguments are equal to the
    /// expected value.
    /// </summary>
    public static IPartialHandler<TArgs, TResult>
        ExpectRef<TArgs, TResult>(this IPartialHandler<TArgs, TResult> handler, Ref<TArgs> expected)
        where TArgs : IEquatable<TArgs> =>
        handler.Do(args => Assert.Equal(expected.Value, args));

    /// <summary>
    /// Creates a handler that asserts whether call arguments meet a specific
    /// condition.
    /// </summary>
    public static IPartialHandler<TArgs, TResult>
        AssertTrue<TArgs, TResult>(this IPartialHandler<TArgs, TResult> handler,
                                   Func<TArgs, bool> predicate,
                                   string? message = null) =>
        handler.Do(args => Assert.True(predicate(args), message));

    /// <summary>
    /// Creates a handler that captures the arguments and results into the call context.
    /// </summary>
    public static IHandler<TArgs, TResult> Capture<TArgs, TResult>(this IHandler<TArgs, TResult> handler) =>
        handler as ICapturingHandler<TArgs, TResult> ?? new CapturingHandler<TArgs, TResult>(handler);

    sealed class CapturingHandler<TArgs, TResult>(IHandler<TArgs, TResult> handler) :
        ICapturingHandler<TArgs, TResult>
    {
        public TResult Handle(CallContext<TArgs, TResult> context, TArgs args)
        {
            var result = handler.Handle(context, args);
            context.Calls.Add((args, result));
            return result;
        }
    }

    /// <summary>
    /// Creates a handler that iterates through a sequence of implementations.
    /// If the handler is invoked when the entire sequence is exhausted, it
    /// throws <see cref="InvalidOperationException"/>.
    /// </summary>
    public static IHandler<TArgs, TResult>
        Sequence<TArgs, TResult>(this INeverHandler<TArgs, TResult> never,
                                 params Func<INeverHandler<TArgs, TResult>, IHandler<TArgs, TResult>>[] factories)
    {
        var count = 0;
        var handler = factories.Select(h => h(never)).GetEnumerator();

        return Create<TArgs, TResult>((context, args) =>
        {
            count++;

            if (!handler.MoveNext())
            {
                try
                {
                    _ = never.Handle(context, args);
                }
                catch (NotImplementedException ex)
                {
                    var suffix = (count % 10) switch { 1 => "st", 2 => "nd", 3 => "rd", _ => "th" };
                    throw new InvalidOperationException($"There is no handler for the {count}{suffix} invocation.", ex);
                }
            }

            return handler.Current.Handle(context, args);
        });
    }
}
