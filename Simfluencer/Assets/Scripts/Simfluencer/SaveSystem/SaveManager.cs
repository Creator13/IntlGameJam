using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveSystem {
    public delegate void GameLoad<in T>(T data);

    public delegate void GameSave<T>(out T data);

    public static class SaveManager<T> where T : new() {
        private const string Filename = "game.dat";

        public static event GameLoad<T> OnGameLoad;
        public static event GameSave<T> OnGameSave;

        public static void Save() {
            var dataObject = new T();
            OnGameSave?.Invoke(out dataObject);

            var formatter = new BinaryFormatter();
            var path = Path.Combine(Application.persistentDataPath, Filename);

            using (var stream = new FileStream(path, FileMode.Create)) {
                formatter.Serialize(stream, dataObject);
            }
        }

        public static T Load() {
            var formatter = new BinaryFormatter();
            var path = Path.Combine(Application.persistentDataPath, Filename);

            if (File.Exists(path)) {
                using (var stream = new FileStream(path, FileMode.Open)) {
                    var dataObject = (T) formatter.Deserialize(stream);
                    OnGameLoad?.Invoke(dataObject);
                    return dataObject;
                }
            }

            Debug.Log("No save found");
            return default;
        }
    }
}
