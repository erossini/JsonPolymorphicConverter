using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyExample2.Interfaces
{
    public interface IElement : IElementType
    {
        string? Description { get; set; }
        bool IsRequired { get; set; }
        bool IsVisible { get; set; }
        string? Name { get; set; }
        string? Title { get; set; }
        string? ElementType { get; set; }
        string VisibleIf { get; set; }
    }
}
