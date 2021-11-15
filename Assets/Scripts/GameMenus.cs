using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenus : MonoBehaviour
{
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
        GetComponent<PickupSpawner>().GameOver();
        
        SkillTree skillTree = GetComponent<SkillTree>();
        skillTree._audioSource.Stop();
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
        SceneManager.LoadScene(0);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
