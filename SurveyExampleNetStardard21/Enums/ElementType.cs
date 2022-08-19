using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SurveyExampleNetStardard21.Enums
{
    public enum ElementType
    {
        [Description("Radiobutton")]
        Radiobutton = 1,
        [Description("Textbox")]
        Textbox = 2
    }
}
