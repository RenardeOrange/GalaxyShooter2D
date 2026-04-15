using UnityEngine;
using TMPro;

public class UIEnd : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtScore;
    [SerializeField] private TextMeshProUGUI _txtRecord;


    void Start()
    {
        _txtScore.text = $"Pointage : {PlayerPrefs.GetInt("PlayerScore", 0)}";
        _txtRecord.text = $"Record    : {PlayerPrefs.GetInt("PlayerScore", 0)}";
    }

    public void OnDeleteRecord()
    {
        PlayerPrefs.DeleteKey("PlayerHighScore");
        PlayerPrefs.Save();
        _txtRecord.text = $"Record    : {PlayerPrefs.GetInt("PlayerScore", 0)}";
    }
}
