using Framework.Messaging;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour
{
    #region Variables

    private Text _text;

    #endregion

    private void Start()
    {
        _text = GetComponent<Text>();

        MessagingSystem.Instance.AttachListener(typeof(TimeTextMessage), TimeTextHandler);
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
            MessagingSystem.Instance.DetachListener(typeof(TimeTextMessage), TimeTextHandler);
    }
}