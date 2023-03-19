using System;
using Zenject;

namespace KBSL_MOD.Events
{
    public class ScoreEventHandler : IInitializable, IDisposable
    {
        [Inject] private ScoreController _scoreController;

        private void ScoreDidChangeEvent(int rawScore, int modifiedScore)
        {
            Plugin.Log.Info($"RawScore: {rawScore} modifiedScore: {modifiedScore}");
        }
        
        public void Initialize()
        {
            _scoreController.scoreDidChangeEvent += ScoreDidChangeEvent;
            Plugin.Log.Notice("ScoreEventHandler Initialized!");
        }

        public void Dispose()
        {
            _scoreController.scoreDidChangeEvent -= ScoreDidChangeEvent;
            Plugin.Log.Notice("ScoreEventHandler Disposed!");
        }
    }
}