using SurveyExample.Converters;
using SurveyExample.Interfaces;
using SurveyExample.Models;
using System.Text.Json;

var surveyResult = new SurveyResultModel()
{
    Id = "id",
    SurveyId = "surveyId",
    Steps = new List<ISurveyStepResult>()
    {
        new BoolStepResult() { Id = "1", Value = true},
        new TextStepResult() { Id = "2", Value = "some text"},
        new StarsStepResult() { Id = "3", Value = 5}
    }
};

var jsonSerializerOptions = new JsonSerializerOptions()
{
    Converters = { new TypeDiscriminatorConverter<ISurveyStepResult>() },
    WriteIndented = true
};
var result = JsonSerializer.Serialize(surveyResult, jsonSerializerOptions);

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("Convertsion result:");
Console.ForegroundColor = ConsoleColor.Gray;
Console.WriteLine(result);
Console.WriteLine("");

var back = JsonSerializer.Deserialize<SurveyResultModel>(result, jsonSerializerOptions);

var result2 = JsonSerializer.Serialize(back, jsonSerializerOptions);

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("Second Convertsion result:");
Console.ForegroundColor = ConsoleColor.Gray;
Console.WriteLine(result2);
Console.WriteLine("");

// Using: Newtonsoft.Json
var settings = new Newtonsoft.Json.JsonSerializerSettings
{
    TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects,
    Formatting = Newtonsoft.Json.Formatting.Indented
};

var result3 = Newtonsoft.Json.JsonConvert.SerializeObject(surveyResult, settings);
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("Newtonsoft Convertsion result:");
Console.ForegroundColor = ConsoleColor.Gray;
Console.WriteLine(result3);
Console.WriteLine("");

/*
Convertsion result:
{
  "Id": "id",
  "SurveyId": "surveyId",
  "Steps": [
    {
      "Id": "1",
      "TypeDiscriminator": "BoolStepResult",
      "Value": true
    },
    {
      "Id": "2",
      "TypeDiscriminator": "TextStepResult",
      "Value": "some text"
    },
    {
      "Id": "3",
      "TypeDiscriminator": "StarsStepResult",
      "Value": 5
    }
  ]
}

Second Convertsion result:
{
  "Id": "id",
  "SurveyId": "surveyId",
  "Steps": [
    {
      "Id": "1",
      "TypeDiscriminator": "BoolStepResult",
      "Value": true
    },
    {
      "Id": "2",
      "TypeDiscriminator": "TextStepResult",
      "Value": "some text"
    },
    {
      "Id": "3",
      "TypeDiscriminator": "StarsStepResult",
      "Value": 5
    }
  ]
}

Newtonsoft Convertsion result:
{
  "$type": "SurveyExample.Models.SurveyResultModel, SurveyExample",
  "Id": "id",
  "SurveyId": "surveyId",
  "Steps": [
    {
      "$type": "SurveyExample.Models.BoolStepResult, SurveyExample",
      "Id": "1",
      "TypeDiscriminator": "BoolStepResult",
      "Value": true
    },
    {
      "$type": "SurveyExample.Models.TextStepResult, SurveyExample",
      "Id": "2",
      "TypeDiscriminator": "TextStepResult",
      "Value": "some text"
    },
    {
      "$type": "SurveyExample.Models.StarsStepResult, SurveyExample",
      "Id": "3",
      "TypeDiscriminator": "StarsStepResult",
      "Value": 5
    }
  ]
}
 */