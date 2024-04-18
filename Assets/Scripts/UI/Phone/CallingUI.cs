using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallingUI : PopupUIBase
{
    public Image characterImage;
    public Text characterText;
    
    public void OnSet(string name)
    {
        characterImage.sprite = DataManager.Instance.SpriteLoad("Image/Phone/" + name);
        characterText.text = name;
    }
}
