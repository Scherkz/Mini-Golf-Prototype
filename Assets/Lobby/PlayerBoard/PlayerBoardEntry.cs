using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBoardEntry : MonoBehaviour
{
    private Image colorCircle;
    private TMP_Text nameText;
    private TMP_Text mapVoteText;
    private TMP_Text readyText;
    private bool ready = false;

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

    public void SetPlayerName(string name)
    {
        if (nameText != null)
            nameText.text = $"{name}";
    }

    public void SetMapVote(string vote)
    {
        if (mapVoteText == null) return;

        mapVoteText.text = vote == null || vote == ""
            ? "—"
            : $"{vote}";
    }

    public void ToggleReady()
    {
        if (readyText == null) return;
        ready = !ready;
        readyText.text = ready ? "(Ready)" : "(Not Ready)";
        readyText.color = ready ? Color.green : Color.red;
    }
}
