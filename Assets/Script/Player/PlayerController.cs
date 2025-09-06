using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    const string IDLE = "Idle";
    const string WALK = "Walk";
    const string RUN = "Run";
    const string ATAQUE = "AtaqueNormal";

    CustomActions input;
    NavMeshAgent agent;
    Animator animator;

    [Header("Configurações")]
    [SerializeField] LayerMask AreaDeClique;
    [SerializeField] GameObject efeitoClique;
    [SerializeField] float speedWalk = 3.5f;
    [SerializeField] float speedRun = 6f;

    [Header("Ataque")]
    [SerializeField] LayerMask AreaDeAtaque;
    [SerializeField] float DistanciaDeAtaque = 5f;
    Transform target;
    public GameObject magia;
    public Transform spawnMagia;
    

    private Transform alvo; 
    

    bool Run = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        input = new CustomActions();
        InputSystem();
    }

    void InputSystem()
    {
        input.Main.Move.performed += ctx => Clicar();
        input.Main.Run.started += ctx => Run = true;
        input.Main.Run.canceled += ctx => Run = false;
    }

    void OnEnable() => input.Enable();
    void OnDisable() => input.Disable();

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            float distanciaAtual = Vector3.Distance(transform.position, target.position);

            if (distanciaAtual <= DistanciaDeAtaque)
            {
                agent.isStopped = true;
                
                animator.Play(ATAQUE);
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            agent.isStopped = false;
        }

        if (Run)
        {
            agent.speed = speedRun;
        }
        else
        {
            agent.speed = speedWalk;
        }

        AnimPlay();
    }

    void Clicar()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);      

        if (Physics.Raycast(ray, out hit, 100, AreaDeAtaque))
        {
            target = hit.transform;
            

            
            InstanciarMagia();

            if (efeitoClique != null)
            {
                GameObject efeitoNovo = Instantiate(efeitoClique, hit.point + Vector3.up * 0.1f, Quaternion.identity);
                Destroy(efeitoNovo, 2f);
            }
        }
        else if (Physics.Raycast(ray, out hit, 100, AreaDeClique))
        {
            target = null;
            agent.destination = hit.point;

            if (efeitoClique != null)
            {
                GameObject efeitoNovo = Instantiate(efeitoClique, hit.point + Vector3.up * 0.1f, Quaternion.identity);
                Destroy(efeitoNovo, 2f);
            }
        }
    }
    

    void InstanciarMagia()
    {
        if (magia != null && spawnMagia != null)
        {
            GameObject novoProjetil = Instantiate(magia, spawnMagia.position, spawnMagia.rotation);

            ProjetilMagia scriptProjetil = novoProjetil.GetComponent<ProjetilMagia>();
            if (scriptProjetil != null && target != null)
            {
                scriptProjetil.Alvo(target);
            }
        }
    }

    void AnimPlay()
    {
        if (agent.velocity.magnitude < 0.1f || (target != null && agent.isStopped))
        {
            animator.Play(IDLE);
        }
        else
        {
            if (Run)
                animator.Play(RUN);
            else
                animator.Play(WALK);
        }
    }
}