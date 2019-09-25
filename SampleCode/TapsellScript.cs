using UnityEngine;
using System.Collections;
using TapsellSDK;
using TapsellSimpleJSON;

public class TapsellScript : MonoBehaviour {

    public static bool available = false;
    public static bool bannerIsHidden = true;
    public static TapsellAd ad = null;
    public static TapsellNativeBannerAd nativeAd = null;
    public static string bannerZoneId = "5a60d65bf697ee0001f4fe8a";
    public static string VideoZoneId = "5a60d65bf697ee0001f4fe8a";
    public UILabel l;
    public LevelsManager LM;
    public GameDataManager DataManager;



    // Use this for initialization
    void Start() {
        Tapsell.initialize("coqfiqiiofoogchikgaqictgmtmfidetnspamjosfkinhnogksdsthfsfhonrldopqkeqr");

        l.text ="Tapsell Version: " + Tapsell.getVersion() + "\n";
        Tapsell.setDebugMode(true);
        Tapsell.setPermissionHandlerConfig(Tapsell.PERMISSION_HANDLER_AUTO);
        Tapsell.setRewardListener(
            (TapsellAdFinishedResult result) =>
            {
                // onFinished, you may give rewards to user if result.completed and result.rewarded are both True
                l.text += "onFinished, adId:" + result.adId + ", zoneId:" + result.zoneId + ", completed:" + result.completed + ", rewarded:" + result.rewarded + "\n";
                if (result.completed && result.rewarded)
                {
                    DataManager.Credit += 5;
                    DataManager.Save();
                    LM.UpdateCreditVisual();
                }
            }
        );

        



    }

    IEnumerator WaitForRequest(WWW data)
    {
        Debug.Log("my start waiting...");
        yield return data; // Wait until the download is done
        if (data.error != null)
        {
            Debug.Log("my server error is " + data.error);
        }
        else
        {
            Debug.Log("my server result is " + data.text);

            JSONNode node = JSON.Parse(data.text);
            bool valid = node["valid"].AsBool;
            if (valid)
            {
                // if suggestion is valid, you can give in game gifts to the user
                Debug.Log("Ad is valid");
            }
            else
            {
                Debug.Log("Ad is not valid");
            }
        }
    }


    private void requestAd(string zone, bool cached)
    {
        l.text += "dadayeh" + "\n";
        Tapsell.requestAd(zone, cached,
            (TapsellAd result) => {
                // onAdAvailable
                l.text += "Action: onAdAvailable" + "\n";
                
                TapsellScript.available = true;
                TapsellScript.ad = result;

            },

            (string zoneId) => {
            // onNoAdAvailable
            l.text += "No Ad Available"+"\n";
            },

            (TapsellError error) => {
                // onError
                l.text += error.error + "\n";
            },

            (string zoneId) => {
                // onNoNetwork
                l.text += "No Network: " + zoneId + "\n";
            },

            (TapsellAd result) => {
                //onExpiring
                l.text += "Expiring" + "\n";
                TapsellScript.available = false;
                TapsellScript.ad = null;
                requestAd(result.zoneId, false);
            }

        );
    }

    void Update()
    {
        if (available)
        {
            available = false;
            TapsellShowOptions options = new TapsellShowOptions();
            options.backDisabled = false;
            options.immersiveMode = false;
            options.rotationMode = TapsellShowOptions.ROTATION_LOCKED_LANDSCAPE;
            options.showDialog = true;
            Tapsell.showAd(ad, options);
        }

    }

    public void WatchVideo()
    {
        requestAd(VideoZoneId, false);

    }
}
