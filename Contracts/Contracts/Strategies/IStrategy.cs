namespace Contracts.Strategies
{
    public interface IStrategy
    {
        object Parameters { get; set; }

        void Do();
    }
}
