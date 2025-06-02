using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


// NoteSpawner class handles spawning falling notes
public class NoteSpawner : MonoBehaviour
{
    // Prefab for white keys
    public GameObject WhiteNotePrefab;
    // Prefab for black keys
    public GameObject BlackNotePrefab;

    // Song file name to load from Resources
    private string songName;

    // Offset to adjust spawn position
    public float offset;

    // Direction the notes will fall for Piano1 (left)
    public Vector3 dirPiano1 = new Vector3(-1, 0, 0);
    // Direction the notes will fall for Piano2 (right)
    public Vector3 dirPiano2 = new Vector3(1, 0, 0);

    // Rotation for notes on Piano1
    public Vector3 rotPiano1Euler = Vector3.zero;
    // Rotation for notes on Piano2ç
    public Vector3 rotPiano2Euler = new Vector3(0, 90, 0);

    private bool white = false;

    // Key-to-SpawnPoint Mapping Setup
    [System.Serializable]
    public class KeySpawnMapping
    {
        // The name of the piano key (e.g. "C1", "Cs1", "D1")
        public string keyName;
        // Where in the scene the note should spawn
        public Transform spawnPoint;
    }

    // List of mappings between key names and spawn locations
    public KeySpawnMapping[] keyMappings;
    // Dictionary for quick lookup of spawn points
    private Dictionary<string, Transform> spawnPointMap;

    // Speed at which notes fall
    private float fallSpeed = 10;

    // Note parameters
    [System.Serializable]
    public class NoteData
    {
        public string keyIndex;    // Which key (and thus spawner) to use
        public float spawnTime;    // Time (in seconds) to spawn the note
        public float duration;     // How long the note lasts
        public string targetPiano; // Which note spawner has to work
    }

    // Music sheets
    [System.Serializable]
    public class NoteDataListWrapper
    {
        public List<NoteData> song;
    }

    // Note sequence for the song (from JSON)
    public List<NoteData> songData = new List<NoteData>();

    // Audio source for playing sounds
    private AudioSource audioSource;

    // Tracks elapsed time of the song
    private float songTimer = 0f;
    // Keeps track of which note should be spawned next
    private int currentNoteIndex = 0;

    // Text for the "You win!" pop up
    public TextMeshProUGUI winText1;
    public TextMeshProUGUI winText2;


    // Start method is called once when the script is initialized
    void Start()
    {
        // Get audiosource or add component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Build the dictionary from keyMappings
        spawnPointMap = new Dictionary<string, Transform>();
        foreach (var mapping in keyMappings)
        {
            if (!spawnPointMap.ContainsKey(mapping.keyName))
            {
                spawnPointMap[mapping.keyName] = mapping.spawnPoint;
            }
        }
        // Load JSON file
        songName = SongSelectionData.selectedSongName;
        LoadSongData();
    }

