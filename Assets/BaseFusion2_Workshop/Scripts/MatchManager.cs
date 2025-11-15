using UnityEngine;
using Fusion;
using TMPro;

public class MatchManager : NetworkBehaviour
{
    public GameObject panelEndGame;
    public TextMeshProUGUI playerWinnerName;
    public void EndMatch(PlayerRef winner)
    {
        Debug.Log($"Player winner is: {winner}");
        playerWinnerName.text = winner.ToString();
        panelEndGame.SetActive( true );

        StopAllCoroutines();
        Time.timeScale = 0f;

        
    }
}
