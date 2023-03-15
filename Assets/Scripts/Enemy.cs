using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    TMP_Text asdf;

    private void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);
    }

    void somemothod()
    {
        print("hehe");
    }
}
