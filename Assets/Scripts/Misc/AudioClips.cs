using UnityEngine;

public static class AudioClips
{
    public static AudioClip selCursor, selDecision, selCancel;
    public static AudioClip menuOpen, menuClose;
    public static AudioClip bump;
    public static AudioClip summaryPage;
    public static AudioClip doorEnter, doorExit;
    public static AudioClip exclaim;
    public static AudioClip partySwitch;
    public static AudioClip wildBattleBGM;
    public static AudioClip getItem;

    static AudioClips()
    {
        selCursor = Resources.Load<AudioClip>("Audio/selCursor");
        selDecision = Resources.Load<AudioClip>("Audio/selDecision");
        selCancel = Resources.Load<AudioClip>("Audio/selCancel");
        menuOpen = Resources.Load<AudioClip>("Audio/menuOpen");
        menuClose = Resources.Load<AudioClip>("Audio/menuClose");
        bump = Resources.Load<AudioClip>("Audio/bump");
        summaryPage = Resources.Load<AudioClip>("Audio/summaryPage");
        doorEnter = Resources.Load<AudioClip>("Audio/doorEnter");
        doorExit = Resources.Load<AudioClip>("Audio/doorExit");
        exclaim = Resources.Load<AudioClip>("Audio/exclaim");
        partySwitch = Resources.Load<AudioClip>("Audio/partySwitch");
        wildBattleBGM = Resources.Load<AudioClip>("Audio/wildBattleBGM");
        getItem = Resources.Load<AudioClip>("Audio/itemGet");
    }
}
