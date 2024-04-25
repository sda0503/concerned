using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    //ĳ���� ������ ����
    [SerializeField]
    public int charceterNum;
    public int placeNum;

    //0.���ο�
    //1.�ѹ̷�
    //2.����
    //3.����
    //4.��ż�Ž��
    //5.��ȣ��
    //6.�γ�ȸ��
    //7.������
    //8.������
    //9.�ù�ȸ��
    //10.���ο� ��ʽ���

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
