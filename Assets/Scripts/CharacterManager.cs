using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : CharacterData
{


    // Update is called once per frame
    void Update()
    {
        switch(charceterNum)
        {
            case 0: if(GameManager.Instance.Playerinformation.date < 3)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
                break;
        }
    }
}
