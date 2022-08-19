using SurveyExample2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SurveyExample2.Models
{
    public class Radiobutton : ElementBase
    {
        public string Type => "Radiobutton";

        public List<string>? Choices { get; set; }
    }
}
