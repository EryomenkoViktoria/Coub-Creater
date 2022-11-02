using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Test.Cube
{
    public class MoveObject : MonoBehaviour
    {
        [SerializeField] private Transform _finishPoint;

        private float _duration;
        private const int SCALE = 0;
        private const float TIMESCALE = 1f;
        public void Initialization(int speed, int distanse, Transform finishPos)
        {
            _finishPoint = finishPos;
            _duration = distanse / speed;
            _finishPoint.position = new Vector3(distanse, 0, 0);

            Move();
            StartCoroutine(Destroy());
        }

        private void Move()
        {
            Tween move = transform.DOMove(_finishPoint.position, _duration, false);
            Tween scale = transform.DOScale(SCALE, TIMESCALE);
            var sequence = DOTween.Sequence();
            sequence.Append(move);
            sequence.Append(scale);
            sequence.Play();
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(_duration + TIMESCALE);
            Destroy(gameObject);
        }
    }
}
