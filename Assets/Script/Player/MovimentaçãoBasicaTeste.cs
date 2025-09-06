using UnityEngine;

public class MovimentaçãoBasicaTeste : MonoBehaviour
{
   
    public Rigidbody rb;
    public int velocidade = 5;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float vertival = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 movimento = new Vector3(horizontal, 0, vertival);

       transform.Translate(movimento * velocidade * Time.deltaTime);
    }
}
