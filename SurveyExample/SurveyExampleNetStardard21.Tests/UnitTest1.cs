using SurveyExampleNetStardard21.Converters;
using SurveyExampleNetStardard21.Interfaces;
using SurveyExampleNetStardard21.Models;
using System.Text.Json;

namespace SurveyExampleNetStardard21.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConversionJson_SystemTextJson_3Textbox_1radiobutton()
        {
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
    var json = JsonSerializer.Serialize(form, jsonSerializerOptions);
    Console.WriteLine(json);

    var back = JsonSerializer.Deserialize<Form>(json, jsonSerializerOptions);

    var json2 = JsonSerializer.Serialize(back, jsonSerializerOptions);
            Console.WriteLine(json);

            Assert.AreEqual(json, json2);
        }
    }
}