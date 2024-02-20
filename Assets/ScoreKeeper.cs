using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] string infoText = "The audience couldn't get enough. Score:";

    // Start is called before the first frame update
    void Start()
    {
        string letterGrade = "D";
        if (GameManager.Instance.Audience.TotalPoints > 500) letterGrade = "D";
        if (GameManager.Instance.Audience.TotalPoints > 700) letterGrade = "C";
        if (GameManager.Instance.Audience.TotalPoints > 850) letterGrade = "B";
        if (GameManager.Instance.Audience.TotalPoints > 900) letterGrade = "A-";
        if (GameManager.Instance.Audience.TotalPoints > 950) letterGrade = "A";
        if (GameManager.Instance.Audience.TotalPoints > 975) letterGrade = "A+";

        scoreText.text = infoText + letterGrade;
    }
}
