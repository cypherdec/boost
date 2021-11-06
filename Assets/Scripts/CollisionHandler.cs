using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float reloadDelay = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem succssParticles;
    [SerializeField] ParticleSystem crashParticles;
    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        DebugCommands();
    }

    void DebugCommands()
    {
        SkipLevel();
        DisableCollisions();
    }

    void OnCollisionEnter(Collision other)
    {  //other thing we bumped into 

        if (isTransitioning || collisionDisabled)
        {
            return;
        }

        switch (other.gameObject.tag)
        {

            case "Finish":
                SuccessSequence();
                break;

            case "Friendly":
               // Debug.Log("Friendly");
                break;

            default:
                CrashSequence();
                break;

        }
    }



    void CrashSequence()
    {
        crashParticles.Play();
        audioSource.Stop();
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashSound, 1);
        Invoke("ReloadLevel", reloadDelay);
    }



    void SuccessSequence()
    {
        succssParticles.Play();
        audioSource.Stop();
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(successSound, 1);
        Invoke("NextLevel", reloadDelay);
    }



    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);  // can use scene index of scene name
    }



    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }

    }


    void SkipLevel()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }
    }

    void DisableCollisions()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
            Debug.Log("colliosn DIsbaled : " + collisionDisabled);

        }
    }



}
