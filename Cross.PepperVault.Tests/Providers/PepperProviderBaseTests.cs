using System.Collections.ObjectModel;
using System.Threading;
using Cross.PepperVault.Tests.TestSupport;

namespace Cross.PepperVault.Tests.Providers;

[TestFixture]
public sealed class PepperProviderBaseTests
{
    [Test]
    public void Given_ValidPayload_When_ReloadAsync_Then_ExposesCurrentAndPeppers()
    {
        var ttl = new Mock<IOptionsMonitor<TimeSpan>>();
        ttl.Setup(m => m.CurrentValue).Returns(TimeSpan.FromDays(1));
        var peppers = new ReadOnlyDictionary<short, string>(new Dictionary<short, string> { [2] = "secret", [1] = "old" });
        var sut = new TestPepperProvider(ttl.Object, (2, peppers));

        sut.Invoking(s => s.ReloadAsync().GetAwaiter().GetResult()).Should().NotThrow();

        sut.CurrentVersion.Should().Be((short)2);
        sut.Peppers.Should().HaveCount(2);
        sut.TryGetCurrentVersion(out var p).Should().BeTrue();
        p.Should().Be("secret");
        sut.TryGet(1, out var oldP).Should().BeTrue();
        oldP.Should().Be("old");
    }

    [Test]
    public void Given_CurrentMissingInMap_When_ReloadAsync_Then_Throws()
    {
        var ttl = new Mock<IOptionsMonitor<TimeSpan>>();
        ttl.Setup(m => m.CurrentValue).Returns(TimeSpan.FromDays(1));
        var peppers = new ReadOnlyDictionary<short, string>(new Dictionary<short, string> { [1] = "only" });
        var sut = new TestPepperProvider(ttl.Object, (2, peppers));

        sut.Invoking(s => s.ReloadAsync().GetAwaiter().GetResult())
            .Should().Throw<InvalidOperationException>()
            .WithMessage("*Current pepper missing*");
    }

    [Test]
    public void Given_NoPriorReload_When_ReadCurrentVersion_Then_LoadsOnce()
    {
        var ttl = new Mock<IOptionsMonitor<TimeSpan>>();
        ttl.Setup(m => m.CurrentValue).Returns(TimeSpan.FromDays(1));
        var peppers = new ReadOnlyDictionary<short, string>(new Dictionary<short, string> { [1] = "x" });
        var sut = new TestPepperProvider(ttl.Object, (1, peppers));

        sut.CurrentVersion.Should().Be((short)1);

        sut.LoadCount.Should().Be(1);
    }

    [Test]
    public void Given_Loaded_When_ReadTwiceInsideTtl_Then_LoadsOnce()
    {
        var ttl = new Mock<IOptionsMonitor<TimeSpan>>();
        ttl.Setup(m => m.CurrentValue).Returns(TimeSpan.FromDays(1));
        var peppers = new ReadOnlyDictionary<short, string>(new Dictionary<short, string> { [1] = "x" });
        var sut = new TestPepperProvider(ttl.Object, (1, peppers));

        _ = sut.CurrentVersion;
        _ = sut.CurrentVersion;

        sut.LoadCount.Should().Be(1);
    }

    [Test]
    [NonParallelizable]
    public void Given_Loaded_When_TtlExpired_Then_ReloadsOnRead()
    {
        var ttl = new Mock<IOptionsMonitor<TimeSpan>>();
        ttl.Setup(m => m.CurrentValue).Returns(TimeSpan.FromMilliseconds(25));
        var peppers = new ReadOnlyDictionary<short, string>(new Dictionary<short, string> { [1] = "x" });
        var sut = new TestPepperProvider(ttl.Object, (1, peppers));

        _ = sut.CurrentVersion;
        Thread.Sleep(80);
        _ = sut.Peppers;

        sut.LoadCount.Should().BeGreaterThanOrEqualTo(2);
    }

    [Test]
    public void Given_UnknownVersion_When_TryGet_Then_False()
    {
        var ttl = new Mock<IOptionsMonitor<TimeSpan>>();
        ttl.Setup(m => m.CurrentValue).Returns(TimeSpan.FromDays(1));
        var peppers = new ReadOnlyDictionary<short, string>(new Dictionary<short, string> { [1] = "x" });
        var sut = new TestPepperProvider(ttl.Object, (1, peppers));

        sut.ReloadAsync().GetAwaiter().GetResult();

        sut.TryGet(99, out var p).Should().BeFalse();
        p.Should().BeNull();
    }

    [Test]
    public async Task Given_CancelledToken_When_ReloadAsync_Then_ThrowsOperationCanceled()
    {
        var ttl = new Mock<IOptionsMonitor<TimeSpan>>();
        ttl.Setup(m => m.CurrentValue).Returns(TimeSpan.FromDays(1));
        var peppers = new ReadOnlyDictionary<short, string>(new Dictionary<short, string> { [1] = "x" });
        var sut = new TestPepperProvider(ttl.Object, (1, peppers));
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        var act = async () => await sut.ReloadAsync(cts.Token).ConfigureAwait(false);

        await act.Should().ThrowAsync<OperationCanceledException>();
    }

    [Test]
    public void Given_LoadFailsOnce_When_RetryReload_Then_Succeeds()
    {
        var ttl = new Mock<IOptionsMonitor<TimeSpan>>();
        ttl.Setup(m => m.CurrentValue).Returns(TimeSpan.FromDays(1));
        var peppers = new ReadOnlyDictionary<short, string>(new Dictionary<short, string> { [1] = "ok" });
        var sut = new ThrowingThenOkPepperProvider(
            ttl.Object,
            new InvalidOperationException("load-boom"),
            (1, peppers));

        sut.Invoking(s => s.ReloadAsync().GetAwaiter().GetResult())
            .Should().Throw<InvalidOperationException>()
            .WithMessage("load-boom");

        sut.Invoking(s => s.ReloadAsync().GetAwaiter().GetResult()).Should().NotThrow();
        sut.CurrentVersion.Should().Be((short)1);
    }

    [Test]
    public void Given_PeppersProperty_When_AfterReload_Then_ReturnsSameDictionaryReferencePath()
    {
        var ttl = new Mock<IOptionsMonitor<TimeSpan>>();
        ttl.Setup(m => m.CurrentValue).Returns(TimeSpan.FromDays(1));
        var peppers = new ReadOnlyDictionary<short, string>(new Dictionary<short, string> { [3] = "p" });
        var sut = new TestPepperProvider(ttl.Object, (3, peppers));

        sut.ReloadAsync().GetAwaiter().GetResult();

        sut.Peppers[3].Should().Be("p");
    }

    [Test]
    public async Task Given_ConcurrentReload_When_TwoCalls_Then_BothComplete()
    {
        var ttl = new Mock<IOptionsMonitor<TimeSpan>>();
        ttl.Setup(m => m.CurrentValue).Returns(TimeSpan.FromDays(1));
        var peppers = new ReadOnlyDictionary<short, string>(new Dictionary<short, string> { [1] = "x" });
        var sut = new TestPepperProvider(ttl.Object, (1, peppers));

        var t1 = sut.ReloadAsync();
        var t2 = sut.ReloadAsync();
        await Task.WhenAll(t1, t2).ConfigureAwait(false);

        sut.CurrentVersion.Should().Be((short)1);
        sut.LoadCount.Should().BeGreaterThanOrEqualTo(2);
    }
}
