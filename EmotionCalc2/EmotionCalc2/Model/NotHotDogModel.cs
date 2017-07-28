using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

using Xamarin.Forms;

namespace EmotionCalc2.Model
{
    public class NotHotDogModel
    {
        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "Longitude")]
        public float Longitude { get; set; }

        [JsonProperty(PropertyName = "Latitude")]
        public float Latitude { get; set; }
    }
}