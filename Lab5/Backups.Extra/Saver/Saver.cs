using Newtonsoft.Json;

namespace Backups.Extra.Saver;

public class Saver<T>
{
    public Saver(string? jsonPath, T objectToSave)
    {
        JsonPath = jsonPath;
        ObjectToSave = objectToSave;
    }

    public static string? JsonPath { get; set; }
    public T ObjectToSave { get; }

    public static T Load()
    {
        if (JsonPath != null)
        {
            var json = File.ReadAllText(JsonPath);
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented,
            });
        }

        throw new Exception("JsonPath is null");
    }

    public void Save()
    {
        var json = JsonConvert.SerializeObject(ObjectToSave, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            Formatting = Formatting.Indented,
        });
        if (JsonPath != null) File.WriteAllText(JsonPath, json);
    }
}