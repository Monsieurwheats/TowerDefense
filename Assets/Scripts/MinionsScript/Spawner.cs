using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject mP;

    private void Start()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        while (true)
        {

            Instantiate(mP, transform);
            yield return new WaitForSeconds(3f);
        }

        yield return null;
    }

}
