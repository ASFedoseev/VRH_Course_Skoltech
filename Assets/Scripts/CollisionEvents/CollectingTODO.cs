using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CollectingTODO : MonoBehaviour
{
    public int collectingPackages = 0;
    public int collectingRubys = 0;
    public int collectingPills = 0;
    public int score = 0;
    public Text scoreText = null;
    public GameObject WinPanel = null;
    public Transform spawmPoint = null;
    public AudioSource hitSound = null;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DisplayScore();

        if (score > 3)
        {
            WinPanel.SetActive(true);
        }
        if (Input.GetKey(KeyCode.R))
        {
            Restart();

        }
        

}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Package")
        {
            collectingPackages += 1;
            score += 1;
            Destroy(collision.gameObject);
            hitSound.volume = collision.impulse.magnitude * 1000f;
            hitSound.Play();
        }
    }
    void DisplayScore()
    {
        scoreText.text = "Collected packages: " + score.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
