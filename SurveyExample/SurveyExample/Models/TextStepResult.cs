using SurveyExample.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyExample.Models
{
    public class TextStepResult : ISurveyStepResult
    {
        public string Id { get; set; }
        public string TypeDiscriminator => nameof(TextStepResult);

        public string Value { get; set; }
        public string Text { get; set; }
    }
}
