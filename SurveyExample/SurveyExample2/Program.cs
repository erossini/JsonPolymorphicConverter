using SurveyExample2.Converters;
using SurveyExample2.Interfaces;
using SurveyExample2.Models;
using System.Text.Json;

Form form = new Form()
{
    Elements = new List<IElement>()
    {
        new Textbox() { Title = "First name", PlaceHolder = "Enter your name", Name = "txt1", IsRequired = true },
        new Textbox() { Title = "Middle name", PlaceHolder = "Enter your middle name", Name = "txt2",
            Description = "Insert your middle name", VisibleIf = "txt1 = '1'" },
        new Textbox() { Title = "Last name", PlaceHolder = "Enter your last name", Name = "txt3",
            Description = "Insert your last name", IsRequired = true },
        new Radiobutton() { Title = "This is a radiobutton", Description = "Select one of the following options",
            Name = "radio1", Choices = new List<string>() { "One", "Two", "Three" }, IsRequired = true }
    }
};

var jsonSerializerOptions = new JsonSerializerOptions()
{
    Converters = { new ElementTypeConverter<IElement>() },
    WriteIndented = true
};
var result = JsonSerializer.Serialize(form, jsonSerializerOptions);

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("Convertsion result:");
Console.ForegroundColor = ConsoleColor.Gray;
Console.WriteLine(result);
Console.WriteLine("");

var back = JsonSerializer.Deserialize<Form>(result, jsonSerializerOptions);

var result2 = JsonSerializer.Serialize(back, jsonSerializerOptions);

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("Second Convertsion result:");
Console.ForegroundColor = ConsoleColor.Gray;
Console.WriteLine(result2);
Console.WriteLine("");