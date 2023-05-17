using System.Text.Json.Serialization;

namespace BusinessLogicLayer.DeserializeModels;

public class DishesDeserialized
{
    [JsonPropertyName("items")]
    public DishDeserialized[] Items { get; set; } = new DishDeserialized[0];
}
