using System.Threading;
using Cross.PepperVault.Providers;

namespace Cross.PepperVault.Tests.TestSupport;

/// <summary>Первый вызов <see cref="LoadAsync"/> бросает; второй возвращает данные (после сброса счётчика внешним кодом не нужен — цикл по попыткам внутри теста).</summary>
internal sealed class ThrowingThenOkPepperProvider : PepperProviderBase
{
    private int _attempt;
    private readonly InvalidOperationException _firstError;

    private readonly (short current, IReadOnlyDictionary<short, string> peppers) _okPayload;

    public ThrowingThenOkPepperProvider(
        IOptionsMonitor<TimeSpan> ttlMonitor,
        InvalidOperationException firstError,
        (short current, IReadOnlyDictionary<short, string> peppers) okPayload)
        : base(ttlMonitor)
    {
        _firstError = firstError;
        _okPayload = okPayload;
    }

    protected override Task<(short current, IReadOnlyDictionary<short, string> peppers)> LoadAsync(CancellationToken cancellationToken)
    {
        if (Interlocked.Increment(ref _attempt) == 1)
            throw _firstError;

        return Task.FromResult(_okPayload);
    }
}
