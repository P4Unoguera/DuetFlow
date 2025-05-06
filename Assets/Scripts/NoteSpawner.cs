using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject WhiteNotePrefab;
    public GameObject BlackNotePrefab;


    [System.Serializable]
    public class KeySpawnMapping
    {
        public string keyName;         // e.g. "C1", "D#4"
        public Transform spawnPoint;   // The Transform in the scene
    }

    public KeySpawnMapping[] keyMappings;
    private Dictionary<string, Transform> spawnPointMap;


    private float fallSpeed = 10;

    [System.Serializable]
    public class NoteData
    {
        public string keyIndex;      // Which key (and thus spawner) to use
        public float spawnTime;   // When to spawn the note (seconds)
        public float duration;    // How long the note lasts (optional)
    }

    public List<NoteData> songData = new List<NoteData>(); // Fill this from JSON or manually

    private float songTimer = 0f;
    private int currentNoteIndex = 0;

    void Start()
    {
        // Build the dictionary from keyMappings
        spawnPointMap = new Dictionary<string, Transform>();
        foreach (var mapping in keyMappings)
        {
            if (!spawnPointMap.ContainsKey(mapping.keyName))
            {
                spawnPointMap[mapping.keyName] = mapping.spawnPoint;
            }
        }
    }


    void Update()
    {
        songTimer += Time.deltaTime;

        // Spawn notes when it's time
        while (currentNoteIndex < songData.Count && songData[currentNoteIndex].spawnTime <= songTimer)
        {
            NoteData note = songData[currentNoteIndex];
            SpawnNote(note);
            currentNoteIndex++;
        }
    }

    void SpawnNote(NoteData noteData)
    {
        Transform spawnTransform = spawnPointMap[noteData.keyIndex];
        GameObject newNote;

        if (noteData.keyIndex == "Cs1" || noteData.keyIndex == "Ds1" || noteData.keyIndex == "Fs1" || noteData.keyIndex == "Gs1" || noteData.keyIndex == "As1" ||
            noteData.keyIndex == "Cs2" || noteData.keyIndex == "Ds2" || noteData.keyIndex == "Fs2" || noteData.keyIndex == "Gs2" || noteData.keyIndex == "As2")
        {
            newNote = Instantiate(BlackNotePrefab, spawnTransform.position, Quaternion.identity);
        }

        else
        {
            newNote = Instantiate(WhiteNotePrefab, spawnTransform.position, Quaternion.identity);
        }

        // Pass duration info to the note
        var noteScript = newNote.GetComponent<FallingNote>();
        noteScript.fallSpeed = fallSpeed;
        noteScript.Initialize(noteData.duration);  // Set scale based on duration
    }

}
