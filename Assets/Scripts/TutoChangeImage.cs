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
            gameObject.transform.parent.GetChild(0).GetComponent<Image>().sprite = tutoImage[imageCnt];
            //gameObject.transform.parent.GetChild(2).gameObject.SetActive(false);
            //gameObject.transform.parent.GetChild(3).gameObject.SetActive(false);
            GameManager.Instance.PositionChange(300);
            DialogueManager.Instance.StartDialogue("나(튜토리얼-숲)");

            DialogueManager.Instance.TargetNameChange += showImage;
            
        }
        else if (imageCnt == 7)
        {
           
            GameManager.Instance.Playerinformation.position = 300;
            UIManager.CanvasGroup.Clear();
            UIManager.Instance.DeleteListener();
            SceneManager.LoadScene("TestScene 1");
            gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.parent.GetChild(0).GetComponent<Image>().sprite = tutoImage[imageCnt];
        }
    }

    void showImage()
    {
        if (!showImageBool)
        {
            Debug.Log("��ȭ ��");
            gameObject.transform.parent.parent.transform.GetChild(0).gameObject.SetActive(false);
            canvas.SetActive(false);
            gameObject.transform.parent.gameObject.SetActive(true);
            gameObject.transform.parent.parent.transform.GetChild(1).gameObject.SetActive(false);

            showImageBool = true;
        }
    }
}
