using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IHasProgress;

public class CharGrillStoveCounter : BaseCounter
{
    public event EventHandler<OnStateChangedEventsArgs> OnStateChanged;
    public class OnStateChangedEventsArgs : EventArgs
    {
               public State state;
    }
    public enum State
    {   
        Idle,
        Grilling,
        Grilled,
        Burned,
    }



    [SerializeField] private GrillingRecipeSO[] grillingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;


    private State state;
    private float grillingTimer;
    private GrillingRecipeSO grillingRecipeSO;
    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Grilling:
                    grillingTimer += Time.deltaTime;

                    if (grillingTimer >= grillingRecipeSO.grillingTimerMax)
                    {
                        // Grilled
                       
                        
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(grillingRecipeSO.output, this);
                        
                        Debug.Log("Object fried!");

                        
                        state = State.Grilled;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventsArgs { state = state });
                    }
                    break;
                case State.Grilled:
                    burningTimer += Time.deltaTime;

                    if (burningTimer >= burningRecipeSO.burningTimerMax)
                    {
                        // Burned


                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        Debug.Log("Object burned!");
                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventsArgs { state = state });
                    }
                        break;

                case State.Burned:
                    break;
            }
            Debug.Log(state);
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

                    grillingRecipeSO = GetGrillingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Grilling;
                    grillingTimer = 0f;
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
                   
                    state = State.Idle;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventsArgs { state = state });
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

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
}


