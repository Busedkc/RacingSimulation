using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TwoButtonModal : Modal
{
    [SerializeField] protected Button button1 = null;
    [SerializeField] protected Button button2 = null;
    private UnityAction callback1;
    private UnityAction callback2;

    protected override void OnEnable()
    {
        base.OnEnable();
        button1.onClick.AddListener(HandleButton1Clicked);
        button2.onClick.AddListener(HandleButton2Clicked);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        button1.onClick.RemoveListener(HandleButton1Clicked);
        button2.onClick.RemoveListener(HandleButton2Clicked);
    }

    protected virtual void HandleButton1Clicked()
    {
        Events.OkButtonClicked?.Invoke();
        callback1?.Invoke();
    }

    protected virtual void HandleButton2Clicked()
    {
        Events.CancelButtonClicked?.Invoke();
        callback2?.Invoke();
    }

    public void Init(UnityAction callback1, UnityAction callback2)
    {
        this.callback1 = callback1;
        this.callback2 = callback2;
    }




}
