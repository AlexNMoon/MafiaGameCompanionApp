using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PanelController : MonoBehaviour
{
    public event Action ContinueButtonClick;
    public event Action BackButtonClick;

    [SerializeField] private GameObject panelGameObject;

    public virtual void OpenPanel()
    {
        panelGameObject.SetActive(true);
    }

    public virtual void ClosePanel()
    {
        panelGameObject.SetActive(false);
    }

    protected void InvokeContinueButtonClick()
    {
        ContinueButtonClick?.Invoke();
    }

    protected void InvokeBackButtonClick()
    {
        BackButtonClick?.Invoke();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }
    
    protected virtual void SubscribeEvents(){}

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    
    protected virtual void UnsubscribeEvents(){}
}
