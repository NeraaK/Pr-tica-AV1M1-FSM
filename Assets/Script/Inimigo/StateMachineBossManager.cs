using UnityEngine;


public class StateMachineBossManager : MonoBehaviour
{
    BossBaseState estadoAtual;
    public Perseguir estadoPerseguir = new Perseguir();
    public AtaqueNormal estadoAtaqueNormal = new AtaqueNormal();
    public AtaqueProjetil estadoAtaqueProjetil = new AtaqueProjetil();
    public AtaqueEspecial estadoAtaqueEspecial = new AtaqueEspecial();
    public Curar estadocurar = new Curar();
    public Morrer estadoMorrer = new Morrer();
    public Fugir estadoFugir = new Fugir();
    public Idle estadoIdle = new Idle();
    public Dano estadoDano = new Dano();
    

    [Header("Atributos")]
    public int velocidade;
    public float vidaMaxima;
    public float vidaAtual;
    public int danoAtaqueBase;
    public int danoAtaqueProjetil;
    public int danoAtaqueEspecial;
    public Transform t_Boss;
    public Transform t_Player;
    public Transform spawnPointProjetil;
    public GameObject projetilPrefab;
    public Transform t_Alvo;
    public Transform t_Recover;
    public float taxaDeRegeneracao = 10f;
    public float tempoRegeneracao;
    public float intervaloRegeneracao = 1f;
    
    public Animator anim;

    public const string IDLE = "Idle";
    public const string WALK = "Walk";
    public const string RUN = "Run";
    public const string ATAQUENORMAL = "AtaqueBase";
    public const string ATAQUEDISTANCIA = "AtaqueDistancia";
    public const string MORRER = "Morrer";
    public const string ESPECIAL = "Especial";

    void Start()
    {
        vidaAtual = vidaMaxima;
        estadoAtual = estadoIdle;
        estadoAtual.EnterState(this);
    }
    void Update()
    {
        estadoAtual.UpdateState(this);
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        estadoAtual.OnCollisionEnter(this, collision);
    }
    
    public void TrocarEstado(BossBaseState estado)
    {
        if(estadoAtual != null)
        {
            estadoAtual.OnExit(this);

            estadoAtual = estado;

            estadoAtual.EnterState(this);
        }
    }
    public GameObject Instanciar(GameObject objeto, Vector3 posicao, Quaternion rotacao)
    {
        GameObject novoObjeto = Instantiate(objeto, posicao, rotacao);
        return novoObjeto;
    }
    public void ReceberDano(float dano)
    {
        vidaAtual -= dano;

       if(vidaAtual <= 0)
        {
            Debug.Log("Morri");
           // TrocarEstado(estadoMorrer);
        }
        
    }
}
