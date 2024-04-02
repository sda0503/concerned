using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    //GameManager�� ������ �� ����
    public Button inventoryButton;
    public Button phoneButton;
    public Button mapButton;
    public Button human1Button;
    public Button human2Button;

    public GameObject TextCanvas;
    public TMP_Text nameText;

    public Transform phoneNumberListPosition;
    public List<GameObject> phoneNumberList; //��ųʸ��� ����

    private void Start()
    {
        phoneButton.onClick.AddListener(OnPhone);
        human1Button.onClick.AddListener(() => TalkHuman("asdf"));
        human2Button.onClick.AddListener(() => TalkHuman("1234"));
    }

    private void TalkHuman(string name) //���Ƿ� ���� name. ���߿� dialogue�� �����ؼ� �ٲ� ����
    {
        //GameManager�� ���ű� ������ ���� PhoneNumberUI�� �̵� ����.
        nameText.text = name;
        phoneNumberList.Add(DataManager.Instance.GameObjectLoad("Prefabs/PhoneNumberList"));
        phoneNumberList[phoneNumberList.Count - 1].GetComponentInChildren<Text>().text = name;
        Instantiate(phoneNumberList[phoneNumberList.Count - 1], phoneNumberListPosition);
    }


    private void OnPhone()
    {
        PopupUIManager.Instance.OpenPopupUI<PhoneUI>();
    }
}
