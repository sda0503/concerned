using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum ItemType
{
    photo,
    objects
}

public enum ItemEventType
{
    Normal,
    Event,
    usable,
    interaction
}

public class ItemData
{
    public int ItemID;
    public string Name;
    public string Description;
    public string Simple;
    public ItemEventType itemEventType;
    public ItemType itemType;
    public string event_description;
}

public class ItemDataList
{
    public List<ItemData> Data;
    public List<ItemData> 시트2;
}

public class DataLoad : MonoBehaviour
{
    private string url = "https://wou6c330.mycafe24.com/Images/DOG.jpg"; //이미 www안이라서 붙여줄 필요 없음.(카페24는 그럼)

    [SerializeField] private RawImage _image;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    [SerializeField] private Button _button;

    //private string url;
    private List<bool> _inventory;

    private string path;
    private int dlsmallidx = 0;
    private int dllargeidx = 0;

    void Start()
    {
        _inventory = new List<bool>(new bool[45]);
        path = Application.persistentDataPath + "/Inventory.json";
        _button.onClick.AddListener(OnClickBtn);
        //_textMeshProUGUI.text = ItemInfo.Data.DataMap[0].check_get ? "True" : "False";
        //
        // url = ItemInfo.Data.DataList[4].Des;
        // StartCoroutine(nameof(Imageset));

        //DOGAM();
//        ItemDataList a = new ItemDataList();

         var b = Resources.Load("ItemInfo (2)").ToString();
            //ItemDataList a = new ItemDataList();
         var a = JsonConvert.DeserializeObject<ItemDataList>(b);
         ItemData[] dogam = new ItemData[24];
         dogam[24] = a.Data[24];
         // foreach (var VARIABLE in a.Data)
         // {
         //     Debug.Log(VARIABLE.Name);
         // }
    }

    public void Save()
    {
        var savedata = JsonConvert.SerializeObject(_inventory);

        File.WriteAllText(path, savedata.ToString());
    }


    IEnumerator Imageset()
    {
        UnityWebRequest form = UnityWebRequestTexture.GetTexture(url);

        yield return form.SendWebRequest(); //서버에서 내려받을때까지 기다림.


        if (!string.IsNullOrEmpty(form.error))
        {
            Debug.Log(form.error);
            yield break;
        }
        else
        {
            _image.texture =
                ((DownloadHandlerTexture)form.downloadHandler).texture; //이미지가 크면 딜레이가 걸림. (5MB이상부터는 대응해줘야함)
            //raw이미지 알아보기
            //다른이미지 불러올때 파기하고 불러와야 메모리적으로 이득 파기하는 건 Destroy 불러온게 남아있지 않게 파기
        }
    }

    private void OnClickBtn()
    {
        
    }

    private void DOGAM()
    {
        var load = File.ReadAllText(path);
        var Load = JsonConvert.DeserializeObject<List<bool>>(load);

        int i = 0;
        // foreach (var VARIABLE in Load)
        // {
        //     if (VARIABLE)
        //     {
        //         if (ItemInfo.Data.DataList[i].itemType == ItemType.Event)
        //         {
        //             StartCoroutine(nameof(Imageset));
        //             Debug.Log($"index : {i} / {VARIABLE} / {ItemInfo.Data.DataList[i].itemType}");
        //         }
        //     }
        //
        //     i++;
        // }

        i = 0;
    }
}