namespace Cross.PepperVault.Tests.Options;

[TestFixture]
public sealed class PepperBaseValidatorTests
{
    [Test]
    public void Given_EmptyProvider_When_Validate_Then_Fails()
    {
        var v = new PepperBaseValidator<int>();
        var opt = new PepperOptions<int>
        {
            Provider = "",
            ProviderOptions = 1,
        };

        var r = v.Validate(opt);

        r.IsValid.Should().BeFalse();
        r.Errors.Should().Contain(e => e.ErrorMessage.Contains("Pepper.provider", StringComparison.Ordinal));
    }

    [Test]
    public void Given_NonPositiveTtl_When_Validate_Then_Fails()
    {
        var v = new PepperBaseValidator<int>();
        var opt = new PepperOptions<int>
        {
            Provider = "p",
            Ttl = TimeSpan.Zero,
            ProviderOptions = 1,
        };

        var r = v.Validate(opt);

        r.IsValid.Should().BeFalse();
        r.Errors.Should().Contain(e => e.ErrorMessage.Contains("Pepper.ttl", StringComparison.Ordinal));
    }

    [Test]
    public void Given_NegativeTtl_When_Validate_Then_Fails()
    {
        var v = new PepperBaseValidator<int>();
        var opt = new PepperOptions<int>
        {
            Provider = "p",
            Ttl = TimeSpan.FromMinutes(-5),
            ProviderOptions = 1,
        };

        var r = v.Validate(opt);

        r.IsValid.Should().BeFalse();
        r.Errors.Should().Contain(e => e.ErrorMessage.Contains("Pepper.ttl", StringComparison.Ordinal));
    }

    [Test]
    public void Given_NullProviderOptions_When_Validate_Then_Fails()
    {
        var v = new PepperBaseValidator<string>();
        var opt = new PepperOptions<string>
        {
            Provider = "p",
            ProviderOptions = null!,
        };

        var r = v.Validate(opt);

        r.IsValid.Should().BeFalse();
        r.Errors.Should().Contain(e => e.ErrorMessage.Contains("Pepper.providerOptions", StringComparison.Ordinal));
    }

    [Test]
    public void Given_ValidOptions_When_Validate_Then_Succeeds()
    {
        var v = new PepperBaseValidator<int>();
        var opt = new PepperOptions<int>
        {
            Provider = "vault",
            Ttl = TimeSpan.FromMinutes(1),
            ProviderOptions = 42,
        };

        var r = v.Validate(opt);

        r.IsValid.Should().BeTrue();
    }
}
