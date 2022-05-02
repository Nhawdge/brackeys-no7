using System.Text.Json;
using JustWind.Components;

namespace JustWind
{
    public static class EngineHelpers
    {
        const string configFile = "config.json";
        public static void LoadConfiguration(this Engine engine)
        {
            var singleton = engine.Singleton.GetComponent<Singleton>();
            var preferences = singleton.Options;

            preferences.MusicVolume = 1f;
            preferences.SoundVolume = 1f;

            if (File.Exists(configFile))
            {
                try
                {
                    var json = File.ReadAllText(configFile);
                    var loadedData = JsonSerializer.Deserialize<UserOptions>(json);
                    if (loadedData != null)
                    {
                        preferences.MusicVolume = loadedData.MusicVolume;
                        preferences.SoundVolume = loadedData.SoundVolume;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                    var data = JsonSerializer.Serialize(preferences, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(configFile, data);
                }
            }
            else
            {
                var data = JsonSerializer.Serialize(preferences, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(configFile, data);
            }
        }
    }
}
