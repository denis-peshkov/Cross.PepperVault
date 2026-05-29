namespace Cross.PepperVault.Tests.TestSupport;

internal sealed class TestPepperProvider : PepperProviderBase
{
    private (short current, IReadOnlyDictionary<short, string> peppers) _payload;

    private int _loadCount;

    public TestPepperProvider(IOptionsMonitor<TimeSpan> ttlMonitor, (short current, IReadOnlyDictionary<short, string> peppers) payload)
        : base(ttlMonitor)
    {
        _payload = payload;
    }

    public int LoadCount => Volatile.Read(ref _loadCount);

    public void SetPayload((short current, IReadOnlyDictionary<short, string> peppers) payload) => _payload = payload;

    protected override Task<(short current, IReadOnlyDictionary<short, string> peppers)> LoadAsync(CancellationToken cancellationToken)
    {
        Interlocked.Increment(ref _loadCount);
        return Task.FromResult(_payload);
    }
}
