using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed;

    bool aiming = false;
    LineRenderer lr;

    Camera cam;

    int currentWeapon;

    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] Image WeaponImage;
    [SerializeField] SpriteRenderer weaponSpriteRendere;

    [SerializeField] Transform weaponHolder;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        lr = gameObject.GetComponent<LineRenderer>();
        cam = Camera.main;
        UpdateAmmoText();
        WeaponImage.sprite = GameManager.instance.PlayerWeaponInstances[currentWeapon].weapon.weaponSprite;
        weaponSpriteRendere.sprite = GameManager.instance.PlayerWeaponInstances[currentWeapon].weapon.weaponSprite;
    }

    void Update()
    {
        HandleMovement();
        ManageAiming();
        HandleAimRot();
        if (Input.GetMouseButtonDown(1))
        {
            StartAiming();
        }
        if (Input.GetMouseButtonUp(1))
        {
            StopAiming();
        }

        if(Input.GetMouseButtonDown(0)) 
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
            ChangeWeapon();
    }

    void HandleAimRot() 
    {
        Vector2 dir = (Vector3)cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        weaponHolder.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void ChangeWeapon() 
    {
        currentWeapon++;
        if (currentWeapon >= GameManager.instance.PlayerWeaponInstances.Count)
            currentWeapon = 0;
        UpdateAmmoText();
        WeaponImage.sprite = GameManager.instance.PlayerWeaponInstances[currentWeapon].weapon.weaponSprite;
        weaponSpriteRendere.sprite = GameManager.instance.PlayerWeaponInstances[currentWeapon].weapon.weaponSprite;
    }

    private void HandleMovement()
    {
        float xInput = Input.GetAxisRaw("Horizontal") * speed;
        float yInput = Input.GetAxisRaw("Vertical") * speed;

        if (yInput == 0)
            yInput = -0.3f;

        float multi = 1;

        if (aiming)
            multi = 0.2f;

        rb.linearVelocity = new Vector2(xInput, yInput) * multi;
    }

    void ManageAiming() 
    {
        if (!aiming)
            return;

        lr.SetPosition(0, transform.position);

        Vector2 dir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.Normalize();

        lr.SetPosition(1, transform.position + (Vector3)dir * 100);
    }

    void StartAiming() 
    {
        if (aiming)
            return;

        lr.enabled = true;
        aiming = true;
    }

    void StopAiming() 
    {
        if (!aiming)
            return;

        lr.enabled = false;
        aiming = false;
    }

    void Shoot() 
    {
        PlayerWeaponInstance weaponInstance = GameManager.instance.PlayerWeaponInstances[currentWeapon];

        if (weaponInstance.ammo > 0)
        {
            weaponInstance.ammo--;
            PlayerProdjectile prodjectileInstance = Instantiate(weaponInstance.weapon.prodjectile);
            prodjectileInstance.InstanceProdjectile(this, cam.ScreenToWorldPoint(Input.mousePosition));
            UpdateAmmoText();
        }
    }

    void UpdateAmmoText() 
    {
        PlayerWeaponInstance weaponInstance = GameManager.instance.PlayerWeaponInstances[currentWeapon];

        ammoText.text = weaponInstance.ammo + "/" + Mathf.Max(3, GameManager.instance.GetUpgradeValue( weaponInstance.weapon.ammoUpgrade));
    }
}
