using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float crashDelay = 1f;  
    [SerializeField] AudioClip crashExplosion;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

     AudioSource audioSource;

     bool isTransitioning = false;
     bool collisionDisabled = false;
    void Start() 
      {
          audioSource = GetComponent<AudioSource>();
      }
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
     void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisabled) { return; }

        switch(other.gameObject.tag)
        {
            case "Friendly" :
                Debug.Log("We hit a friendly object!");
                break;
            case "Finish" :
                FinishSequence();
                break;
            default:
                StartCrashSequence();
                break;

        }
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashExplosion);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",crashDelay);
    }
    void FinishSequence()
    {   
        isTransitioning = true;
        successParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel",crashDelay);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
