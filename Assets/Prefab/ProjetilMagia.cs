using UnityEngine;

public class ProjetilMagia : MonoBehaviour
{
    public float velocidade = 15f;
    public float dano = 20f;
    private Transform alvo;

    public void Alvo(Transform novoAlvo)
    {
        alvo = novoAlvo;
    }

    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, alvo.position, velocidade * Time.deltaTime);
        if (Vector3.Distance(transform.position, alvo.position) < 0.1)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
           
            StateMachineBossManager vidaInimigo = other.GetComponent<StateMachineBossManager>();

            if(vidaInimigo != null)
            {
                vidaInimigo.ReceberDano(dano);
                
               
            }
           
        
    }
}