    // Update is called once per frame
    void Update()
    {
        // Exits the method if the game has not started yet
        if (!GameManager.Instance.startGame) return;
        // Increase timer with time elapsed since last frame
        songTimer += Time.deltaTime;

        // Start music if not already playing
        if (!audioSource.isPlaying && audioSource.clip != null && songTimer < audioSource.clip.length)
        {
            audioSource.Play();
        }

        // Spawn notes when it is time
        while (currentNoteIndex < songData.Count && songData[currentNoteIndex].spawnTime <= songTimer)
        {
            NoteData note = songData[currentNoteIndex];

            // Spawn for Piano1
            if (gameObject.tag == "Piano1"){
                NoteData noteForPiano1 = new NoteData()
                {
                    keyIndex = note.keyIndex,
                    spawnTime = note.spawnTime,
                    duration = note.duration,
                    targetPiano = "Piano1"
                };
                SpawnNote(noteForPiano1);
            }

            // Spawn for Piano2
            if (gameObject.tag == "Piano2")
            {
                NoteData noteForPiano2 = new NoteData()
                {
                    keyIndex = note.keyIndex,
                    spawnTime = note.spawnTime,
                    duration = note.duration,
                    targetPiano = "Piano2"
                };
                SpawnNote(noteForPiano2);
            }

            currentNoteIndex++;
        }

        // Stop game once song finishes
        if (audioSource.isPlaying && songTimer >= audioSource.clip.length - 5f)
        {
            // Get score from the score manager
            float score1 = ScoreManager.Instance.score1;
            float score2 = ScoreManager.Instance.score2;

            // Compare score between players
            if (gameObject.CompareTag("Piano1"))
            {
                winText1.gameObject.SetActive(true);

                if (score1 > score2)
                {
                    winText1.text = $"You win!\nYour Score: {(int)score1}";
                }
                else if (score2 > score1)
                {
                    winText1.text = $"You lose!\nYour Score: {(int)score1}";
                }
            }

            if (gameObject.CompareTag("Piano2"))
            {
                winText2.gameObject.SetActive(true);

                if (score1 > score2)
                {
                    winText2.text = $"You lose!\nYour Score: {(int)score2}";
                }
                else if (score2 > score1)
                {
                    winText2.text = $"You win!\nYour Score: {(int)score2}";
                }
            }


            // Coroutine for delay and exit 
            StartCoroutine(EndGameAfterDelay(7f));
        }
    }
    void LoadSongData()
    {
        // Default songName for testing the Competition Mode scene
        songName = string.IsNullOrEmpty(SongSelectionData.selectedSongName) ? "NinthSymphony" : SongSelectionData.selectedSongName;
        // Load JSON
        TextAsset jsonFile = Resources.Load<TextAsset>(songName); // Name of music sheet
        if (jsonFile != null)
        {
            string jsonText = "{\"song\":" + jsonFile.text + "}"; // Wrap in root object
            NoteDataListWrapper wrapper = JsonUtility.FromJson<NoteDataListWrapper>(jsonText);
            songData = wrapper.song;
        }
        else
        {
            Debug.LogError("Could not load the json file from Resources.");
        }
        // Load audio clip
        AudioClip audioClip = Resources.Load<AudioClip>(songName);
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
        }
        else
        {
            Debug.LogError("Could not load audio for song: " + songName);
        }
    }
    void SpawnNote(NoteData noteData)
    {
        // Find the correct spawn point for the note's key
        Transform spawnTransform = spawnPointMap[noteData.keyIndex];
        GameObject newNote;
        Quaternion rotation;
        Vector3 direction;

        // Set direction and rotation based on target piano
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

        // Determine if it's a black key and use the appropriate prefab
        if (noteData.keyIndex == "Cs1" || noteData.keyIndex == "Ds1" || noteData.keyIndex == "Fs1" || noteData.keyIndex == "Gs1" || noteData.keyIndex == "As1")
        {
            newNote = Instantiate(BlackNotePrefab, spawnTransform.position + new Vector3(offset, 0, 0), rotation);
        }

        else
        {
            newNote = Instantiate(WhiteNotePrefab, spawnTransform.position + new Vector3(offset, 0, 0), rotation);
            white = true;
        }

        // Get the note's script to pass in speed and direction
        var noteScript = newNote.GetComponent<FallingNote>();
        noteScript.fallSpeed = fallSpeed;

        // Change color and set movement direction based on piano
        if (noteData.targetPiano == "Piano1")
        {
            newNote.GetComponent<Renderer>().material.color = Color.blue;
            noteScript.dir = dirPiano1;
        }
        else if (noteData.targetPiano == "Piano2")
        {
            newNote.GetComponent<Renderer>().material.color = Color.red;
            noteScript.dir = dirPiano2;
            
            if (white)
            {
                newNote.GetComponent<BoxCollider>().center = new Vector3(0, -3, -1.25f);
            }
        }

        noteScript.Initialize(noteData.duration);  // Set scale based on duration
    }

    private IEnumerator EndGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        winText1.gameObject.SetActive(false);
        winText2.gameObject.SetActive(false);
        SceneManager.LoadScene("Title");
    }
}
