using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.UI;

public class Question : MonoBehaviour {

    private DatabaseModel question;

    public string questionId;
    public GameObject questionModal;
    public Score score;

    private Text questionText;
    private Dropdown dropdownAnswer;
    private Button submitButton;

    void Start() {
        GameObject questionTextGameObject = questionModal.transform.Find("QuestionText").gameObject;
        questionText = questionTextGameObject.GetComponent<Text>();
        GameObject submitButtonGameObject = questionModal.transform.Find("SubmitButton").gameObject;
        submitButton = submitButtonGameObject.GetComponent<Button>();
        GameObject dropdownAnswerGameObject = questionModal.transform.Find("Dropdown").gameObject;
        dropdownAnswer = dropdownAnswerGameObject.GetComponent<Dropdown>();
    }

    void Update() { }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.name == "Player") {
            questionModal.SetActive(true);
            Time.timeScale = 0;
            StartCoroutine(GetQuestion(questionId, result => {
                questionText.text = result.question_text;
                submitButton.onClick.AddListener(() =>{SubmitOnClick(result, dropdownAnswer);});
            }));
        }
    }

    void SubmitOnClick(DatabaseModel db, Dropdown dropdownAnswer) {
        db.answer = dropdownAnswer.value == 0;
        StartCoroutine(CheckAnswer(db.Stringify(), result => {
            if(result == true) {
                score.AddPoints(1);
            }
            questionModal.SetActive(false);
            Time.timeScale = 1;
            submitButton.onClick.RemoveAllListeners();
        }));
    }

    IEnumerator GetQuestion(string id, System.Action<DatabaseModel> callback = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Get("https://webhooks.mongodb-realm.com/api/client/v2.0/app/skunkworks-rptwf/service/webhooks/incoming_webhook/get_question?problem_id=" + id))
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
        using (UnityWebRequest request = new UnityWebRequest("https://webhooks.mongodb-realm.com/api/client/v2.0/app/skunkworks-rptwf/service/webhooks/incoming_webhook/checkanswer", "POST")) {
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
                if(callback != null) {
                    callback.Invoke(request.downloadHandler.text != "{}");
                }
            }
        }
    }
}
