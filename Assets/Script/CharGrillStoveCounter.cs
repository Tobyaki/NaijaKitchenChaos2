using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class CharGrillStoveCounter : BaseCounter
{
    [SerializeField] private GrillingRecipeSO[] grillingRecipeSOArray;


    private float grillingTimer;

    private void Update()
    {
        if (HasKitchenObject())
        { grillingTimer += Time.deltaTime;
            GrillingRecipeSO grillingRecipeSO = GetGrillingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            if (grillingTimer >= grillingRecipeSO.grillingTimerMax)
            {
                // Grilled
                grillingTimer = 0;
                Debug.Log("Grilled!");
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(grillingRecipeSO.output, this);
            }
            Debug.Log("GrillingTimer: " + grillingTimer);




        }
    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        { //There is no KitchenObject here
            if (player.HasKitchenObject())
            {//Player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {//Player is carrying something that can be grilled)
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                }
                else
                {//Player not carrying anything
                }
            }
            else
            {//There is a KitchenObject here
                if (!player.HasKitchenObject())
                {
                    //Player is carrying something
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
                else
                {//Player is not carrying anything
                   
                }
            }
        }
    }
        private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        GrillingRecipeSO grillingRecipeSO = GetGrillingRecipeSOWithInput(inputKitchenObjectSO);
        return grillingRecipeSO != null;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)

    {
        GrillingRecipeSO grillingRecipeSO = GetGrillingRecipeSOWithInput(inputKitchenObjectSO);
        if (grillingRecipeSO != null)
        {
            return grillingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private GrillingRecipeSO GetGrillingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (GrillingRecipeSO grillingRecipeSO in grillingRecipeSOArray)
        {
            if (grillingRecipeSO.input == inputKitchenObjectSO)
            {
                return grillingRecipeSO;
            }
        }
        return null;
    }
}


