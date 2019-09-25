using UnityEngine;
using System.Collections;
using Soomla.Store;
using System;

public class IABItems : IStoreAssets
{
    public VirtualCategory[] GetCategories()
    {
        return new VirtualCategory[] { };
    }

    public VirtualCurrency[] GetCurrencies()
    {
        return new VirtualCurrency[] { COIN_CURRENCY };
    }

    public VirtualCurrencyPack[] GetCurrencyPacks()
    {
        return new VirtualCurrencyPack[] { ONEHUNDCOIN_PACK, FOURHUNDCOIN_PACK, SEVENHUNDCOIN_PACK, NINEHUNDCOIN_PACK };
    }

    public VirtualGood[] GetGoods()
    {
        return new VirtualGood[] { };
    }

    public int GetVersion()
    {
        return 0;
    }





    public const string COIN_CURRENCY_ITEM_ID = "currency_coin";

    public const string GIFTCOIN_PACK_PRODUCT_ID = "30pack";

    public const string ONEHUNDCOIN_PACK_PRODUCT_ID = "100pack";

    public const string FOURHUNDCOIN_PACK_PRODUCT_ID = "400pack";

    public const string SEVENHUNDCOIN_PACK_PRODUCT_ID = "700pack";

    public const string NINEHUNDCOIN_PACK_PRODUCT_ID = "900pack";





    public static VirtualCurrency COIN_CURRENCY = new VirtualCurrency(
        "Coin",                                      // name
        "",                                             // description
        COIN_CURRENCY_ITEM_ID                         // item id
    );


    public static VirtualCurrencyPack GIFTCOIN_PACK = new VirtualCurrencyPack(
        "30 Coin",                                   // name
        "",                                           // description
        "30pack",                                   // item id
        30,                                             // number of currencies in the pack
        COIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
        new PurchaseWithMarket(GIFTCOIN_PACK_PRODUCT_ID, 0)
    );

    public static VirtualCurrencyPack ONEHUNDCOIN_PACK = new VirtualCurrencyPack(
            "100 Coin",                                   // name
            "",                                           // description
            "100pack",                                   // item id
            100,                                             // number of currencies in the pack
            COIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
            new PurchaseWithMarket(ONEHUNDCOIN_PACK_PRODUCT_ID, 2500)
        );

    public static VirtualCurrencyPack FOURHUNDCOIN_PACK = new VirtualCurrencyPack(
            "400 Coin",                                   // name
            "",                                          // description
            "400pack",                                   // item id
            425,                                             // number of currencies in the pack
            COIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
            new PurchaseWithMarket(FOURHUNDCOIN_PACK_PRODUCT_ID, 7000)
    );

    public static VirtualCurrencyPack SEVENHUNDCOIN_PACK = new VirtualCurrencyPack(
            "700 Coin",                                  // name
            "",                                            // description
            "700pack",                                  // item id
            700,                                            // number of currencies in the pack
            COIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
            new PurchaseWithMarket(SEVENHUNDCOIN_PACK_PRODUCT_ID, 10000)
    );

    public static VirtualCurrencyPack NINEHUNDCOIN_PACK = new VirtualCurrencyPack(
            "900 Coin",                                 // name
            "",                                          // description
            "900pack",                                 // item id
            950,                                           // number of currencies in the pack
            COIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
            new PurchaseWithMarket(NINEHUNDCOIN_PACK_PRODUCT_ID, 12000)
    );

}
