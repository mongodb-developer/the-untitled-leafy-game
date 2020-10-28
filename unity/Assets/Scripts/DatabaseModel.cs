using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseModel {

    public string _id;
    public string question_text;
    public string problem_id;
    public string subject_area;
    public bool answer;

    public string Stringify() {
        return JsonUtility.ToJson(this);
    }

    public static DatabaseModel Parse(string json) {
        return JsonUtility.FromJson<DatabaseModel>(json);
    }

}