using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PostWebRequest : MonoBehaviour
{
    //IP address
    public string url;
    //post number
    public string port;

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RequestPost($"{url}:{port}")); //조립형태 string으로 넘겨줌
        }
    }

    //post request func
    IEnumerator RequestPost(string requestURL)//한방에 보내주자
    { 
        //폼 객체 생성
        WWWForm form = new WWWForm();
        
        //임시 입력을 해보자
        form.AddField("id", "jh");
        form.AddField("pw", "123456");//해시테이블 key , value 형식과 닮음 -> javascript 객체 타입으로 받기 좋음

        // post 요청 객체 생성
        UnityWebRequest request = UnityWebRequest.Post(requestURL, form);//주소값, 폼 객체를 넘김 -> 패킷처리에 의해 전송 (서버 구축시 하드웨어에서 방식) :IP관리대역이 아니면 끊어버림
        // post 요청 전달
        yield return request.SendWebRequest();
        //결과 확인
        if(request.result == UnityWebRequest.Result.Success) 
        {
            Debug.Log(request.downloadHandler.text);
        }
    }
}
