using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuestDisplay
{
    public void Init(string _text);
    public void UpdateText(string _text);
    public void End();
}
