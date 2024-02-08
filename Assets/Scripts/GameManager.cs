using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    
    public int score { get; private set; }
    public int lives { get; private set; }

    public void Start()
    {
        NewGame();
    }
    
    public void GhostEaten(Ghost eatenGhost)
    {
        SetScore(score + eatenGhost.points);

    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);
        SetLives(this.lives - 1);

        if (this.lives >= 0)
        {
            Invoke(nameof(ResetState), 3.0f);
            ResetState();
        }
        else
        {
            GameOver();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(0);
        NewRound();
    }

    private void NewRound()
    {
        foreach (Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(true);
        }

        pacman.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }

        pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }
}
