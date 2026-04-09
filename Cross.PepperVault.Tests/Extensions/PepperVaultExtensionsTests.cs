using System.IO;
using System.Text;
using Cross.PepperVault.Extensions;
using Cross.PepperVault.Tests.TestSupport;

namespace Cross.PepperVault.Tests.Extensions;

[TestFixture]
public sealed class PepperVaultExtensionsTests
{
    private static IConfigurationRoot BuildJsonConfig(string json)
    {
        return new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(json)))
            .Build();
    }

    [Test]
    public void Given_ValidConfiguration_When_ResolveOptions_Then_BindsPepperSection()
    {
        var cfg = BuildJsonConfig("""{"Pepper":{"Provider":"vault","Ttl":"00:15:00","ProviderOptions":99}}""");
        var services = new ServiceCollection();
        services.AddPepperOptions<int, IntGreaterThanZeroValidator>(cfg);
        var sp = services.BuildServiceProvider();

        var opt = sp.GetRequiredService<IOptions<PepperOptions<int>>>().Value;

        opt.Provider.Should().Be("vault");
        opt.Ttl.Should().Be(TimeSpan.FromMinutes(15));
        opt.ProviderOptions.Should().Be(99);
    }

    [Test]
    public void Given_ConfigurationViolatesProviderValidator_When_ResolveOptions_Then_Throws()
    {
        var cfg = BuildJsonConfig("""{"Pepper":{"Provider":"vault","Ttl":"00:01:00","ProviderOptions":0}}""");
        var services = new ServiceCollection();
        services.AddPepperOptions<int, IntGreaterThanZeroValidator>(cfg);
        var sp = services.BuildServiceProvider();

        var act = () => _ = sp.GetRequiredService<IOptions<PepperOptions<int>>>().Value;

        act.Should().Throw<OptionsValidationException>();
    }

    [Test]
    public void Given_AddPepperOptions_When_Called_Then_ReturnsSameServiceCollection()
    {
        var cfg = BuildJsonConfig("""{"Pepper":{"Provider":"vault","Ttl":"00:15:00","ProviderOptions":99}}""");
        var services = new ServiceCollection();

        var returned = services.AddPepperOptions<int, IntGreaterThanZeroValidator>(cfg);

        returned.Should().BeSameAs(services);
    }

    [Test]
    public void Given_EmptyProviderInConfiguration_When_ResolveOptions_Then_Throws()
    {
        var cfg = BuildJsonConfig("""{"Pepper":{"Provider":"","Ttl":"00:01:00","ProviderOptions":1}}""");
        var services = new ServiceCollection();
        services.AddPepperOptions<int, IntGreaterThanZeroValidator>(cfg);
        var sp = services.BuildServiceProvider();

        var act = () => _ = sp.GetRequiredService<IOptions<PepperOptions<int>>>().Value;

        act.Should().Throw<OptionsValidationException>();
    }
}
