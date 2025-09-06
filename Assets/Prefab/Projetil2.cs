using UnityEngine;

public class Projetil2 : MonoBehaviour
{
    public float velocidade = 15f;
    public float dano = 20f;
    private Transform alvo;

    
    public void AlvoGuardiao(Transform novoAlvo)
    {
        alvo = novoAlvo;
    }

    void Update()
    {
        if (alvo == null) return;

        transform.position = Vector3.MoveTowards(transform.position, alvo.position, velocidade * Time.deltaTime);

        if (Vector3.Distance(transform.position, alvo.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Atividade2 vidaInimigo = other.GetComponent<Atividade2>();
        if (vidaInimigo != null)
        {
            vidaInimigo.ReceberDano(dano);
            Destroy(gameObject);
        }
    }
}
