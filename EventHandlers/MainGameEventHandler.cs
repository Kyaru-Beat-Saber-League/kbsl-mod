using System;
using Zenject;

namespace KBSL_MOD.Events
{
    public class MainGameEventHandler : IInitializable, IDisposable
    {
        [Inject] private GameSongController _gameSongController;
        [Inject] private BeatmapObjectManager _beatmapObjectManager;
        [Inject] private ScoreController _scoreController;
        // [Inject] private BeatmapLevelSO _beatmapLevelSO;
        [Inject] private GameplayCoreSceneSetupData _gameplayCoreSceneSetup;
        [Inject] private PlayerDataModel _playerDataModel;
        
        private int _goodCuts = 0;
        private int _allCuts = 0;

        private void NoteWasCutEvent(NoteController controller, in NoteCutInfo info)
        {
            _allCuts++;
            if (controller.noteData.colorType != ColorType.None && info.allIsOK) _goodCuts++;
            Plugin.Log.Info($"allCut: {_allCuts} goodCut: {_goodCuts} percentage: {(float)_goodCuts / _allCuts * 100.0f}%");
        }

        private void NoteWasMissedEvent(NoteController controller)
        {
            if (controller.noteData.colorType == ColorType.None) return;
            _allCuts++;
            Plugin.Log.Info($"allCut: {_allCuts} goodCut: {_goodCuts} percentage: {(float)_goodCuts / _allCuts * 100.0f}%");
        }

        private void ScoreDidChangeEvent(int rawScore, int modifiedScore)
        {
            Plugin.Log.Info($"RawScore: {rawScore} modifiedScore: {modifiedScore}");
        }
        
        private void SongDidFinishEvent()
        {
            Plugin.Log.Notice("SongDidFinishEvent!");
        }
        
        public void Initialize()
        {
            _beatmapObjectManager.noteWasCutEvent += NoteWasCutEvent;
            _beatmapObjectManager.noteWasMissedEvent += NoteWasMissedEvent;
            _scoreController.scoreDidChangeEvent += ScoreDidChangeEvent;
            _gameSongController.songDidFinishEvent += SongDidFinishEvent;

            var pbl = _gameplayCoreSceneSetup.previewBeatmapLevel;
            Plugin.Log.Notice($"name: {pbl.songName} / {pbl.songAuthorName} / {pbl.levelID} / {pbl.levelAuthorName}");
            Plugin.Log.Notice("MainGameEventHandler Initialized!");
            
        }

        public void Dispose()
        {
            _beatmapObjectManager.noteWasCutEvent -= NoteWasCutEvent;
            _beatmapObjectManager.noteWasMissedEvent -= NoteWasMissedEvent;
            _scoreController.scoreDidChangeEvent -= ScoreDidChangeEvent;
            _gameSongController.songDidFinishEvent -= SongDidFinishEvent;
            Plugin.Log.Notice("MainGameEventHandler Disposed!");
        }
    }
}