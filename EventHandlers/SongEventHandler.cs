using System;
using Zenject;

namespace KBSL_MOD.Events
{
    public class SongEventHandler : IInitializable, IDisposable
    {
        [Inject] private readonly GameSongController _gameSongController;
        [Inject] private readonly PlayerAllOverallStatsData _playerAllOverallStatsData;
        

        private void SongDidFinishEvent()
        {
            
        }
        
        public void Initialize()
        {
            _gameSongController.songDidFinishEvent += SongDidFinishEvent;
            // _playerAllOverallStatsData.allOverallStatsData.
        }

        public void Dispose()
        {
            _gameSongController.songDidFinishEvent -= SongDidFinishEvent;
        }
    }
}