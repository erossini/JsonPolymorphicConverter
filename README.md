# Json Polymorphic Converter
In this repository you see in action `System.Text.Json` and how to implement a converter for polymorphic classes.

I like to share with you an issue I found using `System.Text.Json`. I followed the approach `TypeDiscriminatorConverter` that [Demetrius Axenowski][1]. It works very well.

My problems started when I added some annotations for the JSON. For example:

```
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

I get the following error:

> Test method SurveyExampleNetStardard21.Tests.UnitTest1.TestConversionJson_SystemTextJson_3Textbox_1radiobutton threw exception:
>
> System.Text.Json.JsonException: The JSON value could not be converted to System.Collections.Generic.List`1[SurveyExampleNetStardard21.Interfaces.IElement]. Path: $.Elements[3] | LineNumber: 42 | BytePositionInLine: 5.

I removed the `JsonPropertyName` and it works fine. I tried to set

```
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