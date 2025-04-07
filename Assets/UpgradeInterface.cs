using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class UpgradeInterface : MonoBehaviour
{
    [SerializeField] Upgrade upgrade;
    [SerializeField] Image upgradeTier;
    [SerializeField] Transform upgradeTierParent;
    [SerializeField] TextMeshProUGUI nameText, costText, ValueText;
    [SerializeField] GameObject buyButton;

    int cost;
    List<Image> images = new List<Image>();
    

    private void Start()
    {

        InstanceUpgradeTiers();
        SetUpgrade();
    }

    public void Buy() 
    {
        if(GameManager.instance.money >= cost) 
        {
            GameManager.instance.ChangeMoney(-cost);
            GameManager.instance.LevelUpgrade(upgrade);
            SetUpgrade();
            InstanceUpgradeTiers();
        }
    }

    public void SetUpgrade() 
    {
        if (upgrade == null)
            return;

        if (GameManager.instance.GetUpgradeLevel(upgrade) >= upgrade.prices.Length)
        {
            buyButton.SetActive(false);
            costText.text = "";
        }
        else
        {

            cost = upgrade.prices[GameManager.instance.GetUpgradeLevel(upgrade)];
            costText.text =  cost.ToString();
        }
        nameText.text = upgrade.name;
        ValueText.text = GameManager.instance.GetUpgradeValue(upgrade).ToString();
    }

    void InstanceUpgradeTiers() 
    {
        foreach (var item in images)
        {
            Destroy(item.gameObject);
        }

        images.Clear();

        Vector2 pos = upgradeTierParent.transform.position;

        for (int i = 0; i < upgrade.prices.Length; i++)
        {

            Image imageInstance = Instantiate(upgradeTier, pos, Quaternion.identity, upgradeTierParent);

            images.Add(imageInstance);

            if (i < GameManager.instance.GetUpgradeLevel(upgrade))
                imageInstance.color = Color.green;

            pos += Vector2.right * 13;
        }
    }
}
