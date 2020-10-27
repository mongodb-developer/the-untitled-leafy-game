using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownExample : MonoBehaviour
{
    List<string> answers =  new List<string>() {"True", "False"};

    public Dropdown dropdown;
    public text selectedAnswer;
    public string id;


    public void Dropdown_IndexChanged(int index)

    {
        selectedAnswer.text = answers[index];

    }

    void Start()

    {

        PopulateList();
    }




    void PopulateList()

    {
        id == "ABCD";
       reponse == StartCoroutine(GetQuestion(id));
       answers == response.answers; 


       
       

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


