using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PackageCollision : MonoBehaviour
{
    public int packagesCollected = 0;
    public AudioSource hitSound = null;
    public float hitSoundVolume = 0.0f;

    public Text packageDisplay = null;


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
            packagesCollected+=1;
            //Debug.Log("packages: " + packagesCollected.ToString());
            packageDisplay.text = "Packages: " + packagesCollected.ToString();
            Destroy(collision.gameObject);
            hitSound.volume = collision.relativeVelocity.magnitude * hitSoundVolume;
            //hitSound.volume = collision.impulse.magnitude * hitSoundVolume;
            //Debug.Log(hitSound.volume);
            hitSound.Play();
        }

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
