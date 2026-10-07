using UnityEngine;

public class SpawnManager : SingletonMono<SpawnManager>
{
    public Vector2 SpawnPoint {  get; private set; }
    public void SetSpawnPoint(GameObject target)
    {
        this.SpawnPoint = target.transform.position;
    }
}
