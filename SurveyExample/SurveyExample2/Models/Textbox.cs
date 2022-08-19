using SurveyExample2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SurveyExample2.Models
{
    public class Textbox : ElementBase
    {
        public string Type => "Textbox";

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PlaceHolder { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Text { get; set; }
    }
}
