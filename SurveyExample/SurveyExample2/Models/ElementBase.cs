using SurveyExample2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SurveyExample2.Models
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
        public string? Type { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string VisibleIf { get; set; }

        public virtual string ElementType { get; set; }
    }
}
