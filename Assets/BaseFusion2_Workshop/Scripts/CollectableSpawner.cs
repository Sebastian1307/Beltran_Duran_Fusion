using Fusion;
using UnityEngine;

public class CollectableSpawner : NetworkBehaviour
{
    [SerializeField] private NetworkPrefabRef collectablePrefab;

    [SerializeField] private Vector3 areaMin = new Vector3(-5, 1, -5);
    [SerializeField] private Vector3 areaMax = new Vector3(5, 1, 5);

    [Networked] private TickTimer spawnTimer { get; set; }

    public override void Spawned()
    {
        if (Runner.IsServer)
            spawnTimer = TickTimer.CreateFromSeconds(Runner, 3f);
    }

    public override void FixedUpdateNetwork()
    {
        if (Runner.IsServer == false)
            return;

        if (spawnTimer.Expired(Runner))
        {
            SpawnCollectable();
            spawnTimer = TickTimer.CreateFromSeconds(Runner, 3f);
        }
    }

    private void SpawnCollectable()
    {
        Vector3 pos = new Vector3(
            Random.Range(areaMin.x, areaMax.x),
            Random.Range(areaMin.y, areaMax.y),
            Random.Range(areaMin.z, areaMax.z)
        );

        Runner.Spawn(collectablePrefab, pos, Quaternion.identity);
    }
}
