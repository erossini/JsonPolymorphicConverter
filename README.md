# Json Polymorphic Converter
In this repository you see in action `System.Text.Json` and how to implement a converter for polymorphic classes.

## Issue

I'm tried to object a JSON with `System.Text.Json` from my class. I have a base class called `Element` that is defined like this

```csharp
public interface IElement
{
    public string? Type { get; set; }
    public string? Name { get; set; }
}
```

Then, I have few classes that inherit from it, for example

```csharp
public class Textbox : IElement
{
    [JsonPropertyName("type")]
    public virtual string? Type { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }
}

public class Radiobutton : IElement
{
    [JsonPropertyName("type")]
    public virtual string? Type { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("choises")]
    public List<string> Choises = new List<string>();
}
```

Now, I want to have a class that defines the form with all the elements

```csharp
public class Form
{
    [JsonPropertyName("elements")]
    public List<IElement> Elements { get; set; } = new List<IElement>();
}
```

After that, I define the form

```csharp
Form form = new Form()
{
    Elements = new List<IElement>()
    {
        new Textbox() { Name = "txt1", Type = "Textbox", Text = "One" },
        new Radiobutton() { 
            Name = "radio1", Type = "Radiobutton",
            Choices = new List<string>() { "One", "Two", "Three" }}
    }
};
```

If I create the JSON from this object, it has only the common fields

```
{
    "elements": [
    {
        "type": "Textbox",
        "name": "txt1",
    },
    {
        "type": "Radiobutton",
        "name": "radio1",
    }
    ]
}
```

The fields `Text` for the Textbox or `Choices` for the Radiobutton are ignored. I read the [Microsoft documentation](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-polymorphism): I tried the code

```csharp
jsonString = JsonSerializer.Serialize<object>(weatherForecast, options);
```

but I obtained the same result.

How can I create the JSON with all the details of the `Form` object regardless of the type of `Element`? Viceversa, when I have the JSON, how can I deserialize it in the `Form` class?

## Solution

I like to share with you an issue I found using `System.Text.Json`. I followed the approach `TypeDiscriminatorConverter` that [Demetrius Axenowski][1]. It works very well.

My problems started when I added some annotations for the JSON. For example:

```csharp
[JsonPropertyName("name")]
```

I have lost all day to understand why the code didn't work. I created some dummy code to understand where the problem was. All the source code is now on [GitHub][2].

So, the problem was in the `JsonPropertyName` for the property I check in the converter. For example, this is a class

```csharp
public class Radiobutton : ElementBase
{
    [JsonPropertyName("type")]
    public string Type => "Radiobutton";
    public ElementType ElementType = ElementType.Radiobutton;

    public List<string>? Choices { get; set; }
}
```

As you can see, I set the `JsonPropertyName` because I like to see `type` in lower case. Now, if I convert the class with this converter:

```csharp
public class ElementTypeConverter<T> : JsonConverter<T> where T : IElementType
{
    private readonly IEnumerable<Type> _types;

    public ElementTypeConverter()
    {
        var type = typeof(T);
        _types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
            .ToList();
    }

    public override T Read(ref Utf8JsonReader reader, 
        Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        using (var jsonDocument = JsonDocument.ParseValue(ref reader))
        {
            if (!jsonDocument.RootElement.TryGetProperty(
                nameof(IElementType.Type), out var typeProperty))
            {
                throw new JsonException();
            }

            var type = _types.FirstOrDefault(x => x.Name == 
                typeProperty.GetString());
            if (type == null)
            {
                throw new JsonException();
            }

            var jsonObject = jsonDocument.RootElement.GetRawText();
            var result = (T)JsonSerializer.Deserialize(jsonObject, type, options);

            return result;
        }
    }

    public override void Write(Utf8JsonWriter writer, T value, 
        JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, options);
    }
}
```

I get the following error:

> Test method SurveyExampleNetStardard21.Tests.UnitTest1.TestConversionJson_SystemTextJson_3Textbox_1radiobutton threw exception:
>
> System.Text.Json.JsonException: The JSON value could not be converted to System.Collections.Generic.List`1[SurveyExampleNetStardard21.Interfaces.IElement]. Path: $.Elements[3] | LineNumber: 42 | BytePositionInLine: 5.

I removed the `JsonPropertyName` and it works fine. I tried to set

```csharp
[JsonPropertyName("Type")]
```

(basically, the same as the variable) and it works fine. So, don't change the name. The converter is working both ways (object to Json and Json to object). This is the test code:

```csharp
var jsonSerializerOptions = new JsonSerializerOptions()
{
    Converters = { new ElementTypeConverter<IElement>() },
    WriteIndented = true
};
var json = JsonSerializer.Serialize(form, jsonSerializerOptions);

var back = JsonSerializer.Deserialize<Form>(json, jsonSerializerOptions);

var json2 = JsonSerializer.Serialize(back, jsonSerializerOptions);
```

Another annotation is related to `Newtonsoft.Json`: I converted the object to Json and it was good without any particular configuration. When I tried to convert the result Json in the object, I got issues in the conversion. 

  [1]: https://stackoverflow.com/users/4040476/demetrius-axenowski
  [2]: https://github.com/erossini/JsonPolymorphicConverter