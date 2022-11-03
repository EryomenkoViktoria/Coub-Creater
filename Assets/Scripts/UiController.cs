using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

namespace Test.Cube
{
    public class UiController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _speedGrid, _distanseGrid, _timeGrid;
        [SerializeField] private Button _startButton, _errorButton, _pauseButton;
        [SerializeField] private GameObject _errorPanel;
        [SerializeField] private SpawnObject _spawnObject;
        [SerializeField] private Camera _camera;

        private const float ADDDISTANSE = 50;

        private int _speedData;
        public int SpeedData => _speedData;

        private int _distanseData;
        public int DistanseData => _distanseData;

        private float _timeData;
        public float TimeData => _timeData;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(CreateNewDataGame);
            _errorButton.onClick.AddListener(ClousedErrorDataPanel);
            _pauseButton.onClick.AddListener(StopGame);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(CreateNewDataGame);
            _errorButton.onClick.RemoveListener(ClousedErrorDataPanel);
            _pauseButton.onClick.RemoveListener(StopGame);
        }

        private void CreateNewDataGame()
        {
            if (IsError())
            {
                _errorPanel.SetActive(true);
                return;
            }
            ConvertNewData();
        }

        private void ConvertNewData()
        {
            _speedData = Convert.ToInt32(_speedGrid.text);
            _distanseData = Convert.ToInt32(_distanseGrid.text);
            _timeData = Convert.ToSingle(_timeGrid.text);
            CheckNewData();
        }

        private bool IsError()
        {
            bool error = false;
            string pattern = @"[0-9]";


           /// //string pattern = @"\d";
            var data = new string[]
            {
                
             _speedGrid.text,
             _distanseGrid.text,
             _timeGrid.text,
            };

            
            for (int i = 0; i < data.Length; i++)
            {
                if (!(Regex.IsMatch(data[i], pattern)))
                {
                   
                    error = true;
                    return error;
                }

                if (data[i] == "")
                {
                    error = true;
                    return error;
                }
            }


            return error;
        }

        private void CheckNewData()
        {
            _spawnObject.Activate(true);
            _camera.farClipPlane = _distanseData + ADDDISTANSE;
            ActivateButton(_pauseButton, _startButton);
            ActivateinputField(false);
            CreateGameField(_speedData, _distanseData, _timeData);
        }

        private void StopGame()
        {
            ActivateButton(_startButton, _pauseButton);
            _spawnObject.Activate(false);
            ActivateinputField(true);
        }
        private void ClousedErrorDataPanel()
        {
            _errorPanel.SetActive(false);
        }

        private void CreateGameField(int speed, int sistanse, float timeSpawn)
        {
            _spawnObject.StartGame(speed, sistanse, timeSpawn);
        }

        private void ActivateButton(Button on, Button off)
        {
            on.gameObject.SetActive(true);
            off.gameObject.SetActive(false);
        }

        private void ActivateinputField(bool activate)
        {
            _speedGrid.enabled = activate;
            _distanseGrid.enabled = activate;
            _timeGrid.enabled = activate;
        }

    }
}
