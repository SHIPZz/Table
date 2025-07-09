namespace Amulet.Common;

public interface IPrototype<T>
{
    T Clone();
}