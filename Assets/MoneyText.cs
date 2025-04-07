using UnityEngine;
using TMPro;

public class MoneyText : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    private void OnEnable()
    {
        if(GameManager.instance != null)

            GameManager.instance.onMoneyChanged += UpdateText;
    }

    void UpdateText() 
    {
        if (GameManager.instance != null)
            text.text = GameManager.instance.money + " NOK";
    }

    private void OnDisable()
    {
        GameManager.instance.onMoneyChanged -= UpdateText;
    }
}
