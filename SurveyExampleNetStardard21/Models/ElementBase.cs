using SurveyExampleNetStardard21.Enums;
using SurveyExampleNetStardard21.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SurveyExampleNetStardard21.Models
{
    public class ElementBase : IElement
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }
        public bool IsRequired { get; set; }
        public bool IsVisible { get; set; }
        public string? Name { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; set; }

        [JsonPropertyName("Type")]
        public string? Type { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string VisibleIf { get; set; }

        ElementType IElement.ElementType { get; }
    }
}
