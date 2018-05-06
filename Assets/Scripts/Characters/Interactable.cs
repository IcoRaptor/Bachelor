using AI;
using Framework.Debugging;
using Framework.Messaging;
using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    #region Variables

    private Blackboard _blackboard;

    #endregion

    private void Start()
    {
        _blackboard = GetComponent<Blackboard>();

        if (!_blackboard)
        {
            Debugger.LogFormat(LOG_TYPE.ERROR,
               "{0}: {1} missing!\n",
                gameObject.name, typeof(Blackboard).Name);
        }
    }

    public void Interact()
    {
        if (_blackboard.Dialog.Length == 0)
            return;

        var msg = new DialogTextMessage(_blackboard.Dialog);
        MessagingSystem.Instance.QueueMessage(msg);

        _blackboard.InteractionInterrupt = true;

        StartCoroutine(WaitForRemove());
    }

    private IEnumerator WaitForRemove()
    {
        yield return new WaitForSecondsRealtime(2f);

        var dialogMessage = new DialogTextMessage(string.Empty);
        MessagingSystem.Instance.QueueMessage(dialogMessage);

        _blackboard.InteractionInterrupt = false;
    }
}