using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OneButtonModal : Modal
{
    [SerializeField] protected Button button = null;
    private UnityAction callback;

    protected override void OnEnable()
    {
        base.OnEnable();
        button.onClick.AddListener(HandleButtonClicked);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        button.onClick.RemoveListener(HandleButtonClicked);
    }

    protected virtual void HandleButtonClicked()
    {
        Events.OkButtonClicked?.Invoke();
        callback?.Invoke();
    }

    public void Init(UnityAction callback)
    {
        this.callback = callback;
    }
}
