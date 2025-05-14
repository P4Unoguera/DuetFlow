using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject WhiteNotePrefab;
    public GameObject BlackNotePrefab;

    public float offset;

    public Vector3 dirPiano1 = new Vector3(-1, 0, 0);   // left
    public Vector3 dirPiano2 = new Vector3(1, 0, 0);    // right

    public Vector3 rotPiano1Euler = Vector3.zero;
    public Vector3 rotPiano2Euler = new Vector3(0, 90, 0);

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
        public string targetPiano;  // Which note spawner has to work
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

        Quaternion rotation;
        Vector3 direction;

        if (noteData.targetPiano == "Piano1")
        {
            rotation = Quaternion.Euler(rotPiano1Euler);
            direction = dirPiano1;
        }
        else
        {
            rotation = Quaternion.Euler(rotPiano2Euler);
            direction = dirPiano2;
        }

        if (noteData.keyIndex == "Cs1" || noteData.keyIndex == "Ds1" || noteData.keyIndex == "Fs1" || noteData.keyIndex == "Gs1" || noteData.keyIndex == "As1")
        {
            newNote = Instantiate(BlackNotePrefab, spawnTransform.position + new Vector3(offset, 0, 0), rotation);
        }

        else
        {
            newNote = Instantiate(WhiteNotePrefab, spawnTransform.position + new Vector3(offset, 0, 0), rotation);
        }

        // Pass duration info to the note
        var noteScript = newNote.GetComponent<FallingNote>();
        noteScript.fallSpeed = fallSpeed;


        if (noteData.targetPiano == "Piano1")
        {
            newNote.GetComponent<Renderer>().material.color = Color.blue;
            noteScript.dir = dirPiano1;
        }
        else if (noteData.targetPiano == "Piano2")
        {
            newNote.GetComponent<Renderer>().material.color = Color.red;
            noteScript.dir = dirPiano2;
        }

        noteScript.Initialize(noteData.duration);  // Set scale based on duration
    }

}
