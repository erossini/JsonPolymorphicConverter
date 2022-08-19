using SurveyExample.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyExample.Models
{
    public class StarsStepResult : ISurveyStepResult
    {
        public string Id { get; set; }
        public string TypeDiscriminator => nameof(StarsStepResult);

        public int Value { get; set; }
    }
}
