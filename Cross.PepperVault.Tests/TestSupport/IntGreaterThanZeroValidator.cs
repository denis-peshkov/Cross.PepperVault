namespace Cross.PepperVault.Tests.TestSupport;

internal sealed class IntGreaterThanZeroValidator : AbstractValidator<PepperOptions<int>>
{
    public IntGreaterThanZeroValidator()
    {
        RuleFor(x => x.ProviderOptions).GreaterThan(0);
    }
}
