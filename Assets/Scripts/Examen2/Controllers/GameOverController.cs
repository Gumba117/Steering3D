using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverController : MonoBehaviour
{
    public CentralTowerController centralTower;
    public GameObject gameOverPanel, gameOverWin, gameOverLose;

    private void Update()
    {
        if (centralTower.gameOver)
        {
            gameOverPanel.SetActive(true);
            if (centralTower.win)
            {
                gameOverWin.SetActive(true);
            }
            else if (centralTower.lose)
            {
                gameOverLose.SetActive(true);
            }
        }
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
