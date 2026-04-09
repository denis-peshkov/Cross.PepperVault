namespace Cross.PepperVault.Tests.Options;

[TestFixture]
public sealed class CompositeFluentOptionsValidatorAdapterTests
{
    [Test]
    public void Given_NoValidators_When_Validate_Then_Succeeds()
    {
        IValidateOptions<PepperOptions<int>> sut = new CompositeFluentOptionsValidatorAdapter<PepperOptions<int>>(Array.Empty<IValidator<PepperOptions<int>>>());
        var opt = new PepperOptions<int> { Provider = "p", ProviderOptions = 1 };

        var r = sut.Validate(Microsoft.Extensions.Options.Options.DefaultName, opt);

        r.Failed.Should().BeFalse();
    }

    [Test]
    public void Given_OneFailingValidator_When_Validate_Then_FailsWithMessage()
    {
        var failing = new Mock<IValidator<PepperOptions<int>>>();
        failing.Setup(x => x.Validate(It.IsAny<PepperOptions<int>>()))
            .Returns(new FluentValidation.Results.ValidationResult(new[]
            {
                new FluentValidation.Results.ValidationFailure("x", "custom-error"),
            }));

        IValidateOptions<PepperOptions<int>> sut = new CompositeFluentOptionsValidatorAdapter<PepperOptions<int>>(new[] { failing.Object });
        var opt = new PepperOptions<int> { Provider = "p", ProviderOptions = 1 };

        var r = sut.Validate(Microsoft.Extensions.Options.Options.DefaultName, opt);

        r.Failed.Should().BeTrue();
        r.Failures.Should().Contain("custom-error");
    }

    [Test]
    public void Given_TwoValidators_When_BothFail_Then_AggregatesFailures()
    {
        var a = new Mock<IValidator<PepperOptions<int>>>();
        a.Setup(x => x.Validate(It.IsAny<PepperOptions<int>>()))
            .Returns(new FluentValidation.Results.ValidationResult(new[] { new FluentValidation.Results.ValidationFailure("a", "err-a") }));
        var b = new Mock<IValidator<PepperOptions<int>>>();
        b.Setup(x => x.Validate(It.IsAny<PepperOptions<int>>()))
            .Returns(new FluentValidation.Results.ValidationResult(new[] { new FluentValidation.Results.ValidationFailure("b", "err-b") }));

        IValidateOptions<PepperOptions<int>> sut = new CompositeFluentOptionsValidatorAdapter<PepperOptions<int>>(new[] { a.Object, b.Object });
        var opt = new PepperOptions<int> { Provider = "p", ProviderOptions = 1 };

        var r = sut.Validate(Microsoft.Extensions.Options.Options.DefaultName, opt);

        r.Failures.Should().HaveCount(2);
        r.Failures.Should().Contain(new[] { "err-a", "err-b" });
    }

    [Test]
    public void Given_TwoPassingValidators_When_Validate_Then_Succeeds()
    {
        var a = new Mock<IValidator<PepperOptions<int>>>();
        a.Setup(x => x.Validate(It.IsAny<PepperOptions<int>>()))
            .Returns(new FluentValidation.Results.ValidationResult());
        var b = new Mock<IValidator<PepperOptions<int>>>();
        b.Setup(x => x.Validate(It.IsAny<PepperOptions<int>>()))
            .Returns(new FluentValidation.Results.ValidationResult());

        IValidateOptions<PepperOptions<int>> sut = new CompositeFluentOptionsValidatorAdapter<PepperOptions<int>>(new[] { a.Object, b.Object });
        var opt = new PepperOptions<int> { Provider = "p", ProviderOptions = 1 };

        var r = sut.Validate(Microsoft.Extensions.Options.Options.DefaultName, opt);

        r.Failed.Should().BeFalse();
        a.Verify(x => x.Validate(It.IsAny<PepperOptions<int>>()), Times.Once);
        b.Verify(x => x.Validate(It.IsAny<PepperOptions<int>>()), Times.Once);
    }
}
