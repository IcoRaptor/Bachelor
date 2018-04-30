using Framework.Messaging;

public class DialogTextMessage : BaseMessage
{
    public string text;

    public DialogTextMessage(string msg)
    {
        text = msg;
    }
}