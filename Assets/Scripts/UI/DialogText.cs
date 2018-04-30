using Framework.Messaging;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogText : MonoBehaviour
{
    #region Variables

    private Type _msgType = typeof(DialogTextMessage);
    private Text _text;

    #endregion

    private void Awake()
    {
        _text = GetComponent<Text>();
        MessagingSystem.Instance.AttachListener(_msgType, DialogTextHandler);
    }

    private bool DialogTextHandler(BaseMessage msg)
    {
        DialogTextMessage castMsg = (DialogTextMessage)msg;
        _text.text = castMsg.text;

        return true;
    }

    private void OnDestroy()
    {
        if (MessagingSystem.IsAlive)
            MessagingSystem.Instance.DetachListener(_msgType, DialogTextHandler);
    }
}