using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CallingUI : PopupUIBase
{
    public Image characterImage;
    public TextMeshProUGUI characterText;
    
    public void OnSet(string name)
    {
        characterImage.sprite = DataManager.Instance.SpriteLoad("Image/Phone/" + name);
        characterText.text = name;
    }

    public void OnDestroy()
    {
        characterImage.sprite = null;
        characterText.text = null;
        PopupUIManager.Instance.popupUI.Remove("CallingUI");
        Destroy(gameObject);
    }
}
