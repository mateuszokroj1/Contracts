namespace Contracts.Validators
{
    /// <summary>
    /// Value validator
    /// </summary>
    /// <typeparam name="Tvalue"></typeparam>
    public interface IValidator<Tvalue>
    {
        bool? Validate(Tvalue value);
    }
}
