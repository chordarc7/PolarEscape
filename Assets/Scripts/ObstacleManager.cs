using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance { get; private set; }

    [Header("Pool")]
    [SerializeField] private GameObject[] pool;
    
    [Header("Spawn Settings")]
    [SerializeField] private int count;
    [SerializeField] private SpriteRenderer spawnArea;
    [SerializeField] private float edgePadding;
    [SerializeField] private Transform container;
    
    [Header("UI")]
    [SerializeField] private LoadingScreen loadingScreen;

    private float _minX;
    private float _maxX;
    private float _minY;
    private float _maxY;

    private void Awake()
    {
        Instance = this;
        
        _minX = spawnArea.bounds.min.x + edgePadding;
        _maxX = spawnArea.bounds.max.x - edgePadding;
        _minY = spawnArea.bounds.min.y + edgePadding;
        _maxY = spawnArea.bounds.max.y - edgePadding;
    }

    public IEnumerator Spawn()
    {
        loadingScreen.Show();

        for (var i = 0; i < count; i++)
        {
            var randomPos = GetRandomPos();
            var obstacle = Instantiate(pool[Random.Range(0, pool.Length)], container);
            obstacle.transform.position = randomPos;
            
            loadingScreen.SetProgress(i + 1 / (float)count);

            if (i % 10 == 0) yield return null;
        }
        
        loadingScreen.Hide();
    }

    private Vector2 GetRandomPos()
    {
        while (true)
        {
            var randomPos = new Vector2(Random.Range(_minX, _maxX), Random.Range(_minY, _maxY));
            if (Vector2.Distance(randomPos, Vector2.zero) < 10f) continue;
            return randomPos;
        }
    }
}
