using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //instance
    public static GameController instance;

    //reference to characters
    public Character charOne;
    public Character charTwo;

    //Game
    public bool isFrozen;
    public string scene;
    bool isEnemyDead;
    bool isHeartDead;

    //UI
    public Image enemyHealthBar;
    public GameObject panel;
    public GameObject victoryPanel;


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isFrozen = false;
        isEnemyDead = false;
        isHeartDead = false;
        HidePanel();
        HideVictory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Game
    public void FreezeMotion()
    {
        isFrozen = true;
    }

    public void EnableMotion()
    {
        isFrozen = false;
    }

    public void HeartIsDead(GameObject heart)
    {
        if (!isEnemyDead)
        {
            isHeartDead = true;
            FreezeMotion();
            ShowPanel();
            charOne.ToSad();
            charTwo.ToSad();
            Destroy(heart);

        }
        
    }

    public void EnemyIsDead()
    {
        if (!isHeartDead)
        {
            isEnemyDead = true;
            charOne.ToGlad();
            charTwo.ToGlad();
            ShowVictory();
        }
        
    }


    //UI
    public void ChangeEnemyHealthBar(float amount)
    {
        enemyHealthBar.fillAmount = amount;
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }

    public void ShowVictory()
    {
        victoryPanel.SetActive(true);
    }

    public void HideVictory()
    {
        victoryPanel.SetActive(false);
    }



    //listeners
    public void RestartGame()
    {
        SceneManager.LoadScene(scene);
    }

    public void ToCreditScene()
    {
        SceneManager.LoadScene("Ending");
    }


}
