using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class CollisionHandler : MonoBehaviour
{


    [SerializeField] private float timeToRealodScene = 2f;
    [SerializeField] private ParticleSystem crushVFX; // VFX - VisualEffects

    private void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCrushSequence();
    }
    // 1. Нам нужно добавить наши партиклы в переменную
    // Отключить управление (компонент и отключаем его)
    // 2. Отключить его меш
    // И проиграть их, когда мы врежемся

    // 3. Возьмите ваш эффект взрыва и добавьте его ко врагам
    // Как только враг разбивается, то вы проигрываете ваш партикл эффект

    private void StartCrushSequence()
    {
        GetComponent<MovementController>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        crushVFX.Play();
        Invoke(nameof(ReloadLevel), timeToRealodScene);

    }

    private void ReloadLevel()
    {
        // Не забывайте добавлять сцену в билдСеттингс
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
