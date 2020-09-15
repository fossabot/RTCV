namespace RTCV.CorruptCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using RTCV.NetCore;
    using RTCV.NetCore.Commands;

    public class CorruptCoreConnector : IRoutable
    {
        private static volatile object loadLock = new object();
        private static object LoadLock => loadLock;

        public object OnMessageReceived(object sender, NetCoreEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            try
            { //Use setReturnValue to handle returns
                var message = e.message;
                var advancedMessage = message as NetCoreAdvancedMessage;

                switch (e.message.Type)
                {
                    case "GETSPECDUMPS":
                        GetSpecDumps(ref e);
                        break;
                    //UI sent its spec
                    case Remote.RemotePushUISpec:
                        {
                            SyncObjectSingleton.FormExecute(() => AllSpec.UISpec = new FullSpec((PartialSpec)advancedMessage.objectValue, !RtcCore.Attached));
                            break;
                        }

                    //UI sent a spec update
                    case Remote.REMOTE_PUSHUISPECUPDATE:
                        SyncObjectSingleton.FormExecute(() => AllSpec.UISpec?.Update((PartialSpec)advancedMessage.objectValue));
                        break;

                    //Vanguard sent a copy of its spec
                    case Remote.PushVanguardSpec:

                        SyncObjectSingleton.FormExecute(() =>
                        {
                            if (!RtcCore.Attached)
                            {
                                AllSpec.VanguardSpec = new FullSpec((PartialSpec)advancedMessage.objectValue, !RtcCore.Attached);
                            }
                        });
                        break;

                    //Vanguard sent a spec update
                    case Remote.PushVanguardSpecUpdate:
                        AllSpec.VanguardSpec?.Update((PartialSpec)advancedMessage.objectValue, false);
                        break;

                    //UI sent a copy of the CorruptCore spec
                    case Remote.PushCorruptCoreSpec:
                        PushCorruptCoreSpec((PartialSpec)advancedMessage.objectValue, ref e);
                        break;

                    //UI sent an update of the CorruptCore spec
                    case Remote.RemotePushCorruptCoreSpecUpdate:
                        SyncObjectSingleton.FormExecute(() => AllSpec.CorruptCoreSpec?.Update((PartialSpec)advancedMessage.objectValue, false));
                        break;

                    case Remote.REMOTE_EVENT_DOMAINSUPDATED:
                        var domainsChanged = (bool)advancedMessage.objectValue;
                        MemoryDomains.RefreshDomains(domainsChanged);
                        break;

                    case Remote.REMOTE_EVENT_RESTRICTFEATURES:
                        RestrictFeatures();
                        break;

                    case Remote.REMOTE_EVENT_SHUTDOWN:
                        RtcCore.Shutdown();
                        break;

                    case Remote.REMOTE_OPENHEXEDITOR:
                        OpenHexEditor();
                        break;

                    case Emulator.EMU_OPEN_HEXEDITOR_ADDRESS:
                        OpenHexEditorAddress(advancedMessage.objectValue);
                        break;

                    case Basic.MANUALBLAST:
                        RtcCore.GenerateAndBlast();
                        break;

                    case Basic.GENERATEBLASTLAYER:
                        GenerateBlastLayer(advancedMessage, ref e);
                        break;

                    case Basic.APPLYBLASTLAYER:
                        ApplyBlastLayer(advancedMessage);
                        break;

                    case Remote.REMOTE_PUSHRTCSPEC:
                        AllSpec.CorruptCoreSpec = new FullSpec((PartialSpec)advancedMessage.objectValue, !RtcCore.Attached);
                        e.setReturnValue(true);
                        break;

                    case Remote.REMOTE_PUSHRTCSPECUPDATE:
                        AllSpec.CorruptCoreSpec?.Update((PartialSpec)advancedMessage.objectValue, false);
                        break;

                    case Basic.BLASTGENERATOR_BLAST:
                        {
                            var valueAsObjectArr = advancedMessage.objectValue as object[];
                            BlastGeneratorBlast(valueAsObjectArr, ref e);
                        }
                        break;

                    case Remote.REMOTE_LOADSTATE:
                        {
                            var valueAsObjectArr = advancedMessage.objectValue as object[];
                            LoadState(valueAsObjectArr, ref e);
                        }
                        break;
                    case Remote.REMOTE_SAVESTATE:
                        {
                            StashKey sk = null;
                            void a()
                            {
                                sk = StockpileManager_EmuSide.SaveState_NET(advancedMessage.objectValue as StashKey); //Has to be nullable cast
                            }
                            SyncObjectSingleton.EmuThreadExecute(a, false);
                            e.setReturnValue(sk);
                        }
                        break;
                    case Remote.REMOTE_SAVESTATELESS:
                        {
                            StashKey sk = null;
                            void a()
                            {
                                sk = StockpileManager_EmuSide.SaveStateLess_NET(advancedMessage.objectValue as StashKey); //Has to be nullable cast
                            }
                            SyncObjectSingleton.EmuThreadExecute(a, false);
                            e.setReturnValue(sk);
                        }
                        break;

                    case Remote.REMOTE_BACKUPKEY_REQUEST:
                        {
                            //We don't store this in the spec as it'd be horrible to push it to the UI and it doesn't care
                            //if (!LocalNetCoreRouter.QueryRoute<bool>(NetCore.Commands.Basic.Vanguard, NetcoreCommands.REMOTE_ISNORMALADVANCE))
                            //break;

                            StashKey sk = null;
                            //We send an unsynced command back
                            SyncObjectSingleton.FormExecute(() => sk = StockpileManager_EmuSide.SaveState_NET());

                            if (sk != null)
                            {
                                LocalNetCoreRouter.Route(Basic.UI, Remote.REMOTE_BACKUPKEY_STASH, sk, false);
                            }

                            break;
                        }
                    case Remote.REMOTE_DOMAIN_GETDOMAINS:
                        e.setReturnValue(LocalNetCoreRouter.Route(Basic.Vanguard, Remote.REMOTE_DOMAIN_GETDOMAINS, true));
                        break;
                    case Remote.REMOTE_PUSHVMDPROTOS:
                        MemoryDomains.VmdPool.Clear();
                        foreach (var proto in (advancedMessage.objectValue as VmdPrototype[]))
                        {
                            MemoryDomains.AddVMD(proto);
                        }

                        break;

                    case Remote.REMOTE_DOMAIN_VMD_ADD:
                        MemoryDomains.AddVMDFromRemote((advancedMessage.objectValue as VmdPrototype));
                        break;

                    case Remote.REMOTE_DOMAIN_VMD_REMOVE:
                        {
                            StepActions.ClearStepBlastUnits();
                            MemoryDomains.RemoveVMDFromRemote((advancedMessage.objectValue as string));
                        }
                        break;

                    case Remote.REMOTE_DOMAIN_ACTIVETABLE_MAKEDUMP:
                        {
                            void a()
                            {
                                MemoryDomains.GenerateActiveTableDump(
                                    (string)(advancedMessage.objectValue as object[])[0],
                                    (string)(advancedMessage.objectValue as object[])[1]);
                            }

                            SyncObjectSingleton.EmuThreadExecute(a, false);
                        }
                        break;

                    case Remote.REMOTE_BLASTTOOLS_GETAPPLIEDBACKUPLAYER:
                        {
                            var bl = (BlastLayer)(advancedMessage.objectValue as object[])[0];
                            var sk = (StashKey)(advancedMessage.objectValue as object[])[1];

                            void a()
                            {
                                e.setReturnValue(BlastTools.GetAppliedBackupLayer(bl, sk));
                            }

                            SyncObjectSingleton.EmuThreadExecute(a, false);
                            break;
                        }

                    case Remote.REMOTE_LONGARRAY_FILTERDOMAIN:
                        {
                            var objValues = (advancedMessage.objectValue as object[]);
                            FilterDomain(objValues, ref e);
                        }
                        break;

                    case Remote.REMOTE_KEY_GETRAWBLASTLAYER:
                        {
                            void a()
                            { e.setReturnValue(StockpileManager_EmuSide.GetRawBlastlayer()); }
                            SyncObjectSingleton.EmuThreadExecute(a, false);
                        }
                        break;

                    case Remote.REMOTE_BL_GETDIFFBLASTLAYER:
                        {
                            var filename = advancedMessage.objectValue as string;
                            void a()
                            { e.setReturnValue(BlastDiff.GetBlastLayer(filename)); }
                            SyncObjectSingleton.EmuThreadExecute(a, false);
                        }
                        break;

                    case Remote.REMOTE_SET_APPLYUNCORRUPTBL:
                        {
                            void a()
                            {
                                StockpileManager_EmuSide.UnCorruptBL?.Apply(true);
                            }
                            SyncObjectSingleton.EmuThreadExecute(a, false);
                        }
                        break;

                    case Remote.REMOTE_SET_APPLYCORRUPTBL:
                        {
                            void a()
                            {
                                StockpileManager_EmuSide.CorruptBL?.Apply(false);
                            }
                            SyncObjectSingleton.EmuThreadExecute(a, false);
                        }
                        break;

                    case Remote.REMOTE_CLEARSTEPBLASTUNITS:
                        SyncObjectSingleton.FormExecute(() => StepActions.ClearStepBlastUnits());
                        break;

                    case Remote.REMOTE_LOADPLUGINS:
                        LoadPlugins();
                        break;
                    case Remote.REMOTE_REMOVEEXCESSINFINITESTEPUNITS:
                        SyncObjectSingleton.FormExecute(() => StepActions.RemoveExcessInfiniteStepUnits());
                        break;

                    default:
                        new object();
                        break;
                }

                return e.returnMessage;
            }
            catch (Exception ex)
            {
                if (CloudDebug.ShowErrorDialog(ex, true) == DialogResult.Abort)
                {
                    throw new AbortEverythingException();
                }

                return e.returnMessage;
            }
        }

        private static void GetSpecDumps(ref NetCoreEventArgs e)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Spec Dump from CorruptCore");
            sb.AppendLine();
            sb.AppendLine("UISpec");
            AllSpec.UISpec?.GetDump().ForEach(x => sb.AppendLine(x));
            sb.AppendLine("CorruptCoreSpec");
            AllSpec.CorruptCoreSpec?.GetDump().ForEach(x => sb.AppendLine(x));
            sb.AppendLine("VanguardSpec");
            AllSpec.VanguardSpec?.GetDump().ForEach(x => sb.AppendLine(x));
            e.setReturnValue(sb.ToString());
        }

        private static void PushCorruptCoreSpec(PartialSpec partialSpec, ref NetCoreEventArgs e)
        {
            SyncObjectSingleton.FormExecute(() =>
            {
                //So here's the deal. The UI doesn't actually have the full memory domains (md isn't sent across) so if we take them from it, it results in them going null
                //Instead, we stick with what we have, then tell the UI to use that.

                var temp = new FullSpec(partialSpec, !RtcCore.Attached);

                //Stick with what we have if it exists to prevent any exceptions if autocorrupt was on or something, then call refresh
                temp.Update("MEMORYINTERFACES", AllSpec.CorruptCoreSpec?["MEMORYINTERFACES"] ?? new Dictionary<string, MemoryDomainProxy>());

                AllSpec.CorruptCoreSpec = new FullSpec(temp.GetPartialSpec(), !RtcCore.Attached);
                AllSpec.CorruptCoreSpec.SpecUpdated += (ob, eas) =>
                {
                    PartialSpec partial = eas.partialSpec;
                    LocalNetCoreRouter.Route(Basic.UI, Remote.RemotePushCorruptCoreSpecUpdate, partial, true);
                };
                MemoryDomains.RefreshDomains();
            });
            e.setReturnValue(true);
        }

        private static void RestrictFeatures()
        {
            if (!AllSpec.VanguardSpec?.Get<bool>(VSPEC.SUPPORTS_SAVESTATES) ?? true)
            {
                LocalNetCoreRouter.Route(Basic.UI, Remote.REMOTE_DISABLESAVESTATESUPPORT);
            }

            if (!AllSpec.VanguardSpec?.Get<bool>(VSPEC.SUPPORTS_REALTIME) ?? true)
            {
                LocalNetCoreRouter.Route(Basic.UI, Remote.REMOTE_DISABLEREALTIMESUPPORT);
            }

            if (!AllSpec.VanguardSpec?.Get<bool>(VSPEC.SUPPORTS_KILLSWITCH) ?? true)
            {
                LocalNetCoreRouter.Route(Basic.UI, Remote.REMOTE_DISABLEKILLSWITCHSUPPORT);
            }

            if (!AllSpec.VanguardSpec?.Get<bool>(VSPEC.SUPPORTS_GAMEPROTECTION) ?? true)
            {
                LocalNetCoreRouter.Route(Basic.UI, Remote.REMOTE_DISABLEGAMEPROTECTIONSUPPORT);
            }
        }

        private static void LoadState(object[] valueAsObjectArr, ref NetCoreEventArgs e)
        {
            lock (LoadLock)
            {
                var sk = (StashKey)valueAsObjectArr[0];
                var reloadRom = (bool)valueAsObjectArr[1];
                var runBlastLayer = (bool)valueAsObjectArr[2];

                var returnValue = false;

                //Load the game from the main thread
                if (reloadRom)
                {
                    SyncObjectSingleton.FormExecute(() => StockpileManager_EmuSide.LoadRom_NET(sk));
                }
                void a()
                {
                    returnValue = StockpileManager_EmuSide.LoadState_NET(sk, runBlastLayer);
                }
                //If the emulator uses callbacks, we do everything on the main thread and once we're done, we unpause emulation
                if ((bool?)AllSpec.VanguardSpec[VSPEC.LOADSTATE_USES_CALLBACKS] ?? false)
                {
                    SyncObjectSingleton.FormExecute(a);
                    e.setReturnValue(LocalNetCoreRouter.Route(Basic.Vanguard, Remote.REMOTE_RESUMEEMULATION, true));
                }
                else //We're loading on the emulator thread which'll block
                {
                    SyncObjectSingleton.EmuThreadExecute(a, false);
                }
                e.setReturnValue(returnValue);
            }
        }

        private static void OpenHexEditor()
        {
            if ((bool?)AllSpec.VanguardSpec[VSPEC.USE_INTEGRATED_HEXEDITOR] ?? false)
            {
                LocalNetCoreRouter.Route(Basic.Vanguard, Remote.REMOTE_OPENHEXEDITOR, true);
            }
            else
            {
                //Route it to the plugin if loaded
                if (RtcCore.PluginHost.LoadedPlugins.Any(x => x.Name == "Hex Editor"))
                {
                    LocalNetCoreRouter.Route("HEXEDITOR", Remote.REMOTE_OPENHEXEDITOR, true);
                }
                else
                {
                    MessageBox.Show("The current Vanguard implementation does not include a\n hex editor & the hex editor plugin isn't loaded. Aborting.");
                }
            }
        }

        private static void OpenHexEditorAddress(object objectValue)
        {
            if ((bool?)AllSpec.VanguardSpec[VSPEC.USE_INTEGRATED_HEXEDITOR] ?? false)
            {
                LocalNetCoreRouter.Route(Basic.Vanguard, Emulator.EMU_OPEN_HEXEDITOR_ADDRESS, objectValue, true);
            }
            else
            {
                //Route it to the plugin if loaded
                if (RtcCore.PluginHost.LoadedPlugins.Any(x => x.Name == "Hex Editor"))
                {
                    LocalNetCoreRouter.Route("HEXEDITOR", Emulator.EMU_OPEN_HEXEDITOR_ADDRESS, objectValue, true);
                }
                else
                {
                    MessageBox.Show("The current Vanguard implementation does not include a\n hex editor & the hex editor plugin isn't loaded. Aborting.");
                }
            }
        }

        private static void BlastGeneratorBlast(object[] valueAsObjectArr, ref NetCoreEventArgs e)
        {
            List<BlastGeneratorProto> returnList = null;
            var sk = (StashKey)valueAsObjectArr[0];
            var blastGeneratorProtos = (List<BlastGeneratorProto>)valueAsObjectArr[1];
            var loadBeforeCorrupt = (bool)valueAsObjectArr[2];
            var applyAfterCorrupt = (bool)valueAsObjectArr[3];
            var resumeAfter = (bool)valueAsObjectArr[4];
            void a()
            {
                //Load the game from the main thread
                if (loadBeforeCorrupt)
                {
                    SyncObjectSingleton.FormExecute(() => StockpileManager_EmuSide.LoadRom_NET(sk));
                }

                if (loadBeforeCorrupt)
                {
                    StockpileManager_EmuSide.LoadState_NET(sk, false);
                }

                returnList = BlastTools.GenerateBlastLayersFromBlastGeneratorProtos(blastGeneratorProtos);
                if (applyAfterCorrupt)
                {
                    var bl = new BlastLayer();
                    foreach (var p in returnList.Where(x => x != null))
                    {
                        bl.Layer.AddRange(p.bl.Layer);
                    }
                    bl.Apply(true);
                }
            }
            //If the emulator uses callbacks, we do everything on the main thread and once we're done, we unpause emulation
            if ((bool?)AllSpec.VanguardSpec[VSPEC.LOADSTATE_USES_CALLBACKS] ?? false)
            {
                SyncObjectSingleton.FormExecute(a);
                if (resumeAfter)
                {
                    e.setReturnValue(LocalNetCoreRouter.Route(Basic.Vanguard, Remote.REMOTE_RESUMEEMULATION, true));
                }
            }
            else
            {
                SyncObjectSingleton.EmuThreadExecute(a, false);
            }

            e.setReturnValue(returnList);
        }

        private static void GenerateBlastLayer(NetCoreAdvancedMessage advancedMessage, ref NetCoreEventArgs e)
        {
            var val = advancedMessage.objectValue as object[];
            var sk = val[0] as StashKey;
            var loadBeforeCorrupt = (bool)val[1];
            var applyBlastLayer = (bool)val[2];
            var backup = (bool)val[3];

            BlastLayer bl = null;

            var useSavestates = (bool)AllSpec.VanguardSpec[VSPEC.SUPPORTS_SAVESTATES];

            void a()
            {
                lock (LoadLock)
                {
                    //Load the game from the main thread
                    if (useSavestates && loadBeforeCorrupt)
                    {
                        SyncObjectSingleton.FormExecute(() => StockpileManager_EmuSide.LoadRom_NET(sk));
                    }

                    if (useSavestates && loadBeforeCorrupt)
                    {
                        StockpileManager_EmuSide.LoadState_NET(sk, false);
                    }

                    bl = RtcCore.GenerateBlastLayerOnAllThreads();

                    if (applyBlastLayer)
                    {
                        bl?.Apply(backup);
                    }
                }
            }

            //If the emulator uses callbacks, we do everything on the main thread and once we're done, we unpause emulation
            if ((bool?)AllSpec.VanguardSpec[VSPEC.LOADSTATE_USES_CALLBACKS] ?? false)
            {
                SyncObjectSingleton.FormExecute(a);
                e.setReturnValue(LocalNetCoreRouter.Route(Basic.Vanguard, Remote.REMOTE_RESUMEEMULATION, true));
            }
            else //We can just do everything on the emulation thread as it'll block
            {
                SyncObjectSingleton.EmuThreadExecute(a, true);
            }

            if (advancedMessage.requestGuid != null)
            {
                e.setReturnValue(bl);
            }
        }

        private static void ApplyBlastLayer(NetCoreAdvancedMessage advancedMessage)
        {
            var temp = advancedMessage.objectValue as object[];
            var bl = (BlastLayer)temp[0];
            var storeUncorruptBackup = (bool)temp[1];
            var merge = (temp.Length > 2) && (bool)temp[2];
            void a()
            {
                bl.Apply(storeUncorruptBackup, true, merge);
            }

            SyncObjectSingleton.EmuThreadExecute(a, true);
        }

        private static void FilterDomain(object[] objValues, ref NetCoreEventArgs e)
        {
            lock (LoadLock)
            {
                var domain = (string)objValues[0];
                var limiterListHash = (string)objValues[1];
                var sk = objValues[2] as StashKey; //Intentionally nullable
                var allLegalAdresses = new List<long>();

                void a()
                {
                    if (sk != null) //If a stashkey was passed in, we want to load then profile
                    {
                        StockpileManager_EmuSide.LoadState_NET(sk, false);
                    }

                    MemoryInterface mi = MemoryDomains.MemoryInterfaces[domain];

                    var listItemSize = Filtering.GetPrecisionFromHash(limiterListHash);

                    for (long i = 0; i < mi.Size; i += listItemSize)
                    {
                        if (Filtering.LimiterPeekBytes(i, i + listItemSize, limiterListHash, mi))
                        {
                            for (var j = 0; j < listItemSize; j++)
                            {
                                allLegalAdresses.Add(i + j);
                            }
                        }
                    }
                }

                //If the emulator uses callbacks and we're loading a state, we do everything on the main thread and once we're done, we unpause emulation
                if (sk != null && ((bool?)AllSpec.VanguardSpec[VSPEC.LOADSTATE_USES_CALLBACKS] ?? false))
                {
                    SyncObjectSingleton.FormExecute(a);
                    LocalNetCoreRouter.Route(Basic.Vanguard, Remote.REMOTE_RESUMEEMULATION, true);
                }
                else //We can just do everything on the emulation thread as it'll block
                {
                    SyncObjectSingleton.EmuThreadExecute(a, true);
                }

                e.setReturnValue(allLegalAdresses.ToArray());
            }
        }

        private static void LoadPlugins()
        {
            SyncObjectSingleton.FormExecute(() =>
                {
                    var emuPluginDir = string.Empty;
                    try
                    {
                        emuPluginDir = System.IO.Path.Combine(RtcCore.EmuDir, "RTC", "PLUGINS");
                    }
                    catch (Exception e)
                    {
                        Common.Logging.GlobalLogger.Error(e, "Unable to find plugin dir in {dir}", RtcCore.EmuDir + "\\RTC" + "\\PLUGINS");
                    }
                    RtcCore.LoadPlugins(new[] { RtcCore.PluginDir,  emuPluginDir });
                });
        }

        public static void Kill()
        {
        }
    }
}
