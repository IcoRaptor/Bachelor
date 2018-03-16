using Framework.Messaging;

public class TimeTextMessage : BaseMessage
{
    public string text;

    public TimeTextMessage(string timeString)
    {
        text = timeString;
    }
}