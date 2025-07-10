namespace Amulet.Commands;

[AttributeUsage(AttributeTargets.Class)]
public class CommandAttribute : Attribute
{
    public string Name { get; }
    public string Description { get; }
    public int Priority { get; }

    public CommandAttribute(string name, string description, int priority = 0)
    {
        Name = name;
        Description = description;
        Priority = priority;
    }
} 