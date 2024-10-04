using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    private Transform _playerPos;
    public WaveMob _wavesToSpawn;

    [SerializeField] private List<GameObject> _mobPrefabList;
    private List<Mob> _mobScripts = new List<Mob>();
    private List<string> _enemiesInWave = new List<string>();
    
    private Dictionary<string, int> mobsToSpawn = new Dictionary<string, int>();
    
    private bool _hasStarted, _hasFinished, _isSpawningWave;
    
    private float _duration, _spawnDuration, mobTimeSpawn;
    private int _actualWave = 0, _numberEnemies = 0, _actualEnemyToSpawn = 0;
    
    //Je récupère la liste des scripts des préfabs de mobs
    private void Start()
    {
        for (int i = 0; i < _mobPrefabList.Count; i++)
        {
            _mobScripts.Add(_mobPrefabList[i].GetComponent<Mob>());
        }
    }
    
    public void Update()
    {
        //Se lance que quand le siphonnage est lancé
        if (!_hasStarted || _hasFinished) return;
        
        _duration += Time.deltaTime;
        
        //Si la vague a atteint le temps de la vague actuelle
        if (_duration >= _wavesToSpawn.waves[_actualWave].spawnTimeInWaveMob)
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
                InstantiateEnemy();
            }
        }
    }
    
    public void StartSpawnerWaves()
    {
        _hasStarted = true;
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        _playerPos = playerTransform;
    }

    private void WaveSpawning(int waveNumber)
    {
        _numberEnemies = 0;
        mobsToSpawn.Clear();
        _enemiesInWave.Clear();
        
        // Setup la vague en renseignant le dictionnaire avec les infos des ennemis et leurs nombres
        // Renseigne le nombre total d'enemis dans la vague
        for (int i = 0; i < _wavesToSpawn.waves[waveNumber].mobsToSpawn.Count; i++)
        {
            mobsToSpawn[_wavesToSpawn.waves[waveNumber].mobsToSpawn[i].mobName] = _wavesToSpawn.waves[waveNumber].mobsToSpawn[i].mobNumber;
            _numberEnemies += _wavesToSpawn.waves[waveNumber].mobsToSpawn[i].mobNumber;
        }

        // Créer une liste aléatoire avec les ennemis pour les instantier 1 a la fois
        foreach (var mobClassNumber in mobsToSpawn)
        {
            for (int i = 0; i < mobClassNumber.Value; i++)
            {
                _enemiesInWave.Add(mobClassNumber.Key);
            }
        }
        ShuffleList(_enemiesInWave);

        // Créer la variable de tout les quand un ennemi doit apparaître
        float f = _wavesToSpawn.waves[waveNumber].spawnDuration / _numberEnemies;
        mobTimeSpawn = Mathf.Round(f * 100.0f) * 0.01f;
    }
    
    private void InstantiateEnemy()
    {
        
        // Vérifie quelle ennemi doit apparaître à une position aléatoire autour du point de spawn
        for (int j = 0; j < _mobPrefabList.Count; j++)
        {
            if (_mobPrefabList[j].name == _enemiesInWave[_actualEnemyToSpawn])
            {
                Vector3 randomPosToSpawn = GetRandomSpawnPos(transform.position);
                Mob enemyInstantiate = Instantiate(_mobScripts[j], randomPosToSpawn, Quaternion.identity, transform);
                enemyInstantiate.Initialize(_playerPos);
            }
        }
        
        _actualEnemyToSpawn++;
        if (_actualEnemyToSpawn >= _numberEnemies)
        {
            FinishWave();
        }
    }

    //Fini la vague et réinitialise les variables
    private void FinishWave()
    {
        _isSpawningWave = false;
        _actualEnemyToSpawn = 0;
        _spawnDuration = 0;
        _actualWave++;

        if (_actualWave >= _wavesToSpawn.waves.Count)
        {
            _hasFinished = true;
        }
    }

    private Vector3 GetRandomSpawnPos(Vector3 position)
    {
        float xRandomPos = Random.Range(position.x - 10, position.x + 10);
        float zRandomPos = Random.Range(position.z - 10, position.z + 10);

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
