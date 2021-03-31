using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{

    public string toScene; //name of scene to load into
    public bool teleport = false; //flag to teleport to a teleport pad within current scene
    public string layerName;
    public GameObject alert;
    public string findPad;
    Collider2D myCollider; //door collider
    private bool byDoor = false; //detect if player is near door
    GameObject doorAlert; //UI element when near door
    GameObject player; //player gameObject
    GameObject panel; //transition animation panel
    GameObject teleportPad; //pad to teleport to 
    Animator animator; //animator to control transition effect 
    CameraManager cameraManager; 

    void Start()
    {
        byDoor = false;
        myCollider = GetComponent<Collider2D>();
        doorAlert = GameObject.Find("DoorAlert");
        if (alert.activeSelf)
        {
            alert.SetActive(false);
        }
        
        player = GameObject.FindGameObjectWithTag("Player");
        panel = GameObject.Find("TransitionPanel");
        animator = panel.transform.GetComponent<Animator>();
        teleportPad = GameObject.Find(findPad);
        cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //If next to door and e is hit, either go to new scene or teleport within coordinates
        if(byDoor && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TransitionRoutine());
            StartCoroutine(WaitReset());
        }
    }

    private IEnumerator TransitionRoutine()
    {
        if (!teleport)
        {
            animator.SetTrigger("startTrans");
            yield return new WaitForSeconds(1f);
            animator.SetTrigger("transOut");
            SceneManager.LoadScene(toScene);
        }
        else
        {
            animator.SetTrigger("startTrans");
            yield return new WaitForSeconds(1f);
            animator.SetTrigger("transOut");
            if (layerName != null)
            {
                cameraManager.SwitchTownScene(layerName);
            }
            player.transform.position = teleportPad.transform.position;
            
        }

    }

    private IEnumerator WaitReset()
    {
        yield return new WaitForSeconds(3f);
        animator.Rebind();
        animator.Update(3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        byDoor = true;
        alert.SetActive(true);
        Debug.Log("Entering Trigger!!");
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        byDoor = false;
        alert.SetActive(false);
        Debug.Log("Exiting Trigger!!");
    }
}
