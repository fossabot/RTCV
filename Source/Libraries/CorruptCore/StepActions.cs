namespace RTCV.CorruptCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using RTCV.NetCore;

    ///Rather than handling everything individually, we have a system here that works on collections of Blastunits
    ///In most usage, you're probably only going to have a small number of different lifetime/start time mixtures
    ///Rather than operating on every unit individually, we place everything into collections of Blastunits and then operate on them
    ///We have four lists and a linked list.
    ///preProcess contains all the blastunits as they're queued up
    ///buListCollection is the collection of all blast unit lists once they've been filtered into groups of shared StartFrame and Lifetime
    ///queuedLifetime is a linked list that contains a sorted version of all the blastunit lists with a limited lifetime that have let to be applied
    ///appliedLifetime and appliedInfinite are the two collections where we store what we want to actually be applied
    public static class StepActions
    {
        private static List<List<BlastUnit>> buListCollection = new List<List<BlastUnit>>();

        private static LinkedList<List<BlastUnit>> queued = new LinkedList<List<BlastUnit>>();
        private static List<List<BlastUnit>> appliedLifetime = new List<List<BlastUnit>>();
        private static List<List<BlastUnit>> appliedInfinite = new List<List<BlastUnit>>();


        public static List<BlastUnit> StoreDataPool = new List<BlastUnit>();

        private static int currentFrame = 0;
        private static int nextFrame = -1;

        private static bool isRunning = false;
        private static object executeLock = new object();

        public static event EventHandler StepStart;
        public static event EventHandler StepPreCorrupt;
        public static event EventHandler StepPostCorrupt;
        public static event EventHandler StepEnd;


        public static int MaxInfiniteBlastUnits
        {
            get => (int)RTCV.NetCore.AllSpec.CorruptCoreSpec[RTCSPEC.STEP_MAXINFINITEBLASTUNITS];
            set => RTCV.NetCore.AllSpec.CorruptCoreSpec.Update(RTCSPEC.STEP_MAXINFINITEBLASTUNITS, value);
        }

        public static bool LockExecution
        {
            get => (bool)RTCV.NetCore.AllSpec.CorruptCoreSpec[RTCSPEC.STEP_LOCKEXECUTION];
            set => RTCV.NetCore.AllSpec.CorruptCoreSpec.Update(RTCSPEC.STEP_LOCKEXECUTION, value);
        }

        public static bool ClearStepActionsOnRewind
        {
            get => (bool)RTCV.NetCore.AllSpec.CorruptCoreSpec[RTCSPEC.STEP_CLEARSTEPACTIONSONREWIND];
            set => RTCV.NetCore.AllSpec.CorruptCoreSpec.Update(RTCSPEC.STEP_CLEARSTEPACTIONSONREWIND, value);
        }

        public static PartialSpec getDefaultPartial()
        {
            var partial = new PartialSpec("CorruptCore");

            partial[RTCSPEC.STEP_MAXINFINITEBLASTUNITS] = 50;
            partial[RTCSPEC.STEP_LOCKEXECUTION] = false;
            partial[RTCSPEC.STEP_CLEARSTEPACTIONSONREWIND] = false;

            return partial;
        }


        public static void ClearStepBlastUnits()
        {
            lock (executeLock)
            {
                //Clean out the working data to prevent memory leaks
                foreach (List<BlastUnit> buList in appliedLifetime)
                {
                    foreach (BlastUnit bu in buList)
                    {
                        bu.Working = null;
                    }
                }
                foreach (List<BlastUnit> buList in appliedInfinite)
                {
                    foreach (BlastUnit bu in buList)
                    {
                        bu.Working = null;
                    }
                }

                buListCollection = new List<List<BlastUnit>>();
                queued = new LinkedList<List<BlastUnit>>();
                appliedLifetime = new List<List<BlastUnit>>();
                appliedInfinite = new List<List<BlastUnit>>();
                StoreDataPool = new List<BlastUnit>();

                nextFrame = -1;
                currentFrame = 0;
                isRunning = false;
            }
        }

        public static void RemoveExcessInfiniteStepUnits()
        {
            if (LockExecution)
            {
                return;
            }

            lock (executeLock)
            {
                while (appliedInfinite.Count > MaxInfiniteBlastUnits)
                {
                    appliedInfinite.Remove(appliedInfinite[0]);
                }
            }
        }

        public static bool TryRemoveInfiniteStepUnits(string domain, long address)
        {
            lock (executeLock)
            {
                return appliedInfinite.RemoveAll(x => x.Exists(y => y.Lifetime == 0 &&
                    y.Domain == domain &&
                    y.Address == address)) > 0;
            }
        }

        public static bool InfiniteUnitExists(string domain, long address)
        {
            lock (executeLock)
            {
                return appliedInfinite.Any(x => x.Exists(y =>
                {
                    return y.Lifetime == 0 &&
                           y.Domain == domain &&
                           y.Address == address;
                }));
            }
        }

        public static BlastLayer GetAppliedInfiniteUnits()
        {
            lock (executeLock)
            {
                return new BlastLayer(appliedInfinite.SelectMany(x => x.Select(y => y)).ToList());
            }
        }

        public static BlastLayer GetRawBlastLayer()
        {
            lock (executeLock)
            {
                BlastLayer bl = new BlastLayer();
                var tempList = new List<List<BlastUnit>>();
                tempList.AddRange(appliedInfinite);
                tempList.AddRange(appliedLifetime);

                foreach (List<BlastUnit> buList in (buListCollection))
                {
                    foreach (BlastUnit bu in buList)
                    {
                        bl.Layer.Add(bu);
                    }
                }
                return bl;
            }
        }


        /*
         Iterate over all the existing batches.
         If a batch that matches all the params already exists, return that. otherwise, create and return a new batch.
         */
        public static List<BlastUnit> GetBatchedLayer(BlastUnit bu)
        {
            List<BlastUnit> collection = null;
            foreach (List<BlastUnit> it in buListCollection)
            {
                if ((it[0].Working.ExecuteFrameQueued == bu.Working.ExecuteFrameQueued) &&
                    (it[0].Lifetime == bu.Lifetime) &&
                    (it[0].Loop == bu.Loop) &&
                    CheckLimitersMatch(it[0], bu))
                {
                    //We found one that matches so return that
                    collection = it;
                    break;
                }
            }

            //Checks that the limiters match
            bool CheckLimitersMatch(BlastUnit bu1, BlastUnit bu2)
            {
                //We only care if it's pre-execute because otherwise its limiter is independent from batching
                if (bu1.LimiterTime != LimiterTime.PREEXECUTE)
                {
                    return true;
                }

                if (bu1.LimiterListHash == bu2.LimiterListHash &&
                    bu1.LimiterTime == bu2.LimiterTime &&
                    bu1.InvertLimiter == bu2.InvertLimiter)
                {
                    if (bu.Source == BlastUnitSource.STORE)
                    {
                        switch (bu1.StoreLimiterSource)
                        {
                            case StoreLimiterSource.ADDRESS:
                                return (bu1.Address == bu2.Address &&
                                        bu1.Domain == bu2.Domain
                                    );
                            case StoreLimiterSource.SOURCEADDRESS:
                                return (bu1.SourceAddress == bu2.SourceAddress &&
                                        bu1.SourceDomain == bu2.SourceDomain
                                    );
                            case StoreLimiterSource.BOTH:
                                return (bu1.Address == bu2.Address &&
                                        bu1.Domain == bu2.Domain &&
                                        bu1.SourceAddress == bu2.SourceAddress &&
                                        bu1.SourceDomain == bu2.SourceDomain
                                    );
                        }
                    }
                    else // It's VALUE so check the domain and address are the same
                    {
                        return (bu1.Address == bu2.Address &&
                                bu1.Domain == bu2.Domain
                            );
                    }
                }
                return false;
            }


            //No match so make a new list
            if (collection == null)
            {
                collection = new List<BlastUnit>();
                buListCollection.Add(collection);
            }

            return collection;
        }

        public static void AddBlastUnit(BlastUnit bu, bool overrideExecuteFrame)
        {
            lock (executeLock)
            {
                bool UseRealtime = (AllSpec.VanguardSpec[VSPEC.SUPPORTS_REALTIME] as bool? ?? true);
                if (!UseRealtime)
                {
                    bu.Working.ExecuteFrameQueued = 0;
                    bu.Working.LastFrame = 1;
                }
                else
                {
                    if (overrideExecuteFrame) //If a looping unit has a loop timing, use the loop timing instead of ExecuteFrame for subsequent loops
                    {
                        bu.Working.ExecuteFrameQueued = bu.LoopTiming.Value + currentFrame;
                        //We subtract 1 here as we want lifetime to be exclusive. 1 means 1 apply, not applies 0 > applies 1 > done
                        bu.Working.LastFrame = bu.Working.ExecuteFrameQueued + bu.Lifetime - 1;
                    }
                    else
                    {
                        bu.Working.ExecuteFrameQueued = bu.ExecuteFrame + currentFrame;
                        //We subtract 1 here as we want lifetime to be exclusive. 1 means 1 apply, not applies 0 > applies 1 > done
                        bu.Working.LastFrame = bu.Working.ExecuteFrameQueued + bu.Lifetime - 1;
                    }
                }

                var collection = GetBatchedLayer(bu);
                collection.Add(bu);
            }
        }

        //TODO OPTIMIZE THIS TO INSERT RATHER THAN REBUILD
        public static void FilterBuListCollection()
        {
            lock (executeLock)
            {
                //Build up our list of buLists
                foreach (List<BlastUnit> buList in queued)
                {
                    buListCollection.Add(buList);
                }

                //Empty queued out
                queued = new LinkedList<List<BlastUnit>>();

                //buListCollection = buListCollection.OrderBy(it => it[0].Working.ExecuteFrameQueued).ToList();
                //this didnt need to be stored since it is only being used in this one loop
                foreach (List<BlastUnit> buList in buListCollection.OrderBy(it => it[0].Working.ExecuteFrameQueued).ToList())
                {
                    queued.AddLast(buList);
                }

                //Nuke the list
                buListCollection = new List<List<BlastUnit>>();

                //There's data so have the execute loop actually do something
                nextFrame = (queued.First())[0].Working.ExecuteFrameQueued;
                isRunning = true;
            }
        }

        private static void GetStoreBackups()
        {
            foreach (var bu in StoreDataPool)
            {
                bu.StoreBackup();
            }
        }

        private static void CheckApply()
        {
            //We need to do this twice because the while loop is vital on the nextFrame being set from the very beginning.
            if (queued.Count == 0)
            {
                return;
            }
            //This will only occur if the queue has something in it due to the check above
            while (currentFrame >= nextFrame)
            {
                List<BlastUnit> buList = queued.First();

                bool dontApply = false;
                //This is our EnteringExecution
                foreach (BlastUnit bu in buList)
                {
                    //If it returns false, that means the layer shouldn't apply
                    //This is primarily for if a limiter returns false
                    //If this happens, we need to remove it from the pool and then return out
                    if (!bu.EnteringExecution())
                    {
                        queued.RemoveFirst();
                        dontApply = true;
                        break;
                    }
                }

                if (!dontApply)
                {
                    //Add it to the infinite pool
                    if (buList[0].Lifetime == 0)
                    {
                        appliedInfinite.Add(buList);
                        queued.RemoveFirst();
                    }
                    //Add it to the Lifetime pool
                    else
                    {
                        appliedLifetime.Add(buList);
                        queued.RemoveFirst();
                    }
                }

                //Check if the queue is empty
                if (queued.Count == 0)
                {
                    return;
                }
                //It's not empty so set the next frame
                nextFrame = (queued.First())[0].Working.ExecuteFrameQueued;
            }
        }

        public static void Execute()
        {
            lock (executeLock)
            {
                StepStart?.Invoke(null, new EventArgs());
                if (isRunning)
                {
                    bool needsRefilter = false;
                    //Queue everything up
                    CheckApply();

                    //Get the backups for any store units
                    GetStoreBackups();
                    StepPreCorrupt?.Invoke(null, new EventArgs());

                    //Execute all temp units
                    List<List<BlastUnit>> itemsToRemove = new List<List<BlastUnit>>();
                    foreach (List<BlastUnit> buList in appliedLifetime)
                    {
                        foreach (BlastUnit bu in buList)
                        {
                            var result = bu.Execute();
                            if (result == ExecuteState.ERROR)
                            {
                                var dr = MessageBox.Show(
                                    "Something went horribly wrong during BlastUnit execute. Aborting. Would you like to send this to the devs?",
                                    "A fatal error occurred", MessageBoxButtons.YesNo);
                                isRunning = false;
                                if (dr == DialogResult.Yes)
                                {
                                    throw new CustomException("BlastUnit appliedLifetime Execute threw up. Check the log for more info.", Environment.StackTrace);
                                }

                                return;
                            }
                            if (result == ExecuteState.HANDLEDERROR)
                            {
                                isRunning = false;
                                return;
                            }
                        }
                        if (buList[0].Working.LastFrame == currentFrame)
                        {
                            itemsToRemove.Add(buList);
                        }
                    }

                    //Execute all infinite lifetime units
                    foreach (List<BlastUnit> buList in appliedInfinite)
                    {
                        foreach (BlastUnit bu in buList)
                        {
                            var result = bu.Execute();
                            if (result == ExecuteState.ERROR)
                            {
                                var dr = MessageBox.Show(
                                    "Something went horribly wrong during BlastUnit execute. Aborting. Would you like to send this to the devs?",
                                    "A fatal error occurred", MessageBoxButtons.YesNo);
                                isRunning = false;
                                if (dr == DialogResult.Yes)
                                {
                                    throw new CustomException("BlastUnit appliedInfinite Execute threw up. Check the log for more info.", Environment.StackTrace);
                                }

                                return;
                            }
                            if (result == ExecuteState.HANDLEDERROR)
                            {
                                isRunning = false;
                                return;
                            }
                        }
                    }

                    StepPostCorrupt?.Invoke(null, new EventArgs());
                    //Increment the frame
                    currentFrame++;

                    //Remove any temp units that have expired
                    foreach (List<BlastUnit> buList in itemsToRemove)
                    {
                        //Remove it
                        appliedLifetime.Remove(buList);

                        foreach (BlastUnit bu in buList)
                        {
                            bu.Working = null;
                            //Remove it from the store pool
                            if (bu.Source == BlastUnitSource.STORE)
                            {
                                StoreDataPool.Remove(bu);
                            }
                        }

                        //If there's a loop, re-apply all the units
                        if (buList[0].Loop)
                        {
                            needsRefilter = true;
                            foreach (BlastUnit bu in buList)
                            {
                                bool applyLoopTiming = (bu.LoopTiming != null && bu.LoopTiming != -1);
                                bu.Apply(true, applyLoopTiming);
                            }
                        }
                    }
                    //We only call this if there's a loop
                    if (needsRefilter)
                    {
                        FilterBuListCollection();
                    }
                }
                StepEnd?.Invoke(null, new EventArgs());
            }
        }
    }
}
