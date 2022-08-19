using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SurveyExampleNetStardard21.Interfaces
{
    public interface IElementType
    {
        [JsonPropertyName("type")]
        string Type { get; }
    }
}
