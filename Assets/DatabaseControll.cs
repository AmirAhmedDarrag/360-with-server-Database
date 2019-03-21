using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseControll : MonoBehaviour
{
    public Text _TXT;
    public string[] info;
    private string show;

    private bool SearchAll;
    public static string colorName;
    public static string colorType;

    void Update()
    {
        if (SearchAll)
        {
            StartCoroutine(SearchInDatabase());
            SearchAll = false;
        }
    }

    IEnumerator SearchInDatabase()
    {
        show = "";

        WWW infoData = new WWW("http://localhost/unity_test/InfoData.php");
        yield return infoData;

        string infoDataString = infoData.text;

        if (infoDataString.Length != 0)
        {
            Debug.Log(infoDataString);

            info = infoDataString.Split(';');

            for(int i = 0; i < info.Length-1; i++)
            {
                show += GetDataValue(info[i], "Name:");
                show += "         ";
                show += GetDataValue(info[i], "Type:");
                show += "\n";
            }

            _TXT.text = show;
        }
        else
        {
            _TXT.text = "There is no Data";
        }
    }

    string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index)+index.Length);
        if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        return value;
    }

    public void InsertData()
    {
        WWWForm form = new WWWForm();
        form.AddField("colorNamePost",colorName);
        form.AddField("colorTypePost", colorType);

        WWW www = new WWW("http://localhost/unity_test/InsertColor.php",form);
    }

    public void ShowAllData()
    {
        SearchAll = true;
    }

    public void NameClick(int x)
    {
        switch (x)
        {
            case 0:
                colorName = "Red";
                break;

            case 1:
                colorName = "Green";
                break;

            case 2:
                colorName = "Blue";
                break;

        }
    }

    public void TypeClick(int x)
    {
        switch (x)
        {
            case 0:
                colorType = "Light";
                break;

            case 1:
                colorType = "Dark";
                break;


        }
    }
}
