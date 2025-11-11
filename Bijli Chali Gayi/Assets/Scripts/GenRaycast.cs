using UnityEngine;

namespace GeneratorSystem
{
    [RequireComponent(typeof(Camera))]
    public class GenRaycast : MonoBehaviour
    {
        [Header("Raycast Features")]
        [SerializeField] private float interactDistance = 5;
        private GenItem examinableItem;
        private Camera _camera;

        void Start()
        {
            _camera = GetComponent<Camera>();
        }

        void Update()
        {
            if (Physics.Raycast(_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), transform.forward, out RaycastHit hit, interactDistance))
            {
                var examineItem = hit.collider.GetComponent<GenItem>();
                if (examineItem != null)
                {
                    examinableItem = examineItem;
                    examinableItem.ShowObjectStats(true);
                    HighlightCrosshair(true);
                }
                else
                {
                    ClearExaminable();
                }
            }
            else
            {
                ClearExaminable();
            }

            if (examinableItem != null)
            {
                if (Input.GetKeyDown(GenInputManager.instance.interactKey))
                {
                    examinableItem.ObjectInteraction();
                }
            }
        }

        private void ClearExaminable()
        {
            if (examinableItem != null)
            {
                examinableItem.ShowObjectStats(false);
                HighlightCrosshair(false);
                examinableItem = null;
            }
        }

        private void HighlightCrosshair(bool on)
        {
            GenUIManager.instance.HighlightCrosshair(on);
        }
    }
}
