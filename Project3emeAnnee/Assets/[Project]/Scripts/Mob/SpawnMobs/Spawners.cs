using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class Spawners : MonoBehaviour
{
    [SerializedDictionary("Difficulty", "WaveScriptable")]
    public SerializedDictionary<int, ScriptableWaveMob> WavesToSpawn = new SerializedDictionary<int, ScriptableWaveMob>();

    // public ScriptableWaveMob wavesToSpawn;
    public float _mobSpawnRange = 10f;

    private Transform _playerPos;
    private SpawnSiegeMob _siegeManager;
    private List<GameObject> _mobsInWave = new List<GameObject>();

    //TODO Connect to MobHealth Death Event  
    [SerializeField] private List<GameObject> _mobsAlive = new List<GameObject>();


    private Dictionary<GameObject, int> mobsToSpawn = new Dictionary<GameObject, int>();

    private bool _hasStarted, _hasFinished, _isSpawningWave;
    private float _duration, _spawnDuration, mobTimeSpawn;
    private int _actualWave = 0, _numberMobs = 0, _actualMobToSpawn = 0;
    public void Update()
    {
        //Se lance que quand le siphonnage est lancé
        if (!_hasStarted) return;

        if (_hasFinished)
        {
            return;
        }

        _duration += Time.deltaTime;

        //Si la vague a atteint le temps de la vague actuelle
        if (_duration >= WavesToSpawn[GameManager.instance.Difficulty].waves[_actualWave].spawnTimeInWaveMob)
        {
            _isSpawningWave = true;

            WaveSpawning(_actualWave);
        }

        //Fait apparaître un ennemi durant la vague tous les x temps
        if (_isSpawningWave)
        {
            _spawnDuration += Time.deltaTime;
            if (_spawnDuration >= mobTimeSpawn)
            {
                _spawnDuration = 0;
                InstantiateMob();
            }
        }
    }

    public void StartSpawnerWaves(SpawnSiegeMob siegeManagerScript)
    {
        _hasStarted = true;
        _siegeManager = siegeManagerScript;
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        _playerPos = playerTransform;
    }

    private void WaveSpawning(int waveNumber)
    {
        _numberMobs = 0;
        mobsToSpawn.Clear();
        _mobsInWave.Clear();

        // Setup la vague en renseignant le dictionnaire avec les infos des ennemis et leurs nombres
        // Renseigne le nombre total d'enemis dans la vague
        for (int i = 0; i < WavesToSpawn[GameManager.instance.Difficulty].waves[waveNumber].mobsToSpawn.Count; i++)
        {
            mobsToSpawn[WavesToSpawn[GameManager.instance.Difficulty].waves[waveNumber].mobsToSpawn[i].mobName] = 0;
        }

        for (int i = 0; i < WavesToSpawn[GameManager.instance.Difficulty].waves[waveNumber].mobsToSpawn.Count; i++)
        {
            mobsToSpawn[WavesToSpawn[GameManager.instance.Difficulty].waves[waveNumber].mobsToSpawn[i].mobName] +=
                WavesToSpawn[GameManager.instance.Difficulty].waves[waveNumber].mobsToSpawn[i].mobNumber;
            _numberMobs += WavesToSpawn[GameManager.instance.Difficulty].waves[waveNumber].mobsToSpawn[i].mobNumber;
        }

        // Créer une liste aléatoire avec les ennemis pour les instantier 1 a la fois
        foreach (var mobClassNumber in mobsToSpawn)
        {
            for (int i = 0; i < mobClassNumber.Value; i++)
            {
                _mobsInWave.Add(mobClassNumber.Key);
            }
        }
        if (!WavesToSpawn[GameManager.instance.Difficulty].waves[_actualWave].isWaveSplit) ShuffleList(_mobsInWave);


        // Créer la variable de tout les quand un ennemi doit apparaître
        float f = WavesToSpawn[GameManager.instance.Difficulty].waves[waveNumber].spawnDuration / _numberMobs;
        mobTimeSpawn = Mathf.Round(f * 100.0f) * 0.01f;
    }

    private void InstantiateMob()
    {

        // Vérifie quelle ennemi doit apparaître à une position aléatoire autour du point de spawn
        Vector3 randomPosToSpawn = GetRandomSpawnPos(transform.position);
        GameObject mobInstantiate = Instantiate(_mobsInWave[_actualMobToSpawn], randomPosToSpawn, Quaternion.identity, transform);

        _mobsAlive.Add(mobInstantiate);

        MobHealth mobHealth = mobInstantiate.GetComponent<MobHealth>();
        mobHealth.GetComponent<Mob>().Initialize(_playerPos);
        mobHealth.OnDeathEvent.AddListener(RemoveMob);



        _actualMobToSpawn++;
        if (_actualMobToSpawn >= _numberMobs)
        {
            FinishWave();
        }
    }

    private void RemoveMob(MobHealth toRemove)
    {
        if (_mobsAlive.Contains(toRemove.gameObject))
            _mobsAlive.Remove(toRemove.gameObject);
        
        CheckIfMobs();
    }

    private void CheckIfMobs()
    {
        if (_mobsAlive.Count == 0)
        {
            _siegeManager.FinishWave(this);
            enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, _mobSpawnRange);
    }

    //Fini la vague et réinitialise les variables
    private void FinishWave()
    {
        _isSpawningWave = false;
        _actualMobToSpawn = 0;
        _spawnDuration = 0;
        _actualWave++;

        if (_actualWave >= WavesToSpawn[GameManager.instance.Difficulty].waves.Count)
        {
            _hasFinished = true;
        }
    }

    private Vector3 GetRandomSpawnPos(Vector3 position)
    {
        float xRandomPos = Random.Range(position.x - _mobSpawnRange, position.x + _mobSpawnRange);
        float zRandomPos = Random.Range(position.z - _mobSpawnRange, position.z + _mobSpawnRange);

        return new Vector3(xRandomPos, position.y, zRandomPos);
    }

    private void ShuffleList<T>(List<T> listToShuffle)
    {
        for (int i = 0; i < listToShuffle.Count; i++)
        {
            T temp = listToShuffle[i];
            int rand = Random.Range(i, listToShuffle.Count);
            listToShuffle[i] = listToShuffle[rand];
            listToShuffle[rand] = temp;
        }
    }
}
