using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static LobbyManager;

public class PlayerBoardEntry : MonoBehaviour
{
    private Image colorCircle;
    private TMP_Text nameText;
    private TMP_Text mapVoteText;
    private TMP_Text readyText;

    private void Awake()
    {
        colorCircle = transform.Find("ColorCircle")?.GetComponent<Image>();
        nameText = transform.Find("NameText")?.GetComponent<TMP_Text>();
        mapVoteText = transform.Find("MapVoteText")?.GetComponent<TMP_Text>();
        readyText = transform.Find("ReadyText")?.GetComponent<TMP_Text>();
    }

    public void SetPlayerColor(Color color)
    {
        if (colorCircle != null)
            colorCircle.color = color;
    }

    public void SetPlayerName(int playerID)
    {
        if (nameText != null)
            nameText.text = $"Player {playerID}:";
    }

    public void SetMapVote(string vote)
    {
        if (mapVoteText == null) return;

        mapVoteText.text = vote == null || vote == ""
            ? "—"
            : $"{vote}";
    }

    public void SetReady(bool ready)
    {
        if (readyText == null) return;
        readyText.text = ready ? "(Ready)" : "(Not Ready)";
        readyText.color = ready ? Color.green : Color.red;
    }
}
