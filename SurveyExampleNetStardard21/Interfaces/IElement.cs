using SurveyExampleNetStardard21.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyExampleNetStardard21.Interfaces
{
    public interface IElement : IElementType
    {
        ElementType ElementType { get; }
        string? Description { get; set; }
        bool IsRequired { get; set; }
        bool IsVisible { get; set; }
        string? Name { get; set; }
        string? Title { get; set; }
        string VisibleIf { get; set; }
    }
}
