using UnityEngine;
using System.Collections.Generic;

public class FondoInfinito : MonoBehaviour
{
    public GameObject fondoPrefab;
    public Transform camara;
    public float largoFondo = 20f;

    private List<GameObject> fondos = new List<GameObject>();
    private float ultimoX;

    void Start()
    {
        if (fondoPrefab != null && camara != null)
        {
            SpriteRenderer sr = fondoPrefab.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                largoFondo = sr.sprite.bounds.size.x * fondoPrefab.transform.localScale.x;
            }

            for (int i = 0; i < 2; i++)
            {
                Vector3 pos = new Vector3(i * largoFondo, 0, 0);
                GameObject fondo = Instantiate(fondoPrefab, pos, Quaternion.identity);
                fondos.Add(fondo);
            }

            ultimoX = fondos[1].transform.position.x;
        }
    }

    void Update()
    {
        if (camara.position.x + largoFondo * 1.5f > ultimoX)
        {
            Vector3 nuevaPos = new Vector3(ultimoX + largoFondo, 0, 0);
            GameObject nuevoFondo = Instantiate(fondoPrefab, nuevaPos, Quaternion.identity);
            fondos.Add(nuevoFondo);

            if (fondos.Count > 3)
            {
                Destroy(fondos[0]);
                fondos.RemoveAt(0);
            }

            ultimoX = nuevoFondo.transform.position.x;
        }
    }
}
