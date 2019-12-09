using UnityEngine;
using System.Collections;

public class ItemContact : MonoBehaviour
{
    public GameObject playerExplosion;
    public GameObject explosion;
    public int scoreValue;
    private GameController gameController;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.tag == "Item")
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }


        if (other.tag == "Player" | other.tag == "bolt")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            GameObject.Find("Player").GetComponent<PlayerController>().fireRate = .05f;
        }
        gameController.AddScore(scoreValue);
        Destroy(gameObject);
    }

}