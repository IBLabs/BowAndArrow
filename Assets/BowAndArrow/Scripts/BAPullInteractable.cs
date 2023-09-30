using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BAPullInteractable : XRBaseInteractable
{
    public static event Action<float> PullDidRelease;

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;
    [SerializeField] private Transform notchTransform;

    [SerializeField] private GameEventScriptableObject arrowReleasedGameEvent;

    public float CurrentPullAmount { get; private set; }

    private IXRSelectInteractor pullingInteractor;
    private Vector3 initialNotchLocalPosition;

    // protected override void OnEnable()
    // {
    //     base.OnEnable();
    //     initialNotchLocalPosition = notchTransform.localPosition;
    // }

    // public void OnSelectEnter(SelectEnterEventArgs args)
    // {
    //     Debug.Log("[TEST]: selected entered");
    //     
    //     pullingInteractor = args.interactorObject;
    // }

    public void OnSelectExit(SelectExitEventArgs args)
    {
        PullDidRelease?.Invoke(1f);
        
        // PullDidRelease?.Invoke(CurrentPullAmount);
        // arrowReleasedGameEvent.Raise();
        //
        // pullingInteractor = null;
        // CurrentPullAmount = 0;
        //
        // UpdateBow();
    }

    // public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    // {
    //     base.ProcessInteractable(updatePhase);
    //     
    //     if (updatePhase != XRInteractionUpdateOrder.UpdatePhase.Dynamic) return;
    //     if (!isSelected) return;
    //
    //     CurrentPullAmount = CalculatePullAmount(pullingInteractor.transform.position);
    //     
    //     UpdateBow();
    // }

    private float CalculatePullAmount(Vector3 interactorPosition)
    {
        Vector3 pullDirection = interactorPosition - startTransform.position;
        Vector3 targetDirection = endTransform.position - startTransform.position;

        float pullAmount = Vector3.Dot(pullDirection, targetDirection.normalized) / targetDirection.magnitude;

        return Mathf.Clamp01(pullAmount);
    }

    private void UpdateBow()
    {
        UpdateString();
        UpdateNotch();
    }

    private void UpdateString()
    {
        Vector3 middlePointPosition = Vector3.forward * Mathf.Lerp(startTransform.localPosition.z, endTransform.localPosition.z, CurrentPullAmount);
        _lineRenderer.SetPosition(1, middlePointPosition);
    }

    private void UpdateNotch()
    {
        notchTransform.localPosition = initialNotchLocalPosition +
                                       Vector3.back * (startTransform.position - endTransform.position).magnitude *
                                       CurrentPullAmount;
    }
}
