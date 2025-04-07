using UnityEngine;
using TMPro;

public class GameText : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    Vector3 dir;

    void Start()
    {
        dir = new Vector2(Random.Range(1f, -1f), Random.Range(1f, -1f));
        Destroy(gameObject, 0.5f);
    }

    private void Update()
    {
        transform.position += dir * Time.deltaTime;
    }

    public void InstanceText(string text, Color c) 
    {
        this.text.text = text;
        this.text.color = c;
    }

    public void InstanceText(string text)
    {
        InstanceText(text, Color.black);
    }
}