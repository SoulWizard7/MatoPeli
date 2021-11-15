using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenus : MonoBehaviour
{
    // This script handles pause menu and the game over menu & game over functions
    
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    [SerializeField] private Rigidbody _playField;
    private void Awake() => PauseGameNow(); 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) PauseGameNow();
    }

    private void PauseGameNow()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        pauseMenu.SetActive(pauseMenu.activeSelf != true);
    }
    
    public void GameOver()
    {
        TickManager.tick.RemoveAllListeners();
        _playField.useGravity = true;
        
        Rigidbody[] allChildren = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody child in allChildren)
        {
            child.useGravity = true;
            child.GetComponent<Collider>().isTrigger = false;
        }
        
        SkillTree skillTree = GetComponent<SkillTree>();
        skillTree._audioSource.Stop();
        skillTree._audioSource.loop = false;
        skillTree._audioSource.pitch = 1;
        skillTree._audioSource.clip = skillTree.songs[6];
        skillTree._audioSource.Play();

        StartCoroutine(GameOverMenu());
    }

    private IEnumerator GameOverMenu()
    {
        yield return new WaitForSeconds(5);
        gameOverMenu.SetActive(true);
    }

    public void RestartGame()
    {
        SkillTree._skillPoint = 1;
        SceneManager.LoadScene(0);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
