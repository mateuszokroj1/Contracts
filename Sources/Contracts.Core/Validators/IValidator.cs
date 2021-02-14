namespace Contracts.Validators
{
    /// <summary>
    /// Interface for basic validators
    /// </summary>
    /// <typeparam name="Tvalue"></typeparam>
    public interface IValidator<Tvalue>
    {
        bool Validate(Tvalue value);
    }
}
