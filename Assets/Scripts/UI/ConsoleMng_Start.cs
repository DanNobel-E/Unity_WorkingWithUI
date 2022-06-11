using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsoleMng_Start : MonoBehaviour
{
    public RectTransform Content;
    public Image Background;
    public TMP_FontAsset ItalicFont;
    public TMP_FontAsset RegularFont;
    public TMP_FontAsset BoldFont;

    public TextMeshProUGUI TextConsole;
    public TMP_InputField InField;
    public const string Parameter1 = "fontsize";
    public const string Parameter2 = "background";
    public const string Parameter3 = "clear";
    public const string Parameter4 = "textcolor";


    Dictionary<string, Color> bgColor= new Dictionary<string, Color>();
    string correct = "<color=\"green\">";
    string wrong = "<color=\"red\">";
    string std = "<color=\"white\">";


    bool isFocused = false;
    float height;
    string textShown;

    private void Start()
    {
        InitbgColor();
        InField.Select();
        InField.ActivateInputField();
        height = Content.anchoredPosition.y;
    }

    private void InitbgColor()
    {
        bgColor["white"] = Color.white;
        bgColor["black"] = Color.black;
        bgColor["red"] = Color.red;
        bgColor["cyan"] = Color.cyan;
        bgColor["blue"] = Color.blue;
        bgColor["yellow"] = Color.yellow;
        bgColor["green"] = Color.green;
        bgColor["grey"] = Color.grey;
        bgColor["magenta"] = Color.magenta;



    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isFocused)
        {
            SubmitCmd(InField.text);

            if(Content.rect.height>=height-50)
            Content.anchoredPosition = new Vector2(Content.anchoredPosition.x, Content.rect.height+100);

            if (Content.rect.height > height * 3)
                TextConsole.text = "";

        }
        isFocused = InField.isFocused;
    }


    public void OnValueChanged(string text)
    {
        Debug.Log("OnValueChanged Event: " + text);
    }
    public void OnEndEdit(string text)
    {
        Debug.Log("OnEndEdit Event: " + text);
    }
    public void OnSelect(string text)
    {
        Debug.Log("OnSelect Event: " + text);
    }
    public void OnDeselect(string text)
    {
        Debug.Log("OnDeselect Event: " + text);
    }

    public void SubmitCmd(string newCmd)
    {


        textShown = ParseCmd(newCmd);

        TextConsole.font = RegularFont;
        TextConsole.text += $"\n{std}<b>" + newCmd + "</b>";

        string col;

        if (textShown != "OK.")
            col = wrong;
        else
            col = correct;

        TextConsole.font = ItalicFont;
        TextConsole.text += $"\n{col}<b>" + textShown + "</b>\n\n";

       

        InField.text = "";
        InField.Select();
        InField.ActivateInputField();
    }

    private string ParseCmd(string newCmd)
    {
        string result = "";

        string cmdLower = newCmd.ToLower();
        string[] msg= cmdLower.Split(' ');

        switch (msg[0])
        {
            case Parameter1:
                result= ParseFontSize(msg);
                break;
            case Parameter2:
                result = ParseBackgroundColor(msg);
                break;
            case Parameter3:
                result = ParseClear(msg);
                break;
            case Parameter4:
                result = ParseTextColor(msg);
                break;
            default:
                result = "Invalid parameter. Try again.";
                break;
        }


        return result;
    }

    private string ParseTextColor(string[] msg)
    {
        string result = "";

        if (msg.Length > 1)
        {

            int val;
            if (int.TryParse(msg[1], out val))
                result = "parameter must be a string.";
            else
            {
                if (msg.Length > 2)
                    result = "must set only one parameter.";
                else
                {
                    if (bgColor.ContainsKey(msg[1]))
                    {
                        result = "OK.";
                        std = $"<color=\"{msg[1]}\">";

                    }
                    else
                        result = "invalid color.";

                }

            }


        }
        else
            result = "must add a parameter.";




        return result;
    }

    private string ParseClear(string[] msg)
    {
        string result = "";

        if (msg.Length > 1)
            result= "no parameter needed.";
        else
        {
            result = "OK.";
            TextConsole.text = "";
            

        }




        return result;
    }

    private string ParseBackgroundColor(string[] msg)
    {
        string result = "";

        if (msg.Length > 1)
        {

            int val;
            if (int.TryParse(msg[1], out val))
                result = "parameter must be a string.";
            else
            {
                if (msg.Length > 2)
                    result = "must set only one parameter.";
                else
                {
                    if (bgColor.ContainsKey(msg[1]))
                    {
                        result = "OK.";
                        Background.color = bgColor[msg[1]];

                    }
                    else
                        result = "invalid color.";

                }

            }


        }
        else
            result = "must add a parameter.";




        return result;
    }

    private string ParseFontSize(string[] msg)
    {
        string result = "";

        if (msg.Length > 1)
        {

            int val;
            if (!int.TryParse(msg[1], out val))
                result = "parameter must be an integer.";
            else
            {
                if (msg.Length > 2)
                    result = "must set only one parameter.";
                else
                {
                    result = "OK.";
                    TextConsole.fontSize = val;
                }

            }


        }
        else
            result = "must add a parameter.";




        return result;


    }
}
