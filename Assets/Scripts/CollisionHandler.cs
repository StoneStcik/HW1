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
    // 1. ��� ����� �������� ���� �������� � ����������
    // ��������� ���������� (��������� � ��������� ���)
    // 2. ��������� ��� ���
    // � ��������� ��, ����� �� ��������

    // 3. �������� ��� ������ ������ � �������� ��� �� ������
    // ��� ������ ���� �����������, �� �� ������������ ��� ������� ������

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
        // �� ��������� ��������� ����� � ������������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
