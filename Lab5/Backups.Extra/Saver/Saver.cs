using Newtonsoft.Json;

namespace Backups.Extra.Saver;

public class Saver<T>
{
    public Saver(string jsonPath, T objectToSave)
    {
        JsonPath = jsonPath;
        ObjectToSave = objectToSave;
    }

    public string JsonPath { get; }
    public T ObjectToSave { get; }

    public void Save()
    {
        var json = JsonConvert.SerializeObject(ObjectToSave, Formatting.Indented);
        File.WriteAllText(JsonPath, json);
    }

    public T Load()
    {
        var json = File.ReadAllText(JsonPath);
        return JsonConvert.DeserializeObject<T>(json);
    }
}