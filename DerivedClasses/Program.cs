using DerivedClasses.Classes;
using DerivedClasses.Converters;
using System.Text.Json;

var objects = new List<BaseClass> { new DerivedA(), new DerivedB() };

// Using: System.Text.Json
var options = new JsonSerializerOptions
{
    Converters = { new BaseClassConverter() },
    WriteIndented = true
};

string jsonString = JsonSerializer.Serialize(objects, options);
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("Convertsion result:");
Console.ForegroundColor = ConsoleColor.Gray;
Console.WriteLine(jsonString);
Console.WriteLine("");

var roundTrip = JsonSerializer.Deserialize<List<BaseClass>>(jsonString, options);

// Using: Newtonsoft.Json
var settings = new Newtonsoft.Json.JsonSerializerSettings
{
    TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects,
    Formatting = Newtonsoft.Json.Formatting.Indented
};

jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objects, settings);
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("Newtonsoft Convertsion result:");
Console.ForegroundColor = ConsoleColor.Gray;
Console.WriteLine(jsonString);
Console.WriteLine("");

/*
Convertsion result:
[
  {
    "TypeDiscriminator": 1,
    "TypeValue": {
      "Str": null,
      "Int": 0
    }
  },
  {
    "TypeDiscriminator": 2,
    "TypeValue": {
      "Bool": false,
      "Int": 0
    }
  }
]

Newtonsoft Convertsion result:
[
  {
    "$type": "DerivedClasses.Classes.DerivedA, DerivedClasses",
    "Str": null,
    "Int": 0
  },
  {
    "$type": "DerivedClasses.Classes.DerivedB, DerivedClasses",
    "Bool": false,
    "Int": 0
  }
]
*/