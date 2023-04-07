using System;
using JetBrains.Annotations;
using KBSL_MOD.Common;
using KBSL_MOD.Models;
using KBSL_MOD.Utils;
using Newtonsoft.Json;
using Zenject;

namespace KBSL_MOD.EventHandlers
{
    [UsedImplicitly]
    public class MainGameEventHandler : IInitializable, IDisposable
    {
        [Inject] private GameSongController _gameSongController;
        [Inject] private BeatmapObjectManager _beatmapObjectManager;
        [Inject] private ScoreController _scoreController;
        [Inject] private GameplayCoreSceneSetupData _gameplayCoreSceneSetup;
        [Inject] private PlayerDataModel _playerDataModel;
        [Inject] private StandardLevelScenesTransitionSetupDataSO _levelScenesTransitionSetupDataSo;
        
        private int _goodCuts;
        private int _allCuts;
        private int _modifiedScore;

        private void NoteWasCutEvent(NoteController controller, in NoteCutInfo info)
        {
            _allCuts++;
            if (controller.noteData.colorType != ColorType.None && info.allIsOK) _goodCuts++;
        }

        private void NoteWasMissedEvent(NoteController controller)
        {
            if (controller.noteData.colorType == ColorType.None) return;
            _allCuts++;
        }

        private void ScoreDidChangeEvent(int rawScore, int modifiedScore)
        {
            _modifiedScore = modifiedScore;
        }
        
        private void SongDidFinishEvent()
        {
            var beatmapLevel = _gameplayCoreSceneSetup.previewBeatmapLevel;
            var scores = new LeaderModel
            {
                BaseScore = _modifiedScore,
                Accuracy = (double)_goodCuts / _allCuts * 100.0f,
                BadCut = _allCuts - _goodCuts,
                SongHash = beatmapLevel.levelID.Replace("custom_level_", ""),
                SongModeType = _gameplayCoreSceneSetup.difficultyBeatmap.parentDifficultyBeatmapSet.beatmapCharacteristic.serializedName,
                SongDifficulty = _gameplayCoreSceneSetup.difficultyBeatmap.difficulty.ToString()
            };

            CoroutineUtils.instance.StartCoroutine(WebRequestUtils.Post(
                url: $"{KbslConsts.BaseURL}/score",
                body: JsonConvert.SerializeObject(scores),
                onSuccess: success => { Plugin.Log.Notice(JsonConvert.SerializeObject(success)); },
                onFailure: x => Plugin.Log.Notice(x)));

            Plugin.Log.Notice(JsonConvert.SerializeObject(scores));
        }
        
        public void Initialize()
        {
            _beatmapObjectManager.noteWasCutEvent += NoteWasCutEvent;
            _beatmapObjectManager.noteWasMissedEvent += NoteWasMissedEvent;
            _scoreController.scoreDidChangeEvent += ScoreDidChangeEvent;
            _gameSongController.songDidFinishEvent += SongDidFinishEvent;
        }

        public void Dispose()
        {
            _beatmapObjectManager.noteWasCutEvent -= NoteWasCutEvent;
            _beatmapObjectManager.noteWasMissedEvent -= NoteWasMissedEvent;
            _scoreController.scoreDidChangeEvent -= ScoreDidChangeEvent;
            _gameSongController.songDidFinishEvent -= SongDidFinishEvent;
            GamePlayUtils.Init();
        }
    }
}