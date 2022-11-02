using System.Collections;
using UnityEngine;

namespace Test.Cube
{
    public class SpawnObject : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _finishPoint;
        [SerializeField] private Transform _startPoint;

        private bool _activate;

        public void StartGame(int speed, int distanse, float timeSpawn)
        {
            StartCoroutine(CreateObject(speed, distanse, timeSpawn));
        }

        private IEnumerator CreateObject(int speed, int distanse, float timeSpawn)
        {
            while (_activate)
            {
                Instantiate(_playerPrefab, _startPoint).GetComponent<MoveObject>().Initialization(speed, distanse, _finishPoint);
                yield return new WaitForSeconds(timeSpawn);
            }
        }

        public void Activate(bool activate)
        {
            _activate = activate;
        }
    }
}
