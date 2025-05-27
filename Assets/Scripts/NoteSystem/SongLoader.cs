using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is outside MonoBehaviour
[System.Serializable]
public class NoteData
{
    public string keyIndex;
    public float spawnTime;
    public float duration;
    public string targetPiano;
}

[System.Serializable]
public class NoteDataListWrapper
{
    public List<NoteData> song;
}

// SongLoader class that loads the files
public class SongLoader : MonoBehaviour
{
    public string fileName = "TwinkleTwinkle"; // Name of the JSON file without extension

    public List<NoteData> LoadSong()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
        if (jsonFile != null)
        {
            string jsonText = "{\"song\":" + jsonFile.text + "}"; // wrap the list
            NoteDataListWrapper wrapper = JsonUtility.FromJson<NoteDataListWrapper>(jsonText);
            return wrapper.song;
        }
        else
        {
            Debug.LogError("File " + fileName + ".json could not be loaded.");
            return new List<NoteData>();
        }
    }
}
