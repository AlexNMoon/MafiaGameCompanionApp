using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PanelController : MonoBehaviour
{
    public event Action OpenNextPanel;
    public event Action OpenPreviousPanel;

    [SerializeField] private GameObject panelGameObject;

    public virtual void OpenPanel()
    {
        panelGameObject.SetActive(true);
    }

    public virtual void ClosePanel()
    {
        panelGameObject.SetActive(false);
    }

    protected void InvokeOpenNextPanel()
    {
        OpenNextPanel?.Invoke();
    }

    protected void InvokeOpenPreviousPanel()
    {
        OpenPreviousPanel?.Invoke();
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
