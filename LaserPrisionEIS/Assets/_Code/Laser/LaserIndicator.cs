using System.Collections;
using UnityEngine;

public class LaserIndicator : MonoBehaviour
{
    [SerializeField] private GameObject _indicator;
    [SerializeField] private GameObject _laser;

    private void OnEnable()
    {
        _indicator.transform.localScale = Vector3.zero;
        _laser.SetActive(false);

        Invoke("ShowIndicator", 2f);
    }

    private void OnDisable()
    {
        CancelInvoke("ShowIndicator");
        CancelInvoke("ActivateLaser");
    }

    void ShowIndicator()
    {
        Vector3 FinalScale = new Vector3(0.3f, 0.3f, 0.3f);

        StartCoroutine(ScaleIndicator(_indicator.transform.localScale, FinalScale, 2f));

        Invoke("ActivateLaser", 2f);
    }

    void ActivateLaser()
    {
        _laser.SetActive(true);
        Invoke("DeactivateLaser", 3f);
    }

    void DeactivateLaser()
    {
        ObjectPooler.EnqueueObject(this, "Lasers");
    }

    IEnumerator ScaleIndicator(Vector3 StartScale, Vector3 FinalScale, float Duration)
    {
        float TimeElapsed = 0f;

        while (TimeElapsed <= Duration)
        {
            _indicator.transform.localScale = Vector3.Lerp(StartScale, FinalScale, TimeElapsed / Duration);

            TimeElapsed += Time.deltaTime;
            yield return null;
        }

        _indicator.transform.localScale = FinalScale;
    }
}
