using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DataLoad : MonoBehaviour
{
    private string url = "https://wou6c330.mycafe24.com/Images/DOG.jpg"; //이미 www안이라서 붙여줄 필요 없음.(카페24는 그럼)
  
    [SerializeField] private RawImage _image;
  
       
   void Start()
   { 
       StartCoroutine(nameof(Imageset));
   }

   IEnumerator Imageset()
   {
       UnityWebRequest form = UnityWebRequestTexture.GetTexture(url);
       
       yield return form.SendWebRequest();//서버에서 내려받을때까지 기다림.
       
       
       
       if (!string.IsNullOrEmpty(form.error))
       {
           Debug.Log(form.error);
           yield break;
       }
       else
       {
           _image.texture = ((DownloadHandlerTexture)form.downloadHandler).texture; //이미지가 크면 딜레이가 걸림. (5MB이상부터는 대응해줘야함)
           //raw이미지 알아보기
           //다른이미지 불러올때 파기하고 불러와야 메모리적으로 이득 파기하는 건 Destroy 불러온게 남아있지 않게 파기
       }
       
   }
}
