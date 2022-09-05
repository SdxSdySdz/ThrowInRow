using DG.Tweening;
using UnityEngine;

namespace Sources.GameView
{
    public class Stone : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        
        public void Init(Color color)
        {
            _renderer.material.color = color;
        }
        
        public void TakePlace(Vector3 position)
        {
            // var tween = transform.DOMove(position, 1f);

            Vector3 startPosition = transform.position;
            DOTween.To(setter: value =>
                    {
                        transform.position = Parabola(startPosition, position, 10, value);
                    },
                    startValue: 0,
                    endValue: 1,
                    duration: 0.5f)
                .SetEase(Ease.Linear);
        }
        
        private static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
        {
            float Func(float x) => 4 * (-height * x * x + height * x);

            var mid = Vector3.Lerp(start, end, t);

            return new Vector3(mid.x, Func(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
        }
    }
}