using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    [HideInInspector] public List<FishBehaviour> currentFish = new List<FishBehaviour>();

    [SerializeField] TextMeshProUGUI fishText, levelText, oxygenText;

    [SerializeField] GameObject proceedBoxes;

    [SerializeField] Slider pullUp;

    float dragUp;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SummonFish());
        levelText.text = "Current Level: " + GameManager.instance.depth;
    }

    // Update is called once per frame
    void Update()
    {
        HandleOxygen();

        if (Input.GetKey(KeyCode.G))
        {
            dragUp += Time.deltaTime;
            if (dragUp > 1)
                GameManager.instance.GoToTop();
        }
        else
            dragUp = 0;
        pullUp.value = dragUp;
    }

    private void FixedUpdate()
    {
        if (proceedBoxes.activeSelf)
            return;

        if (GameManager.instance.oxygen > 0)
            return;

        GameManager.instance.PlayerDamage(1);
    }

    void HandleOxygen() 
    {
        if (proceedBoxes.activeSelf)
            return;

        if (GameManager.instance.oxygen <= 0)
            return;

        GameManager.instance.oxygen -= Time.deltaTime;
        oxygenText.text = Mathf.RoundToInt(GameManager.instance.oxygen) + " O2";
    }

    IEnumerator SummonFish() 
    {
        List<FishBehaviour> fish = waves[GameManager.instance.depth - 1].fish;

        while(fish.Count != 0) 
        {
            yield return new WaitForSeconds(Random.Range(0f, 1f));

            float xPos = 0;

            if (Random.Range(0f, 1f) > 0.5f)
                xPos = 18;
            else
                xPos = -18;

            FishBehaviour fishInstnace = Instantiate(fish[0]);
            currentFish.Add(fishInstnace);
            fishInstnace.roomManager = this;
            fishInstnace.transform.position = new Vector3(xPos, Random.Range(-9f, 9f));
            fish.RemoveAt(0);
            fishText.text = "Current Fish: " + currentFish.Count; 
        }
    }

    public void RemoveFish(FishBehaviour fish) 
    {
        currentFish.Remove(fish);
        fishText.text = "Current Fish: " + currentFish.Count;

        List<FishBehaviour> fishList = waves[GameManager.instance.depth - 1].fish;

        if (currentFish.Count == 0 && fishList.Count == 0) 
        {
            proceedBoxes.SetActive(true);
        }
    }
}
