using SurveyExample.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyExample.Models
{
    public class BoolStepResult : ISurveyStepResult
    {
        public string Id { get; set; }
        public string TypeDiscriminator => nameof(BoolStepResult);

        public bool Value { get; set; }
    }
}
