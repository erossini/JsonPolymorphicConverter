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
    public class Radiobutton : ElementBase
    {
        [JsonPropertyName("Type")]
        public string Type => "Radiobutton";
        public ElementType ElementType = ElementType.Radiobutton;

        public List<string>? Choices { get; set; }
    }
}