using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OxygenWarning : MonoBehaviour
{
    [SerializeField] Image image;
    bool warning = false;

    void Update()
    {
        if (warning)
            return;

        if (GameManager.instance.oxygen < 5)
            StartCoroutine(Warning());
    }

    IEnumerator Warning() 
    {
        warning = true;

        while (true) 
        {
            image.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            image.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
    }
    
}
