using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class TutoChangeImage : MonoBehaviour
{
    public Sprite[] tutoImage;
    int imageCnt = 0;
    bool showImageBool = false;
    public GameObject canvas;
    public void ChangeImage()
    {
        imageCnt++;
        Debug.Log(imageCnt);
        if (imageCnt == 5)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            gameObject.transform.parent.transform.GetChild(0).GetComponent<Image>().sprite = tutoImage[imageCnt];
            GameManager.Instance.PositionChange(300);
            DialogueManager.Instance.StartDialogue("나(튜토리얼-숲)");

            DialogueManager.Instance.TargetNameChange += showImage;
            
        }
        else if (imageCnt == 7)
        {
            SceneManager.LoadScene("TestScene 1");
            gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.parent.transform.GetChild(0).GetComponent<Image>().sprite = tutoImage[imageCnt];
        }
    }

    void showImage()
    {
        if (!showImageBool)
        {
            Debug.Log("��ȭ ��");
            gameObject.transform.parent.parent.transform.GetChild(1).gameObject.SetActive(false);
            canvas.SetActive(false);
            gameObject.transform.parent.gameObject.SetActive(true);
            gameObject.transform.parent.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            showImageBool = true;
        }
    }
}
