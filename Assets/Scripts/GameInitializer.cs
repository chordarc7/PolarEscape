using System.Collections;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return ObstacleManager.Instance.Spawn();
    }
}
