using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ParticipantLog : MonoBehaviour
{
    public static string user;

    // Start is called before the first frame update
    void Start()
    {
        var input = gameObject.GetComponent<InputField>();
        var se = new InputField.SubmitEvent();
        se.AddListener(SubmitName);
        input.onEndEdit = se;
        
    }

    private void SubmitName(string arg0)
    {
        Debug.Log(arg0);
        user = arg0;
        Debug.Log("User ID is " + user);

        // Check to see if file exists, if it does, ask to either overwrite, append, or input new number
        string file_name_pos = user + "_position.csv";
        string file_name_obj = user + "_objdistance.csv";
        if (File.Exists(file_name_pos))
        {
            Debug.Log("File exists");
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
