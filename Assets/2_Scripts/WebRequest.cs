using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebRequest : MonoBehaviour
{
    #region 필요한 변수

    /// <summary>
    /// url 주소
    /// </summary>
    public string url;

    
    /// <summary>
    /// 포트 번호: 숫자를 받지만 처리는 문자열이기 때문에 string으로 선언
    /// </summary>
    public string port;

    public RawImage image;

    #endregion

    private void Update()
    {
        //Space 키를 누르면 서버에 요청하는 메세지 전달
        if (Input.GetKeyDown(KeyCode.Space)== true)//
        {
            // Todo : (완)서버에 요청을 전달하는 함수를 작성하여 여기에 호출해야 함
            //요청 함수 호출
            StartCoroutine(RequestServer(url, port));
        }
        if (Input.GetKeyDown(KeyCode.T) == true)
        {
            StartCoroutine(RequestTexture(url, port));
        }
    }

    /// <summary>
    /// 서버에 요청하는 함수
    /// </summary>
    /// <param name="url">주소</param>
    /// <param name="port">포트번호</param>
    /// <returns></returns>
    IEnumerator RequestServer(string url, string port)
    {
        //코루틴을 이용, 
        //비동기(node 방식): 답변이 오기까지 무한 대기<-> 다른 일 하다가 답변이 오면 실행

        //서버에 요청하는 객체 생성
        UnityWebRequest request = UnityWebRequest.Get($"{url}:{port}"); //Get 엔터 //앞으로 해야하는 방식 Post: 실습서버는 Get가능함.
        yield return request.SendWebRequest();//서버로부터 응답이 오기 전까지 대기(대기중이면 , 함수 탈출)->응답오면 (진행)

        //다른 처리
        if (request.result == UnityWebRequest.Result.Success)//통신에 문제가 없는 경우
        {
            //일단 로그 찍기
            Debug.Log(request.downloadHandler.text);
            //제이슨 -> 역직렬화
            //버퍼 -> 바이너리

            //isbn 알아도 되는 코드 
            //id pw.. 알면 안되는 코드
        }
    }

    IEnumerator RequestTexture(string url, string port)
    {
        //이미지 요청하는 객체 생성
        UnityWebRequest request = UnityWebRequestTexture.GetTexture($"{url}:{port}/favicon.ico");
        //요청 후 대기
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)//요청이 성공했다면
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);//택스쳐 데이터 추출
            image.texture = texture;// 직접 받는것도 있고, 아니면 버퍼 그대로 사용한다. 인코딩 UTF-8로 줘야한다.
        }
        else
        {
            Debug.Log("택스쳐 오류");
        }
    }


}
