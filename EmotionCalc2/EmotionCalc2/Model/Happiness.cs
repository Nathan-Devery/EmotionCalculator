﻿using System;
using Newtonsoft.Json;

namespace EmotionCalc2.Model
{
    public class Happiness
    {
        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "createdAt")]
        public DateTime createdAt { get; set; }

        [JsonProperty(PropertyName = "happinesslevel")]
        public double happinesslevel { get; set; }

        [JsonProperty(PropertyName = "tag")]
        public String tag { get; set; }
    }
}