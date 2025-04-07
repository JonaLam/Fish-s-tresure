using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Dictionary<Upgrade, int> upgradeAndLevel = new Dictionary<Upgrade, int>();

    public List<PlayerWeaponInstance> PlayerWeaponInstances = new List<PlayerWeaponInstance>();

    public int depth = 0;

    public int money;

    [SerializeField] GameText gameText;

    public float oxygen;

    public delegate void GameManagerEvent();
    public GameManagerEvent onMoneyChanged;
    public GameManagerEvent onHealthChanged;

    [SerializeField] Upgrade oxygenUpgrade, healthUpgrade;
    public int playerHealth;
    public void ChangeMoney(int amount) 
    {
        money += amount;

        if (onMoneyChanged != null)
            onMoneyChanged();
    }

    public void PlayerDamage(int Amount) 
    {
        playerHealth -= Amount;

        if (onHealthChanged != null)
            onHealthChanged();

        if (playerHealth <= 0)
            GoToTop();
    }

    void Start()
    {
        if(instance != null) 
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;    
    }

    public void PrepareDive() 
    {
        oxygen = Mathf.Max(10, GetUpgradeValue(oxygenUpgrade));
        playerHealth = GetUpgradeValue(healthUpgrade);
        foreach (var item in PlayerWeaponInstances)
        {
            item.ammo = Mathf.Max( GetUpgradeValue(item.weapon.ammoUpgrade), 3);
        }
    }

    public void GoToTop() 
    {
        depth = 0;
        SceneManager.LoadScene("BoatScene");
    }

    public void GoDeeper() 
    {
        depth++;
        SceneManager.LoadScene("FishingScene");
    }

    public int GetUpgradeValue(Upgrade upgrade) 
    {
        if (upgradeAndLevel.ContainsKey(upgrade))
            return upgrade.values[GetUpgradeLevel(upgrade) - 1];
        else
            return 0;
    }

    public int GetUpgradeLevel(Upgrade upgrade)
    {
        if (upgradeAndLevel.ContainsKey(upgrade))
            return upgradeAndLevel[upgrade];
        else
            return 0;
    }

    public void LevelUpgrade(Upgrade upgrade) 
    {
        if (upgradeAndLevel.ContainsKey(upgrade)) 
        {
            upgradeAndLevel[upgrade]++;
        }
        else 
        {
            upgradeAndLevel.Add(upgrade, 1);
        }
    }

    public void DrawTextOnScreen(Vector2 pos, string text, Color c) 
    {
        GameText textInstance = Instantiate(gameText, pos, Quaternion.identity);
        textInstance.InstanceText(text, c);
    }

    public void DrawTextOnScreen(Vector2 pos, string text)
    {
        DrawTextOnScreen(pos, text, Color.black);
    }
}
