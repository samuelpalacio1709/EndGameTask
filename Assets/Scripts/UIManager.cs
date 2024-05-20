using TMPro;
using UnityEngine;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] TMP_Text toastMessage;


    public void ShowToastMessage(string message)
    {
        if (toastMessage != null)
        {
            toastMessage.text = message;
        }
    }
    public void HideToastMessage()
    {
        if (toastMessage != null)
        {
            toastMessage.text = "";
        }

    }

}
