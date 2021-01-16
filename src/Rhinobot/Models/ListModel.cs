using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RhinoBot.Models
{
    public class ListModel
    {
        [JsonInclude]
        public Dictionary<string, string> entries;
    }
}