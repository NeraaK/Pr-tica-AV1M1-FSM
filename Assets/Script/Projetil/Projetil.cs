using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 10f;
    private Transform alvo;
    public void AlvoPlayer(Transform player)
    {
        alvo = player;
    }

    
    void Update()
    {
        if (alvo != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, alvo.position, velocidade * Time.deltaTime);
            if (Vector3.Distance(transform.position, alvo.position) < 0.1)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject, 10f);
        }
    }
}
