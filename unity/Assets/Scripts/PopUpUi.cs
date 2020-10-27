using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PopUpUi : MonoBehaviour
{
public GameObject QuestionBox;


// Use this for initialization
void Start()
{
    QuestionBox.SetActive(false);

}


// Update is called once per frame
void Update()
{

}

void OnCollisionEnter2D(Collision2D collision)
{
QuestionBox.SetActive(true);

}

}
