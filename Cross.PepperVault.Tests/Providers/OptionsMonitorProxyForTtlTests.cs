using Cross.PepperVault.Providers;

namespace Cross.PepperVault.Tests.Providers;

[TestFixture]
public sealed class OptionsMonitorProxyForTtlTests
{
    [Test]
    public void Given_InnerOptions_When_ReadCurrentValue_Then_ReturnsTtl()
    {
        var inner = new Mock<IOptionsMonitor<PepperOptions<int>>>();
        inner.Setup(m => m.CurrentValue).Returns(new PepperOptions<int>
        {
            Provider = "p",
            Ttl = TimeSpan.FromMinutes(7),
            ProviderOptions = 1,
        });
        var sut = new OptionsMonitorProxyForTtl<int>(inner.Object);

        sut.CurrentValue.Should().Be(TimeSpan.FromMinutes(7));
    }

    [Test]
    public void Given_InnerOptions_When_GetNamed_Then_ReturnsTtlFromNamedOptions()
    {
        var inner = new Mock<IOptionsMonitor<PepperOptions<int>>>();
        inner.Setup(m => m.Get("n")).Returns(new PepperOptions<int>
        {
            Provider = "p",
            Ttl = TimeSpan.FromSeconds(12),
            ProviderOptions = 2,
        });
        var sut = new OptionsMonitorProxyForTtl<int>(inner.Object);

        sut.Get("n").Should().Be(TimeSpan.FromSeconds(12));
    }

    [Test]
    public void Given_InnerOnChange_When_InnerFires_Then_ListenerGetsTtlAndName()
    {
        Action<PepperOptions<int>, string?>? innerListener = null;
        var inner = new Mock<IOptionsMonitor<PepperOptions<int>>>();
        inner.Setup(m => m.OnChange(It.IsAny<Action<PepperOptions<int>, string?>>()))
            .Callback((Action<PepperOptions<int>, string?> listener) => innerListener = listener)
            .Returns(Mock.Of<IDisposable>());
        var sut = new OptionsMonitorProxyForTtl<int>(inner.Object);

        TimeSpan? ttl = null;
        string? name = null;
        sut.OnChange((t, n) =>
        {
            ttl = t;
            name = n;
        });

        innerListener.Should().NotBeNull();
        innerListener!(
            new PepperOptions<int>
            {
                Provider = "p",
                Ttl = TimeSpan.FromHours(2),
                ProviderOptions = 5,
            },
            "named");

        ttl.Should().Be(TimeSpan.FromHours(2));
        name.Should().Be("named");
    }

    [Test]
    public void Given_NamedOptions_When_GetNullName_Then_ReturnsTtl()
    {
        var inner = new Mock<IOptionsMonitor<PepperOptions<int>>>();
        inner.Setup(m => m.Get(null)).Returns(new PepperOptions<int>
        {
            Provider = "p",
            Ttl = TimeSpan.FromTicks(333),
            ProviderOptions = 0,
        });
        var sut = new OptionsMonitorProxyForTtl<int>(inner.Object);

        sut.Get(null).Should().Be(TimeSpan.FromTicks(333));
    }
}
