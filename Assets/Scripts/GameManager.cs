using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    
    public int ghostMultiplier { get; private set; }
    public int score { get; private set; }
    public int lives { get; private set; }

    public void Start()
    {
        NewGame();
    }

    public void Update()
    {
        if (Input.anyKeyDown && this.lives <= 0) 
        {
            NewGame();
        }
    }
    
    public void GhostEaten(Ghost eatenGhost)
    {
        SetScore(score + (eatenGhost.points * ghostMultiplier));
        ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);
        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
            ResetState();
        }
        else
        {
            GameOver();
        }
    }

    public void PelletEaten(Pellet eatenPellet)
    {
        eatenPellet.gameObject.SetActive(false);
        
        SetScore(this.score + eatenPellet.points);

        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet eatenPellet)
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            this.ghosts[i].frightenedBehavior.Enable(eatenPellet.duration);
        }
        
        PelletEaten(eatenPellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), eatenPellet.duration);
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
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
        ResetGhostMultiplier();
        
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }

        pacman.ResetState();
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

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }
}
