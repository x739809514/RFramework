public class BaseContent
{
    public UIType uiType { get; private set; }

    public BaseContent(UIType uiType)
    {
        this.uiType = uiType;
    }
}