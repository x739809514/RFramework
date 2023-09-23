using System;

public class UIType
{
    public string path { get; private set; }
    public string name { get; private set; }

    public UIType(string path)
    {
        this.path = "UI/"+path;
        name = path.Substring(path.LastIndexOf("/", StringComparison.Ordinal) + 1);
    }

    public override string ToString()
    {
        return $"path:{path}, name:{name}";
    }


#region Panels

    public static readonly UIType Panel_Hud = new UIType("Panel_Hud");

#endregion
}