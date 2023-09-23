using UnityEngine;

public class Panel_Hud_Content : BaseContent
{
    public Panel_Hud_Content() : base(UIType.Panel_Hud)
    {
    }
}

public class Panel_Hud : PanelBase
{
#region Override

    public override void OnEnter()
    {
        
    }

    public override void OnOpen(Object obj)
    {
        
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnClose()
    {
        base.OnClose();
    }

#endregion
}