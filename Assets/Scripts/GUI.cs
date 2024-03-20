using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    //GameManager로 내용이 들어갈 예정
    public Button inventoryButton;
    public Button phoneButton;
    public Button mapButton;
    public Button human1Button;
    public Button human2Button;

    public GameObject TextCanvas;
    public TMP_Text nameText;

    public Transform phoneNumberListPosition;
    public List<GameObject> phoneNumberList; //딕셔너리로 관리

    private void Start()
    {
        phoneButton.onClick.AddListener(OnPhone);
        human1Button.onClick.AddListener(() => TalkHuman("asdf"));
        human2Button.onClick.AddListener(() => TalkHuman("1234"));
    }

    private void TalkHuman(string name) //임의로 받은 name. 나중에 dialogue랑 연결해서 바꿀 예정
    {
        //GameManager로 갈거기 때문에 이후 PhoneNumberUI로 이동 예정.
        nameText.text = name;
        phoneNumberList.Add(Utility.Instance.GameObjectLoad("Prefabs/PhoneNumberList"));
        phoneNumberList[phoneNumberList.Count - 1].GetComponentInChildren<Text>().text = name;
        Instantiate(phoneNumberList[phoneNumberList.Count - 1], phoneNumberListPosition);
    }


    private void OnPhone()
    {
        PopupUIManager.Instance.OpenPopupUI<PhoneUI>();
    }
}
