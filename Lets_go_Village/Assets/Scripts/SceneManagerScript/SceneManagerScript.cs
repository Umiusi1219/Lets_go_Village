using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    //�^�C�g����ʂŎg�p
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickExitButtom()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    //�Q�[���I�[�o�[��ʂŎg�p
    public void OnClickContinueButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
        CheckPointScript.m_nowCheckpoint = 0;
    }

    //�Q�[����ʂŎg�p
    public void PlayerDie()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void ToGate()
    {
        SceneManager.LoadScene("Boss_1Scene");
    }
}
