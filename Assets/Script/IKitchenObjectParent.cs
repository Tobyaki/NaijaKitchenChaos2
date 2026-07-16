using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    Transform GetKitchenObjectFollowTransform();
    void SetkitchenObject(KitchenObject kitchenObject);

    KitchenObject GetKitchenObject();

    void ClearKitchenObject();

    bool HasKitchenObject();
}
