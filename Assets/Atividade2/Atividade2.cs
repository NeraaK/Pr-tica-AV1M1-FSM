using UnityEngine;
using System.Collections.Generic;

public class Atividade2 : MonoBehaviour
{
    public enum Guardiao
    {
        PATRULHAR,
        PERSEGUIR,
        ALERTA,
        ATAQUEBASE,
        ATAQUEPROJETIL,
        CHAMAOSAMIGOS,
        RECUAR,
        MORRER
    }
    public Guardiao estadoAtual;

    [Header("Configurações")]
    public Transform t_Guardiao;
    public Transform t_Player;
    public float velocidade = 2f;

    public float vidaAtual;
    public float vidaMaxima;
    public GameObject projetil;
    public GameObject[] amigos;
    public Transform t_Recover;
    [SerializeField] private List<Vector3> pontosDePatrulha;

    const string IDLE = "Idle";
    const string WALK = "Walk";
    const string COVARDE = "Covarde";
    const string ATAQUE = "AtaqueNormal";
    const string ATAQUEDISTANCIA = "AtaqueDistancia";
    const string MORRER = "Morrer";
    public Animator anim;
    private int _index;
    private float timer;
    private float coolDown = 1.5f;
    public Transform spawnPoint;

    void Start()
    {
        _index = 0;
        estadoAtual = Guardiao.PATRULHAR;
        vidaAtual = vidaMaxima;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            vidaAtual = 40f;
        }
        switch (estadoAtual)
        {
            case Guardiao.PATRULHAR:
                Patrulhar();
                break;

            case Guardiao.PERSEGUIR:
                Perseguir();
                break;

            case Guardiao.ALERTA:
                Atencao();
                break;

            case Guardiao.ATAQUEBASE:
                Ataque1();
                break;

            case Guardiao.ATAQUEPROJETIL:
                AtaqueDistancia();
                break;

            case Guardiao.CHAMAOSAMIGOS:
                ChamarAmigos();
                break;

            case Guardiao.RECUAR:
                Recuar();
                break;

            case Guardiao.MORRER:
                Morrer();
                break;
        }
    }

    public void Patrulhar()
    {
        Debug.Log("Patrulhando");
        if (anim != null) anim.Play(WALK);

        t_Guardiao.position = Vector3.MoveTowards(t_Guardiao.position, pontosDePatrulha[_index], velocidade * Time.deltaTime);

        if (Vector3.Distance(t_Guardiao.position, pontosDePatrulha[_index]) < 2f)
        {
            _index = (_index + 1) % pontosDePatrulha.Count;
            t_Guardiao.LookAt(pontosDePatrulha[_index]);
        }

        if (Vector3.Distance(t_Guardiao.position, t_Player.position) <= 20f)
        {
            estadoAtual = Guardiao.ALERTA;
        }
        else if(vidaAtual <= 0)
        {
            estadoAtual = Guardiao.MORRER;
        }

    }

    public void Atencao()
    {
        Debug.Log("Atençao");
        anim.Play(IDLE);

        t_Guardiao.LookAt(t_Player.position);

        if (Vector3.Distance(t_Guardiao.position, t_Player.position) <= 10f)
        {
            estadoAtual = Guardiao.PERSEGUIR;
        }
        else if (vidaAtual <= 0)
        {
            estadoAtual = Guardiao.MORRER;
        }
    }

    public void Perseguir()
    {
        Debug.Log("Perseguindo");
        anim.Play(WALK);
        t_Guardiao.LookAt(t_Player.position);

        t_Guardiao.position = Vector3.MoveTowards(t_Guardiao.position, t_Player.position, velocidade * Time.deltaTime);

        if (Vector3.Distance(t_Guardiao.position, t_Player.position) <= 3f)
        {
            estadoAtual = Guardiao.ATAQUEBASE;
        }
        else if (vidaAtual <= 0)
        {
            estadoAtual = Guardiao.MORRER;
        }
    }

    public void Ataque1()
    {
        Debug.Log("AtaqueBase");

        anim.Play(ATAQUE);
        t_Guardiao.LookAt(t_Player.position);

        if (Vector3.Distance(t_Guardiao.position, t_Player.position) >= 3f && Vector3.Distance(t_Guardiao.position, t_Player.position) <= 10f)
        {
            estadoAtual = Guardiao.ATAQUEPROJETIL;
        }
        else if (vidaAtual <= 0)
        {
            estadoAtual = Guardiao.MORRER;
        }
        if (vidaAtual <= 40f)
        {
            ChamarAmigos();
        }
        if (vidaAtual <= 20)
        {
            Recuar();
        }
    }

    public void AtaqueDistancia()
    {
        Debug.Log("Projetil");
        t_Guardiao.LookAt(t_Player);
        timer += Time.deltaTime;

        if (timer >= coolDown)
        {
            GameObject novoProjetil = Instantiate(projetil, spawnPoint.position, spawnPoint.rotation);
            Projetil2 scripProjetil = novoProjetil.GetComponent<Projetil2>();

            anim.Play(ATAQUEDISTANCIA);

            if (scripProjetil != null)
            {
                
                scripProjetil.AlvoGuardiao(t_Player);
            }

            Destroy(novoProjetil, 5f);
            timer = 0f;
        }
        if (vidaAtual <= 40f)
        {
            ChamarAmigos();
        }

        if (Vector3.Distance(t_Guardiao.position, t_Player.position) <= 3f)
        {
            estadoAtual = Guardiao.ATAQUEBASE;
        }
        else if (Vector3.Distance(t_Guardiao.position, t_Player.position) >= 10f)
        {
            estadoAtual = Guardiao.PERSEGUIR;
        }
        else if (vidaAtual <= 0)
        {
            estadoAtual = Guardiao.MORRER;
        }
        else if (vidaAtual <= 20)
        {
            Recuar();
        }
    }

    public void ChamarAmigos()
    {
        Debug.Log("Envocando");
        anim.Play(COVARDE);
        estadoAtual = Guardiao.PERSEGUIR;
        if (vidaAtual <= 0)
        {
            estadoAtual = Guardiao.MORRER;
        }
        if(vidaAtual <= 20)
        {
            Recuar();
        }
       
    }

    public void Recuar()
    {
        Debug.Log("Recuando");
        anim.Play(COVARDE);

        t_Guardiao.position = Vector3.MoveTowards(t_Guardiao.position, t_Recover.position, velocidade * Time.deltaTime);

        if (Vector3.Distance(t_Guardiao.position, t_Recover.position) < 1f)
        {
            vidaAtual = vidaMaxima;
            estadoAtual = Guardiao.PERSEGUIR;
        }
        else if (vidaAtual <= 0)
        {
            estadoAtual = Guardiao.MORRER;
        }
    }

    public void Morrer()
    {
        anim.Play(MORRER);
        Debug.Log("Morreu");
    }

    public void ReceberDano(float dano)
    {
        vidaAtual -= dano;

        if (vidaAtual <= 0)
        {
            estadoAtual = Guardiao.MORRER;
        }
        else if (vidaAtual <= 50f)
        {
            estadoAtual = Guardiao.RECUAR;
        }
    }
}
