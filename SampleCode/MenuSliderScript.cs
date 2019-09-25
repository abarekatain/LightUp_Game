using UnityEngine;
using System.Collections;

public class MenuSliderScript : MonoBehaviour {

    [System.Serializable]
    public struct PageData
    {
        public UISprite PageColor;
        public UIWidget PageWidget;
        public UISprite PageLock;
        public int RequiredStars;
        public UIPanel NotificationPanel;
        public float PagePos;
        public bool isLocked;
    }

    public GameObject PGuide1, PGuide2;

    public PageData[] Pages;

    public UIGrid SliderGrid;
    public Transform ScrollViewTransform;

    public float FirstScrollPos;
    public float GridCellPaddingX;

    public int CurrentPage;
    public int Currentindex;
    public float Factor;

    public LevelsManager levelsManager;
    public GameDataManager DataManager;
     int EachSeasonLevel;

    



    public void PlaySound()
    {
        if(Factor >= 0.015f && Factor<=0.985)
        {
            var musicController = MusicController.ControllerInstance;
			musicController.PlayTempMusic((int)(Currentindex/2) + 1);
        }
    }

    void FirstSlideSet()
    {
        if (Currentindex == 1 || (Currentindex == 0 && Factor >= 0.85f))
        {
            if (DataManager.IsFirstSlide == true )
            {
                PGuide2.GetComponent<TweenAlpha>().PlayForward();
                DataManager.IsFirstSlide = false;
                DataManager.Save();
            }
        }


    }


    void Start () {

        DataManager = GameDataManager.ManangerInstance;

        var scrollView = ScrollViewTransform.GetComponent<UIScrollView>();
        UIScrollView.OnDragNotification x = new UIScrollView.OnDragNotification(PlaySound);
        scrollView.onMomentumMove = x;

        UIScrollView.OnDragNotification y = new UIScrollView.OnDragNotification(SeasonLockManager);
        scrollView.onStoppedMoving = y;

        UIScrollView.OnDragNotification z = new UIScrollView.OnDragNotification(FirstSlideSet);
        scrollView.onStoppedMoving = z;

        if (DataManager.IsFirstGuide)
        {
            PGuide1.GetComponent<TweenAlpha>().PlayForward();
            DataManager.IsFirstGuide = false;
            DataManager.Save();
        }
        else
        {
            PGuide1.SetActive(false);
        }


        FirstScrollPos = ScrollViewTransform.localPosition.x;
        GridCellPaddingX = SliderGrid.cellWidth;
        for (int i = 0; i < Pages.Length; i++)
        {
            Pages[i].PagePos = FirstScrollPos - i * GridCellPaddingX;
        }

        
        Invoke("GotoCurrentLevelPage", .3f);


    }

    public void GotoCurrentLevelPage()
    {
        SeasonLockManager();

        //SeasonLockManager();
        EachSeasonLevel = levelsManager.LevelContainers[0].transform.childCount;
        GamePreferences.EachSeasonLevel = EachSeasonLevel;
        int CurrentLevel = DataManager.CurrentLevel;
        for (int i = 0; i < Pages.Length; i++)
        {
            if (CurrentLevel >= EachSeasonLevel*i && CurrentLevel<EachSeasonLevel*(i+1))
            {
                if (!Pages[i].isLocked) CurrentPage = i; else CurrentPage = i - 1;
                Vector3 NewPos = new Vector3(Pages[CurrentPage].PagePos, ScrollViewTransform.localPosition.y, ScrollViewTransform.localPosition.z);
                iTween.MoveTo(ScrollViewTransform.gameObject, iTween.Hash("position", NewPos, "islocal", true, "time", 1));
                break;
            }
        }

        //SeasonLocker();




        GamePreferences.IsProcessing = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetSliderState();

        var temp = Pages[Currentindex].PageColor.color;
        temp.a = 1 - Factor;
        Pages[Currentindex].PageColor.color = temp;

        Pages[Currentindex].PageWidget.alpha = 1 - Factor;


        var Temp = Pages[Currentindex+1].PageColor.color;
        Temp.a =Factor;
        Pages[Currentindex+1].PageColor.color = Temp;
        Pages[Currentindex+1].PageWidget.alpha = Factor;

    }


    public void GetSliderState()
    {
        var p = ScrollViewTransform.localPosition.x;
        for (int i = 0; i < Pages.Length -1; i++)
        {
            if (p>= Pages[i+1].PagePos && p <= Pages[i].PagePos)
            {
                Currentindex = i;
                Factor = (Pages[i].PagePos - p) / (Pages[i].PagePos - Pages[i + 1].PagePos);
            }

                
        }
    }

    public void SeasonLocker()
    {
        for (int i = 1; i < Pages.Length; i++)
        {
            if (i <= CurrentPage)
            {
                Pages[i - 1].PageLock.gameObject.SetActive(false);
            }
            else
            {
                Pages[i].PageWidget.gameObject.SetActive(false);
            }
        }
        if (CurrentPage % 2 == 0)
        {
            Pages[CurrentPage + 1].PageWidget.gameObject.SetActive(true);
        }
    }

    public void SeasonLockManager()
    {


        for (int i = 2; i < Pages.Length; i = i + 2)
        {
            if (DataManager.Coins >=GamePreferences.SeasonsStarLimit[i / 2])
            {
                Pages[i - 1].PageLock.gameObject.SetActive(false);
                Pages[i].PageWidget.gameObject.SetActive(true);
                Pages[i + 1].PageWidget.gameObject.SetActive(true);
                Pages[i].isLocked = false;
                Pages[i + 1].isLocked = false;
            }
            else
            {
                Pages[i - 1].PageLock.gameObject.SetActive(true);
                Pages[i].PageWidget.gameObject.SetActive(false);
                Pages[i + 1].PageWidget.gameObject.SetActive(false);
                Pages[i].isLocked = true;
                Pages[i + 1].isLocked = true;
            }
        }

    }

}
