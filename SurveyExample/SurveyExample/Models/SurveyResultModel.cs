using SurveyExample.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyExample.Models
{
    public class SurveyResultModel
    {
        public string Id { get; set; }
        public string SurveyId { get; set; }
        public List<ISurveyStepResult> Steps { get; set; }
    }
}