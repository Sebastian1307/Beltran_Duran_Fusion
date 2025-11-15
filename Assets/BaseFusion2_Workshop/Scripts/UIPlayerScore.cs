using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fusion;

public class UIPlayerScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private Player localPlayer;

    private void Update()
    {
        if (localPlayer == null)
        {
            foreach (var player in FindObjectsOfType<Player>())
            {
                if (player.Object.HasInputAuthority)
                    localPlayer = player;
            }
            return;
        }

        scoreText.text = "Score: " + localPlayer.Score;
    }
}
