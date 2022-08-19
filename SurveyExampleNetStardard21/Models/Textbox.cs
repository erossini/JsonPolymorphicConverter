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
    public class Textbox : ElementBase
    {
        public string Type => "Textbox";
        public ElementType ElementType = ElementType.Textbox;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PlaceHolder { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Text { get; set; }
    }
}
