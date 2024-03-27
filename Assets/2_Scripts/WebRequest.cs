using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebRequest : MonoBehaviour
{
    #region �ʿ��� ����

    /// <summary>
    /// url �ּ�
    /// </summary>
    public string url;

    
    /// <summary>
    /// ��Ʈ ��ȣ: ���ڸ� ������ ó���� ���ڿ��̱� ������ string���� ����
    /// </summary>
    public string port;

    public RawImage image;

    #endregion

    private void Update()
    {
        //Space Ű�� ������ ������ ��û�ϴ� �޼��� ����
        if (Input.GetKeyDown(KeyCode.Space)== true)//
        {
            // Todo : (��)������ ��û�� �����ϴ� �Լ��� �ۼ��Ͽ� ���⿡ ȣ���ؾ� ��
            //��û �Լ� ȣ��
            StartCoroutine(RequestServer(url, port));
        }
        if (Input.GetKeyDown(KeyCode.T) == true)
        {
            StartCoroutine(RequestTexture(url, port));
        }
    }

    /// <summary>
    /// ������ ��û�ϴ� �Լ�
    /// </summary>
    /// <param name="url">�ּ�</param>
    /// <param name="port">��Ʈ��ȣ</param>
    /// <returns></returns>
    IEnumerator RequestServer(string url, string port)
    {
        //�ڷ�ƾ�� �̿�, 
        //�񵿱�(node ���): �亯�� ������� ���� ���<-> �ٸ� �� �ϴٰ� �亯�� ���� ����

        //������ ��û�ϴ� ��ü ����
        UnityWebRequest request = UnityWebRequest.Get($"{url}:{port}"); //Get ���� //������ �ؾ��ϴ� ��� Post: �ǽ������� Get������.
        yield return request.SendWebRequest();//�����κ��� ������ ���� ������ ���(������̸� , �Լ� Ż��)->������� (����)

        //�ٸ� ó��
        if (request.result == UnityWebRequest.Result.Success)//��ſ� ������ ���� ���
        {
            //�ϴ� �α� ���
            Debug.Log(request.downloadHandler.text);
            //���̽� -> ������ȭ
            //���� -> ���̳ʸ�

            //isbn �˾Ƶ� �Ǵ� �ڵ� 
            //id pw.. �˸� �ȵǴ� �ڵ�
        }
    }

    IEnumerator RequestTexture(string url, string port)
    {
        //�̹��� ��û�ϴ� ��ü ����
        UnityWebRequest request = UnityWebRequestTexture.GetTexture($"{url}:{port}/favicon.ico");
        //��û �� ���
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)//��û�� �����ߴٸ�
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);//�ý��� ������ ����
            image.texture = texture;// ���� �޴°͵� �ְ�, �ƴϸ� ���� �״�� ����Ѵ�. ���ڵ� UTF-8�� ����Ѵ�.
        }
        else
        {
            Debug.Log("�ý��� ����");
        }
    }


}
