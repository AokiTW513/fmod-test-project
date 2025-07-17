using UnityEngine;

public class Coin : MonoBehaviour
{
    public string id;

    private void Start()
    {
        if (SaveManager.instance.IsCollected(id))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.AddCoin();
            SaveManager.instance.MarkCollected(id);
            Destroy(gameObject);
        }
    }
}