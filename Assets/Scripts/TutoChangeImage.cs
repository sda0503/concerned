using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoChangeImage : MonoBehaviour
{
    public Sprite[] tutoImage;
    int imageCnt = 0;
    bool showImageBool = false;
    public void ChangeImage()
    {
        imageCnt++;
        if(imageCnt == 5)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            gameObject.transform.parent.transform.GetChild(0).GetComponent<Image>().sprite = tutoImage[imageCnt];
            GameManager.Instance.PositionChange(300);
            DialogueManager.Instance.StartDialogue("³ª(Æ©Åä¸®¾ó-½£)");

            DialogueManager.Instance.TargetNameChange += showImage;
        }
        else if (imageCnt == 7)
        {
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
            Debug.Log("´ëÈ­ ³¡");
            gameObject.transform.parent.gameObject.SetActive(true);
            showImageBool = true;
        }
    }
}
