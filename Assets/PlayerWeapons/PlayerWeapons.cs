using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerWeapons : ScriptableObject
{
    public PlayerProdjectile prodjectile;
    public Upgrade ammoUpgrade;
    public Upgrade damageUpgrade;
    public Sprite weaponSprite;
}

[System.Serializable]
public class PlayerWeaponInstance 
{
    public PlayerWeapons weapon;
    public int ammo;
}
