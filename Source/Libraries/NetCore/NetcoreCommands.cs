namespace RTCV.NetCore
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Design", "CA1707", Justification = "NetCore command names may have underscores for clarity and compatiblity with existing implementations.")]
    public static class NetcoreCommands
    {
        public const string CORRUPTCORE = nameof(CORRUPTCORE);
        public const string VANGUARD = nameof(VANGUARD);
        public const string DEFAULT = nameof(DEFAULT);
        public const string UI = nameof(UI);
        public const string KILLSWITCH_PULSE = nameof(KILLSWITCH_PULSE);
        public const string RESET_GAME_PROTECTION_IF_RUNNING = nameof(RESET_GAME_PROTECTION_IF_RUNNING);

        public const string REMOTE_PUSHVANGUARDSPEC = nameof(REMOTE_PUSHVANGUARDSPEC);
        public const string REMOTE_PUSHVANGUARDSPECUPDATE = nameof(REMOTE_PUSHVANGUARDSPECUPDATE);
        public const string REMOTE_PUSHCORRUPTCORESPEC = nameof(REMOTE_PUSHCORRUPTCORESPEC);
        public const string REMOTE_PUSHCORRUPTCORESPECUPDATE = nameof(REMOTE_PUSHCORRUPTCORESPECUPDATE);
        public const string REMOTE_PUSHUISPEC = nameof(REMOTE_PUSHUISPEC);
        public const string REMOTE_PUSHUISPECUPDATE = nameof(REMOTE_PUSHUISPECUPDATE);
        public const string REMOTE_ALLSPECSSENT = nameof(REMOTE_ALLSPECSSENT);

        public const string REMOTE_RENDER_STOP = nameof(REMOTE_RENDER_STOP);
        public const string REMOTE_RENDER_START = nameof(REMOTE_RENDER_START);
        public const string REMOTE_RENDER_DISPLAY = nameof(REMOTE_RENDER_DISPLAY);

        public const string REMOTE_EVENT_DOMAINSUPDATED = nameof(REMOTE_EVENT_DOMAINSUPDATED);
        public const string REMOTE_EVENT_RESTRICTFEATURES = nameof(REMOTE_EVENT_RESTRICTFEATURES);
        public const string REMOTE_GETBLASTGENERATOR_LAYER = nameof(REMOTE_GETBLASTGENERATOR_LAYER);
        public const string MANUALBLAST = nameof(MANUALBLAST);
        public const string APPLYBLASTLAYER = nameof(APPLYBLASTLAYER);
        public const string APPLYCACHEDBLASTLAYER = nameof(APPLYCACHEDBLASTLAYER);
        public const string BLAST = nameof(BLAST);
        public const string STASHKEY = nameof(STASHKEY);
        public const string REMOTE_PUSHRTCSPEC = nameof(REMOTE_PUSHRTCSPEC);
        public const string REMOTE_PUSHRTCSPECUPDATE = nameof(REMOTE_PUSHRTCSPECUPDATE);
        public const string REMOTE_PUSHVMDPROTOS = nameof(REMOTE_PUSHVMDPROTOS);
        public const string REMOTE_GENERATEVMDTEXT = nameof(REMOTE_GENERATEVMDTEXT);
        public const string BLASTGENERATOR_BLAST = nameof(BLASTGENERATOR_BLAST);
        public const string REMOTE_LOADPLUGINS = nameof(REMOTE_LOADPLUGINS);
        public const string REMOTE_LOADSTATE = nameof(REMOTE_LOADSTATE);
        public const string REMOTE_SAVESTATE = nameof(REMOTE_SAVESTATE);
        public const string REMOTE_SAVESTATELESS = nameof(REMOTE_SAVESTATELESS);
        public const string REMOTE_RESUMEEMULATION = nameof(REMOTE_RESUMEEMULATION);
        public const string REMOTE_DISABLESAVESTATESUPPORT = nameof(REMOTE_DISABLESAVESTATESUPPORT);
        public const string REMOTE_DISABLEREALTIMESUPPORT = nameof(REMOTE_DISABLEREALTIMESUPPORT);
        public const string REMOTE_DISABLEKILLSWITCHSUPPORT = nameof(REMOTE_DISABLEKILLSWITCHSUPPORT);
        public const string REMOTE_DISABLEGAMEPROTECTIONSUPPORT = nameof(REMOTE_DISABLEGAMEPROTECTIONSUPPORT);
        public const string REMOTE_BLASTEDITOR_STARTSANITIZETOOL = nameof(REMOTE_BLASTEDITOR_STARTSANITIZETOOL);
        public const string REMOTE_BLASTEDITOR_LOADCORRUPT = nameof(REMOTE_BLASTEDITOR_LOADCORRUPT);
        public const string REMOTE_BLASTEDITOR_LOADORIGINAL = nameof(REMOTE_BLASTEDITOR_LOADORIGINAL);
        public const string REMOTE_BLASTEDITOR_GETLAYERSIZE_UNLOCKEDUNITS = nameof(REMOTE_BLASTEDITOR_GETLAYERSIZE_UNLOCKEDUNITS);
        public const string REMOTE_BLASTEDITOR_GETLAYERSIZE = nameof(REMOTE_BLASTEDITOR_GETLAYERSIZE);
        public const string REMOTE_SANITIZETOOL_STARTSANITIZING = nameof(REMOTE_SANITIZETOOL_STARTSANITIZING);
        public const string REMOTE_SANITIZETOOL_LEAVEWITHCHANGES = nameof(REMOTE_SANITIZETOOL_LEAVEWITHCHANGES);
        public const string REMOTE_SANITIZETOOL_LEAVESUBTRACTCHANGES = nameof(REMOTE_SANITIZETOOL_LEAVESUBTRACTCHANGES);
        public const string REMOTE_SANITIZETOOL_YESEFFECT = nameof(REMOTE_SANITIZETOOL_YESEFFECT);
        public const string REMOTE_SANITIZETOOL_NOEFFECT = nameof(REMOTE_SANITIZETOOL_NOEFFECT);
        public const string REMOTE_SANITIZETOOL_REROLL = nameof(REMOTE_SANITIZETOOL_REROLL);



        public const string REMOTE_BACKUPKEY_REQUEST = nameof(REMOTE_BACKUPKEY_REQUEST);
        public const string REMOTE_BACKUPKEY_STASH = nameof(REMOTE_BACKUPKEY_STASH);
        public const string REMOTE_ISNORMALADVANCE = nameof(REMOTE_ISNORMALADVANCE);

        public const string REMOTE_DOMAIN_PEEKBYTE = nameof(REMOTE_DOMAIN_PEEKBYTE);
        public const string REMOTE_DOMAIN_POKEBYTE = nameof(REMOTE_DOMAIN_POKEBYTE);
        public const string REMOTE_DOMAIN_REFRESHDOMAINS = nameof(REMOTE_DOMAIN_REFRESHDOMAINS);
        public const string REMOTE_DOMAIN_GETDOMAINS = nameof(REMOTE_DOMAIN_GETDOMAINS);
        public const string REMOTE_DOMAIN_VMD_ADD = nameof(REMOTE_DOMAIN_VMD_ADD);
        public const string REMOTE_DOMAIN_VMD_REMOVE = nameof(REMOTE_DOMAIN_VMD_REMOVE);
        public const string REMOTE_DOMAIN_ACTIVETABLE_MAKEDUMP = nameof(REMOTE_DOMAIN_ACTIVETABLE_MAKEDUMP);
        public const string REMOTE_KEY_PUSHSAVESTATEDICO = nameof(REMOTE_KEY_PUSHSAVESTATEDICO);
        public const string REMOTE_KEY_GETRAWBLASTLAYER = nameof(REMOTE_KEY_GETRAWBLASTLAYER);
        public const string REMOTE_BL_GETDIFFBLASTLAYER = nameof(REMOTE_BL_GETDIFFBLASTLAYER);
        public const string REMOTE_LONGARRAY_FILTERDOMAIN = nameof(REMOTE_LONGARRAY_FILTERDOMAIN);

        public const string REMOTE_SET_APPLYUNCORRUPTBL = nameof(REMOTE_SET_APPLYUNCORRUPTBL);
        public const string REMOTE_SET_APPLYCORRUPTBL = nameof(REMOTE_SET_APPLYCORRUPTBL);

        public const string REMOTE_CLEARSTEPBLASTUNITS = nameof(REMOTE_CLEARSTEPBLASTUNITS);
        public const string REMOTE_REMOVEEXCESSINFINITESTEPUNITS = nameof(REMOTE_REMOVEEXCESSINFINITESTEPUNITS);
        public const string REMOTE_EVENT_LOADGAMEDONE_NEWGAME = nameof(REMOTE_EVENT_LOADGAMEDONE_NEWGAME);
        public const string REMOTE_EVENT_LOADGAMEDONE_SAMEGAME = nameof(REMOTE_EVENT_LOADGAMEDONE_SAMEGAME);
        public const string REMOTE_EVENT_CLOSEEMULATOR = nameof(REMOTE_EVENT_CLOSEEMULATOR);
        public const string REMOTE_EVENT_SHUTDOWN = nameof(REMOTE_EVENT_SHUTDOWN);
        public const string REMOTE_OPENHEXEDITOR = nameof(REMOTE_OPENHEXEDITOR);

        public const string GENERATEBLASTLAYER = nameof(GENERATEBLASTLAYER);
        public const string SAVESAVESTATE = nameof(SAVESAVESTATE);
        public const string LOADSAVESTATE = nameof(LOADSAVESTATE);
        public const string REMOTE_LOADROM = nameof(REMOTE_LOADROM);
        public const string REMOTE_CLOSEGAME = nameof(REMOTE_CLOSEGAME);
        public const string REMOTE_PRECORRUPTACTION = nameof(REMOTE_PRECORRUPTACTION);
        public const string REMOTE_POSTCORRUPTACTION = nameof(REMOTE_POSTCORRUPTACTION);

        public const string REMOTE_BLASTTOOLS_GETAPPLIEDBACKUPLAYER = nameof(REMOTE_BLASTTOOLS_GETAPPLIEDBACKUPLAYER);

        public const string ERROR_DISABLE_AUTOCORRUPT = nameof(ERROR_DISABLE_AUTOCORRUPT);

        public const string REMOTE_KEY_SETSYNCSETTINGS = nameof(REMOTE_KEY_SETSYNCSETTINGS);
        public const string REMOTE_KEY_SETSYSTEMCORE = nameof(REMOTE_KEY_SETSYSTEMCORE);

        public const string EMU_OPEN_HEXEDITOR_ADDRESS = nameof(EMU_OPEN_HEXEDITOR_ADDRESS);
        public const string EMU_GET_REALTIME_API = nameof(EMU_GET_REALTIME_API);
        public const string EMU_GET_SCREENSHOT = nameof(EMU_GET_SCREENSHOT);
        public const string REMOTE_EVENT_EMU_MAINFORM_CLOSE = nameof(REMOTE_EVENT_EMU_MAINFORM_CLOSE);
        public const string REMOTE_EVENT_EMUSTARTED = nameof(REMOTE_EVENT_EMUSTARTED);

        public const string RTC_INFOCUS = nameof(RTC_INFOCUS);
        public const string EMU_INFOCUS = nameof(EMU_INFOCUS);

        public const string REMOTE_HOTKEY_MANUALBLAST = nameof(REMOTE_HOTKEY_MANUALBLAST);
        public const string REMOTE_HOTKEY_AUTOCORRUPTTOGGLE = nameof(REMOTE_HOTKEY_AUTOCORRUPTTOGGLE);
        public const string REMOTE_HOTKEY_ERRORDELAYDECREASE = nameof(REMOTE_HOTKEY_ERRORDELAYDECREASE);
        public const string REMOTE_HOTKEY_ERRORDELAYINCREASE = nameof(REMOTE_HOTKEY_ERRORDELAYINCREASE);
        public const string REMOTE_HOTKEY_INTENSITYDECREASE = nameof(REMOTE_HOTKEY_INTENSITYDECREASE);
        public const string REMOTE_HOTKEY_INTENSITYINCREASE = nameof(REMOTE_HOTKEY_INTENSITYINCREASE);
        public const string REMOTE_HOTKEY_GHLOADCORRUPT = nameof(REMOTE_HOTKEY_GHLOADCORRUPT);
        public const string REMOTE_HOTKEY_GHCORRUPT = nameof(REMOTE_HOTKEY_GHCORRUPT);
        public const string REMOTE_HOTKEY_GHREROLL = nameof(REMOTE_HOTKEY_GHREROLL);
        public const string REMOTE_HOTKEY_GHLOAD = nameof(REMOTE_HOTKEY_GHLOAD);
        public const string REMOTE_HOTKEY_GHSAVE = nameof(REMOTE_HOTKEY_GHSAVE);
        public const string REMOTE_HOTKEY_GHSTASHTOSTOCKPILE = nameof(REMOTE_HOTKEY_GHSTASHTOSTOCKPILE);
        public const string REMOTE_HOTKEY_BLASTRAWSTASH = nameof(REMOTE_HOTKEY_BLASTRAWSTASH);
        public const string REMOTE_HOTKEY_SENDRAWSTASH = nameof(REMOTE_HOTKEY_SENDRAWSTASH);
        public const string REMOTE_HOTKEY_BLASTLAYERTOGGLE = nameof(REMOTE_HOTKEY_BLASTLAYERTOGGLE);
        public const string REMOTE_HOTKEY_BLASTLAYERREBLAST = nameof(REMOTE_HOTKEY_BLASTLAYERREBLAST);
        public const string REMOTE_HOTKEY_GAMEPROTECTIONBACK = nameof(REMOTE_HOTKEY_GAMEPROTECTIONBACK);
        public const string REMOTE_HOTKEY_GAMEPROTECTIONNOW = nameof(REMOTE_HOTKEY_GAMEPROTECTIONNOW);
        public const string REMOTE_HOTKEY_BEINVERTDISABLED = nameof(REMOTE_HOTKEY_BEINVERTDISABLED);
        public const string REMOTE_HOTKEY_BEREMOVEDISABLED = nameof(REMOTE_HOTKEY_BEREMOVEDISABLED);
        public const string REMOTE_HOTKEY_BEDISABLE50 = nameof(REMOTE_HOTKEY_BEDISABLE50);
        public const string REMOTE_HOTKEY_BESHIFTUP = nameof(REMOTE_HOTKEY_BESHIFTUP);
        public const string REMOTE_HOTKEY_BESHIFTDOWN = nameof(REMOTE_HOTKEY_BESHIFTDOWN);
        public const string REMOTE_HOTKEY_BELOADCORRUPT = nameof(REMOTE_HOTKEY_BELOADCORRUPT);
        public const string REMOTE_HOTKEY_BEAPPLY = nameof(REMOTE_HOTKEY_BEAPPLY);
        public const string REMOTE_HOTKEY_BESENDSTASH = nameof(REMOTE_HOTKEY_BESENDSTASH);
    }
}
