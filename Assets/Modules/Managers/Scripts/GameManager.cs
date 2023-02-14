using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceGame
{
    public class GameManager : MonoSingleton<GameManager>
    {
        protected override void Awake()
        {
            // TODO: Make sure we properly dispose of events later
            _taskCompleteEvent.EventRaised += OnTaskCompleted;
            _taskFailEvent.EventRaised += OnTaskFailed;
            
            _allyHealth.Setup();
            _enemyHealth.Setup();
            _allyHealth.Died += GameOver;
            
            _scoreEvent.RaiseEvent(_score);
            base.Awake();
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Game");
        }

        public void GameOver()
        {
            _gameUI.ShowGameOver();
            Time.timeScale = 0f;
        }

        private void OnTaskCompleted()
        {
            // Do nothing for now
            _score += 1;
            _scoreEvent.RaiseEvent(_score);
        }
        
        private void OnTaskFailed()
        {
            // Deal damage to the player's ship
            _allyHealth.DealDamage(0);
            CameraShake.Instance.Shake();
            //Seth edit
            _repairEventManager.AddRepairEvent();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                CameraShake.Instance.Shake();
        }

        private int _score = 0;
        //Seth edit
        [SerializeField] private RepairEventManager _repairEventManager;

        [SerializeField] private GameUI _gameUI;
        [Header("Health")]
        [SerializeField] private HealthData _allyHealth;
        [SerializeField] private HealthData _enemyHealth;
        [Header("Events")]
        [SerializeField] private VoidEventChannel _taskCompleteEvent;
        [SerializeField] private VoidEventChannel _taskFailEvent;
        [SerializeField] private IntEventChannel _scoreEvent;
    }
}