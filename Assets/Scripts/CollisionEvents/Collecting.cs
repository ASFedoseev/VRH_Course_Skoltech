using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Collecting : MonoBehaviour
{
    public int collectingPackages = 0;
    public int score = 0;
    public Text scoreText = null;
    public AudioSource hitSound = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Package")
        {
            collectingPackages += 1;
            score += 10;
            Destroy(collision.gameObject);
            DisplayScore();
            hitSound.volume = collision.impulse.magnitude * 1000f;
            hitSound.Play();
        }
    }
    private void DisplayScore()
    {
        Debug.Log("Score: " + scoreText);
    }
}
