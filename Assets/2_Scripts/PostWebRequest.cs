using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using static UnityEditor.Progress;


public class PostWebRequest : MonoBehaviour
{
    [Serializable]
    public class TestData
    {
        public string name;
    }

    /// <summary>
    /// IP address
    /// </summary>
    public string url;
    /// <summary>
    /// post number
    /// </summary>
    public string port;
    /// <summary>
    /// UI ID_InputFields,아이디 입력을 위한 입력 필드
    /// </summary>
    public TMPro.TMP_InputField id_InputField;
    /// <summary>
    /// UI PW_InputFields, 비밀번호 입력을 위한 입력 필드
    /// </summary>
    public TMPro.TMP_InputField pw_InputField;


    public TestData testData;

    /// <summary>
    /// 이벤트 등록
    /// </summary>
    //public UnityEvent myTestEvents;

    //public Dictionary<UnityEngine.UI.Button, UnityAction> eventDictionary;
    
    private void Awake()
    {
        /*
        eventDictionary = new Dictionary<UnityEngine.UI.Button, UnityAction>();

        foreach(var item in eventDictionary)
        {
            item.Key.onClick.AddListener(item.Value);
        }
        */
        /*
        UnityEngine.UI.Button button = GetComponentInChildren<UnityEngine.UI.Button>();
        button.onClick.AddListener(OnLoginButtonClicked);
        //디버깅하는데 시간이 더 많이 쓴다. Listener 
        //카테고리 별로 스크립터블 오브젝트 하나에 전부 담아둔다. 
        //딕셔너리에 key로 등록을 해두는 방법도 있다.
        */
    }

    /* 행동을 하지 않아도 배치해주면, 로드가 걸린다. Update 함수가 성능에 영향을 주기 때문에 없애준다.
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space))//테스트용
        {
            StartCoroutine(RequestPost($"{url}:{port}")); //조립형태 string으로 넘겨줌
        }
    }
    */

    /// <summary>
    /// 로그인 버튼 눌리면 실행할 함수
    /// </summary>
    public void OnLoginButtonClicked()
    {
        //입력 컨트롤 참조가 null이면 반환
        if (id_InputField == null || pw_InputField == null)
        {
            return;
        }
        //입력 값(문자열)이 없으면 반환
        if(string.IsNullOrEmpty(id_InputField.text) == true ||
           string.IsNullOrEmpty(pw_InputField.text) == true)
        {
            return;
        }//string.IsNullOrWhiteSpace 빈문자 : 공란 == 입력x 도 확인해야 한다.

        //문제 없으면, 로그인 요청 -> 서버로
        //url:port 구조로 문자열을 만들어서 요청 함수 호출
        StartCoroutine(RequestPost($"{url}:{port}")); //조립형태 string으로 넘겨줌
    }

    //post request func
    IEnumerator RequestPost(string requestURL)//한방에 보내주자
    { 
        //폼 객체 생성
        WWWForm form = new WWWForm();//GET의 쿼리가 보안에 취약하니 POST 폼 형태로 씌워줌

        /*임시 입력을 해보자
        form.AddField("id", "jh");
        form.AddField("pw", "123456");//해시테이블 key , value 형식과 닮음 -> javascript 객체 타입으로 받기 좋음
        */

        //입력 받은 것 기준으로 수정한다.
        form.AddField("id", id_InputField.text);
        form.AddField("pw", pw_InputField.text);

        // post 요청 객체 생성
        UnityWebRequest request = UnityWebRequest.Post(requestURL, form);//주소값, 폼 객체를 넘김 -> 패킷처리에 의해 전송 (서버 구축시 하드웨어에서 방식) :IP관리대역이 아니면 끊어버림
        
        // post 요청 전달
        yield return request.SendWebRequest();
        //결과 확인
        if(request.result == UnityWebRequest.Result.Success) 
        {
            Debug.Log(request.downloadHandler.text);
            testData = JsonUtility.FromJson<TestData>(request.downloadHandler.text);
        }
    }
}
