using UnityEngine;

public class HolyGrenade : MonoBehaviour
{

    public GameObject explosion;
    public float speed;

    private Vector3 direction;
    
    public float MaxLifeTime = 10f;

    private GameMaster gameMaster;
    

    public GameObject DecalParticles;

    public GameObject explosionInstance;

    private bool hasBeenThrown = false;

    public float Damage;

    public AudioClip HitSound;

    public void Start(){
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        GetComponent<BoxCollider>().enabled = false;
    }

    public void yeet(Vector3 direction){
        GetComponent<BoxCollider>().enabled = true;
        Rigidbody rigidbody =  gameObject.AddComponent<Rigidbody>(); 
        rigidbody.isKinematic = false;
        rigidbody.mass = 0.1f;
        rigidbody.AddForce(direction.normalized * speed, ForceMode.Impulse);
        hasBeenThrown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasBeenThrown){
            return;
        }
        MaxLifeTime -= Time.deltaTime;
        if (MaxLifeTime <= 0){
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision){
        if (!hasBeenThrown){
            return;
        }
        if (collision.gameObject.name == "Player"){
            return;
        }

        if (GetComponent<AudioSource>() != null){
            return;
        }

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = HitSound;

        audioSource.Play();

        GameObject c = Instantiate(DecalParticles, transform.position, Quaternion.identity);
        Destroy(c, 1);

        if (collision.gameObject.tag == "Boss"){
                BossBehaviour boss = FindObjectOfType<BossBehaviour>();

                if(boss != null){
                    boss.Damage(Damage);
                    explode();
                }
        }
    }

    void explode(){
        Debug.Log("BOOOOOM");
        explosionInstance = Instantiate(explosion,transform.position, Quaternion.identity);
        Invoke("killExplosion",2.0f);
    }
    void killExplosion(){
        Destroy(explosionInstance);
        Destroy(gameObject);
    }
}
