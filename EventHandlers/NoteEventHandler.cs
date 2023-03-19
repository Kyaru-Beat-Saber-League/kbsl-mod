using System;
using Zenject;

namespace KBSL_MOD.Events
{
    public class NoteEventHandler : IInitializable, IDisposable
    {
        [Inject] private BeatmapObjectManager _beatmapObjectManager;
        
        private int _goodCuts = 0;
        private int _allCuts = 0;

        private void NoteWasCutEvent(NoteController controller, in NoteCutInfo info)
        {
            _allCuts++;
            if (controller.noteData.colorType != ColorType.None && info.allIsOK) _goodCuts++;
            Debug();
        }

        private void NoteWasMissedEvent(NoteController controller)
        {
            if (controller.noteData.colorType == ColorType.None) return;
            _allCuts++;
            Debug();
        }

        private void Debug()
        {
            Plugin.Log.Info($"allCut: {_allCuts} goodCut: {_goodCuts} percentage: {(float)_goodCuts / _allCuts * 100.0f}%");
        }

        public void Initialize()
        {
            _beatmapObjectManager.noteWasCutEvent += NoteWasCutEvent;
            _beatmapObjectManager.noteWasMissedEvent += NoteWasMissedEvent;
            Plugin.Log.Notice("NoteEventHandler Initialized!");
        }
        
        public void Dispose()
        {
            _beatmapObjectManager.noteWasCutEvent -= NoteWasCutEvent;
            _beatmapObjectManager.noteWasMissedEvent -= NoteWasMissedEvent;
            Plugin.Log.Notice("NoteEventHandler Disposed!");
        }
    }
}