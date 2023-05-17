using System.Text.Json.Serialization;

namespace BusinessLogicLayer.DeserializeModels;

public class DishDeserialized
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("calories")]
    public decimal KCalorie { get; set; }

    [JsonPropertyName("serving_size_g")]
    public decimal ServingSize { get; set; }

    [JsonPropertyName("fat_total_g")]
    public decimal TotalFat { get; set; }

    [JsonPropertyName("fat_saturated_g")]
    public decimal SaturatedFat { get; set; }

    [JsonPropertyName("carbohydrates_total_g")]
    public decimal Carbohydrates { get; set; }

    [JsonPropertyName("protein_g")]
    public decimal Protein { get; set; }
}
