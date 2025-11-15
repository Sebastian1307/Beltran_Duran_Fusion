using Fusion;
using UnityEngine;

public class Collectable : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (Object.HasStateAuthority == false)
            return;

        var player = other.GetComponent<Player>();
        if (player)
        {
            player.AddPoint();
            Runner.Despawn(Object);
        }
    }
}
