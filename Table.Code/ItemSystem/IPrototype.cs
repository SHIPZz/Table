namespace Amulet.ItemSystem;

public interface IPrototype<T>
{
    T Clone();
}