using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class Question : MonoBehaviour {

    private DatabaseModel question;

    public string questionId;
    public Score score;

    void Start() {
        //score = GetComponent<Score>();
        // DatabaseModel db = new DatabaseModel();
        // db.problem_id = "ABCD";
        // db.answer = true;
        // StartCoroutine(CheckAnswer(db.Stringify(), result => {
        //     Debug.Log("RESULT: " + result);
        // }));
    }

    void Update() { }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.name == "Player") {
            StartCoroutine(GetQuestion(questionId, result => {
                question = result;
                Debug.Log(question.Stringify());
            }));
            score.AddPoints(1);
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

    IEnumerator CheckAnswer(string data, System.Action<bool> callback = null) {
        using (UnityWebRequest request = new UnityWebRequest("http://localhost:3000/answer", "POST")) {
            request.SetRequestHeader("Content-Type", "application/json");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(data);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError) {
                Debug.Log(request.error);
                if(callback != null) {
                    callback.Invoke(false);
                }
            } else {
                Debug.Log(request.downloadHandler.text);
                // if(request.downloadHandler.text == "{}") {
                //     Debug.Log("RESPONSE WAS NULL");
                // }
                if(callback != null) {
                    callback.Invoke(request.downloadHandler.text != "{}");
                }
            }
        }
    }
}
