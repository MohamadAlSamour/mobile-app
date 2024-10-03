using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PrimeOption.Models
{
	public class Root
	{
        [JsonProperty("userId")]
        public string userId { get; set; }

        [JsonProperty("message")]
        public List<Events> message { get; set; }
    }
}

