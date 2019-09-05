using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SecondaryTaskMsgData : BaseEventData
{
    public Texture texture;

    public SecondaryTaskMsgData() : base(null)
    { }
}

public interface ISecondaryTaskMsgHandler : IEventSystemHandler
{
    void DisplayArrow(SecondaryTaskMsgData msgData);
    void HideArrow();
}