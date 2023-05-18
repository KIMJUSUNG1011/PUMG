using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class FirstFloorClear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !ActionController.isFirstFloorClear)
        {
            Debug.Log(PlayerPrefs.GetInt("Idx") + "�� �÷��̾ 1���� Ż���߽��ϴ�!!");

            // Īȣ API ���
            StartCoroutine(PostRequest());

            ActionController.isFirstFloorClear = true;
        }
    }

    private IEnumerator PostRequest()
    {
        Debug.Log("1�� Ŭ���� Īȣ ���� ��û");

        string json = JsonUtility.ToJson(
            new RoomEscapeInfo
            {
                userIdx = PlayerPrefs.GetInt("Idx"),
                titleIdx = 7,
            }
        );

        using (UnityWebRequest webRequest = new UnityWebRequest("http://k8b108.p.ssafy.io:6999/api/v1/title/earn", "POST"))
        {
            // Content-Type ����� �����մϴ�.
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // �����͸� ���ε� �ڵ鷯�� �Ҵ��մϴ�.
            webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));

            // �ٿ�ε� �ڵ鷯�� �Ҵ��մϴ�. �̰��� �����κ����� ������ ó���մϴ�.
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            // ��û ������
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                // ���� ó��
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                // ���� ó��
                Debug.Log("Received: " + webRequest.downloadHandler.text);
            }
        }
    }
}

