using System;
using Newtonsoft.Json;

namespace EmotionCalc2.Model
{
    public class Cat
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "breed")]
        public string breed { get; set; }

        [JsonProperty(PropertyName = "createdAt")]
        public DateTime createdAt { get; set; }
    }
}
