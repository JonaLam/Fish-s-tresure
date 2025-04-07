using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerProdjectile : MonoBehaviour
{
    [SerializeField] Upgrade damageUpgrade;

    public abstract void InstanceProdjectile(PlayerMovement player, Vector2 aimTo);

    public int GetDamage() 
    {
        return Mathf.Max( GameManager.instance.GetUpgradeValue(damageUpgrade), 1);
    }
}
