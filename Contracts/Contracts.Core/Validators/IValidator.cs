namespace Contracts
{
    public interface IValidator<Tvalue>
    {
        bool Validate(Tvalue value);
    }
}
