using UnityEngine;
using System.Collections;
using Soomla.Store;
using Soomla;

public class IAB_MainScript : MonoBehaviour {
    public LevelsManager LM;
	// Use this for initialization
	void Start () {
        StoreEvents.OnUnexpectedErrorInStore += onUnexpectedErrorInStore;


        SoomlaStore.Initialize(new IABItems());

        if (GameDataManager.ManangerInstance.IsFirstPlay)
            GiveItem();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void onUnexpectedErrorInStore(string obj)
    {
        SoomlaUtils.LogError("IABEventHandler", "Error is" + obj);
    }

    public void Buy100Item()
    {
        StoreInventory.BuyItem("100pack");
        UpdateBalance();
    }
    public void Buy400Item()
    {
        StoreInventory.BuyItem("400pack");
        UpdateBalance();
    }
    public void Buy700Item()
    {
        StoreInventory.BuyItem("700pack");
        UpdateBalance();
    }
    public void Buy900Item()
    {
        StoreInventory.BuyItem("900pack");
        UpdateBalance();
    }


    public void GiveItem()
    {
        StoreInventory.GiveItem("30pack", 1);
    }

    public void UpdateBalance()
    {
        GameDataManager.ManangerInstance.Credit = StoreInfo.Currencies[0].GetBalance();
        GameDataManager.ManangerInstance.SetCredit();
        LM.UpdateCreditVisual();
    }
}
