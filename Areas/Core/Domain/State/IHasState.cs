namespace DddWorkshop.Areas.Core.Domain.State
{
    public interface IHasState<out T>
    {
        T State { get; }
    }
}