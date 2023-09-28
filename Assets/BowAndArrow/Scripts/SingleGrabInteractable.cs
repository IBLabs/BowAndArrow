using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SingleGrabInteractable : XRGrabInteractable
{
    private IXRInteractor currentInteractor;
        
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        currentInteractor = args.interactorObject;
    }

    protected override void OnSelectExiting(XRBaseInteractor interactor)
    {
        currentInteractor = null;
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        if (currentInteractor != null)
        {
            return interactor == currentInteractor;
        }

        return true;
    }
}