using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class Question : MonoBehaviour {

    private DatabaseModel question;
    public string questionId;

    void Start() { }

    void Update() { }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.name == "Player") {
            StartCoroutine(GetQuestion(questionId, result => {
                question = result;
                Debug.Log(question.Stringify());
            }));
        }
    }

    IEnumerator GetQuestion(string id, System.Action<DatabaseModel> callback = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000/question/" + id))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError) {
                Debug.Log(request.error);
                if(callback != null) {
                    callback.Invoke(null);
                }
            }
            else {
                if(callback != null) {
                    callback.Invoke(DatabaseModel.Parse(request.downloadHandler.text));
                }
            }
        }
    }

    // NOT USED YET
    // IEnumerator CheckAnswer(string data)
    // {
    //     using (UnityWebRequest request = new UnityWebRequest("http://localhost:3000/answer", "POST"))
    //     {
    //         request.SetRequestHeader("Content-Type", "application/json");
    //         byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(data);
    //         request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
    //         request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
    //         yield return request.SendWebRequest();
    //         if (request.isNetworkError || request.isHttpError)
    //         {
    //             Debug.Log(request.error);
    //         }
    //         else
    //         {
    //             Debug.Log(request.downloadHandler.text);
    //         }
    //     }
    // }
}
