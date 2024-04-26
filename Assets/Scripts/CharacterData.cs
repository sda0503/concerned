using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    //캐릭터 데이터 셋팅
    [SerializeField]
    public int charceterNum;
    public int placeNum;

    //0.강민우
    //1.한미래
    //2.경비원
    //3.경찰
    //4.흥신소탐정
    //5.변호사
    //6.부녀회장
    //7.김태현
    //8.신현우
    //9.택배회사
    //10.강민우 장례식장

    void Update()
    {

        switch (charceterNum)
        {
            case 0:
                if (GameManager.Instance.Playerinformation.date < 3)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    if(GameManager.Instance.Playerinformation.date % 4 == placeNum)
                    {
                        gameObject.SetActive(true);
                    }
                }
                break;
            case 3:
                if (GameManager.Instance.Playerinformation.date < 1)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
            case 8:
                if (GameManager.Instance.Playerinformation.date < 1)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;

            case 10:
                if (GameManager.Instance.Playerinformation.date < 3)
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
        }
                //Debug.Log(GameManager.Instance.Playerinformation.date);
    }
}
