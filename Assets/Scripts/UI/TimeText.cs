using Framework.Messaging;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    #region Variables

    private Type _msgType = typeof(TimeTextMessage);
    private Text _text;

    #endregion

    private void Awake()
    {
        _text = GetComponent<Text>();
        MessagingSystem.Instance.AttachListener(_msgType, TimeTextHandler);
    }

    private bool TimeTextHandler(BaseMessage msg)
    {
        TimeTextMessage castMsg = (TimeTextMessage)msg;
        _text.text = castMsg.text;

        return true;
    }

    private void OnDestroy()
    {
        if (MessagingSystem.IsAlive)
            MessagingSystem.Instance.DetachListener(_msgType, TimeTextHandler);
    }
}