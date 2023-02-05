using UnityEngine;
using UnityEngine.UI;

namespace SpaceGame
{
    public class InteractableBubble : MonoBehaviour
    {
        public void SetActive(bool active)
        {
            _active = active;
            LeanTween.cancel(gameObject);
            if (active)
            {
                SetProgress(0);
                LeanTween.scale(gameObject, Vector3.one, 0.21f).setEaseOutBack();
            }
            else
            {
                LeanTween.scale(gameObject, Vector3.zero, 0.21f).setEaseInBack();
            }
        }

        public void SetProgress(float progress)
        {
            _image.material.SetFloat("_Fill", progress * 0.9f);
        }

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private Image _image;
        private bool _active;
    }
}