using UnityEngine;
using TMPro;
using System.Collections;

public class SpawnToe : MonoBehaviour
{
    public float nailLength;
    public GameObject nail;
    public GameObject toe;

    [SerializeField] ParticleSystem despawnParticle;
    [SerializeField] AudioClip despawnSound;

    [SerializeField] TextMeshProUGUI toeText;

    private AudioSource audioSource;

    int numberOfToes;
    int totalToes;
    GameObject nailClone;
    GameObject toeClone;
    ParticleSystem currentParticle;

    Vector3 spawnPos;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfToes <= 0)  // If every nail is filed, new toes will spawn.
        {
            nailLength = Random.Range(0.5f, 2.5f);
            float randomX = Random.Range(-6.0f, 5.0f);
            float randomZ = Random.Range(0.0f, 3.75f);

            nail.transform.localScale = new Vector3(1, 1, nailLength);

            spawnPos = new Vector3(randomX, 1, randomZ);

            nailClone = Instantiate(nail, spawnPos, nail.transform.rotation);
            toeClone = Instantiate(toe, spawnPos, nail.transform.rotation);

            numberOfToes++;
        }

        if (FileMovement.levelPassed)  // Despawns toe if nails are filed completely.
        {
            Destroy(nailClone);
            Destroy(toeClone);

            if (currentParticle != null)  // Only one despawnParticle exists at the same time.
            {
                Destroy(currentParticle.gameObject);
            }

            currentParticle = Instantiate(despawnParticle, spawnPos, toeClone.transform.rotation);
            audioSource.PlayOneShot(despawnSound);

            numberOfToes--;
            totalToes++;
            toeText.text = "Happy Toes: " + totalToes;
            FileMovement.levelPassed = false;
        }
    }
}
