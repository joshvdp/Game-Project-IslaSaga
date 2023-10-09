using UnityEngine;

namespace VFX
{
    public class DamageTextPopup : MonoBehaviour
    {
        [SerializeField] Renderer TextRenderer;
        [SerializeField] float Duration;
        [SerializeField] float RisingSpeed;
        [SerializeField] AnimationCurve DurationCurve;
        [SerializeField] AnimationCurve RisingCurve;

        float timeElapsed = 0;
        Color FadingFontColor;
        Color FadingOutlineColor;
        void Start()
        {
            transform.rotation = Quaternion.LookRotation((transform.position - Camera.main.transform.position)) * Quaternion.Euler(0f,0f, Random.Range(-45, 45));
            
            Destroy(gameObject, Duration + 0.5f);
        }

        // Update is called once per frame
        void Update()
        {
            FadeOut();
            PopupAnimation();
        }

        void PopupAnimation()
        {
            transform.Translate(Vector3.up * RisingSpeed * RisingCurve.Evaluate(Time.deltaTime), Space.World);
        }

        void FadeOut()
        {
            timeElapsed += Time.deltaTime;
            FadingFontColor = new Color(TextRenderer.material.GetColor("_FaceColor").r,
                                        TextRenderer.material.GetColor("_FaceColor").g,
                                        TextRenderer.material.GetColor("_FaceColor").b,
                                        Mathf.Lerp(1, 0, DurationCurve.Evaluate(timeElapsed) / Duration));

            FadingOutlineColor = new Color(TextRenderer.material.GetColor("_OutlineColor").r,
                                        TextRenderer.material.GetColor("_OutlineColor").g,
                                        TextRenderer.material.GetColor("_OutlineColor").b,
                                        Mathf.Lerp(1, 0,  DurationCurve.Evaluate(timeElapsed) / Duration));

            TextRenderer.material.SetColor("_FaceColor", FadingFontColor);
            TextRenderer.material.SetColor("_OutlineColor", FadingOutlineColor);
        }
    }
}
