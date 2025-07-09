using System.Text.Json.Serialization;

namespace Amulet.ItemSystem;

[Serializable]
public class ItemEntry
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ItemType Type { get; set; }
    public int Amount { get; set; }
}