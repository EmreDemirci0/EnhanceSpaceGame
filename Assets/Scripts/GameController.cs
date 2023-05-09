using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class GameController : MonoBehaviour
{//68 61
    [SerializeField] public static bool isShoot = false;
    /**/
    [SerializeField]float itemsRotationSpeed;
    public bool isLeft = false;
    /***//**//***/
    bool isFail;
    public bool deneme;

    float timer = 0;
    [Tooltip("The first two red,The last two green")]
    [SerializeField] List<GameObject> itemPrefabs;
    [SerializeField] GameObject itemPoint;
    [SerializeField] Transform WhichItemRedPoint, WhichItemGreenPoint;
    [SerializeField] Transform first, redFinishPos, greenFinishPos;
    [HideInInspector] public GameObject whichRedObject;
    [HideInInspector] public GameObject whichGreenObject;
    public GameObject HolderMain;
    GameObject item;
    bool canPressSpaceLeft = true, canPressSpaceRight=true;
    [SerializeField] List<GameObject> AllItems;

    AudioSource source;
    [SerializeField] AudioClip throwObjectClip;//deðiþcek
    void Start()
    {
        source = GameObject.FindGameObjectWithTag("SoundController").GetComponent<AudioSource>();
        source.clip = throwObjectClip;
        isShoot = false;
        int RedRandom = Random.Range(0, itemPrefabs.Count/2); 
        int BlueRandom = Random.Range(itemPrefabs.Count / 2, itemPrefabs.Count);
        whichRedObject = Instantiate(itemPrefabs[RedRandom], WhichItemRedPoint.position, Quaternion.Euler(new Vector3(itemPrefabs[RedRandom].transform.rotation.eulerAngles.x, itemPrefabs[RedRandom].transform.rotation.x, itemPrefabs[RedRandom].transform.rotation.x)));
        whichGreenObject = Instantiate(itemPrefabs[BlueRandom], WhichItemGreenPoint.position, Quaternion.Euler(new Vector3(itemPrefabs[BlueRandom].transform.rotation.eulerAngles.x, itemPrefabs[BlueRandom].transform.rotation.x, itemPrefabs[BlueRandom].transform.rotation.x)));
        
        //whichRedObject.transform.SetParent(HolderMain.transform); 
        //whichGreenObject.transform.SetParent(HolderMain.transform);

        whichRedObject.name = itemPrefabs[RedRandom].name;
        whichGreenObject.name = itemPrefabs[BlueRandom].name;

        AllItems.Add(whichRedObject);
        AllItems.Add(whichGreenObject);
        int random = Random.Range(2,itemPrefabs.Count);
        int random2 = Random.Range(2,itemPrefabs.Count);
        if (random==random2)
        {
            random = Random.Range(2, itemPrefabs.Count);
            random2 = Random.Range(2, itemPrefabs.Count);
        }
        AllItems.Add(itemPrefabs[random]);
        AllItems.Add(itemPrefabs[random2]);


        //turuncularý ekle


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        //whichGreenObject.transform.Rotate(new Vector3(1, 1, 1) * itemsRotationSpeed * Time.deltaTime); // objeyi x, y ve z eksenlerinde sonsuz olarak döndürmek için
        //whichRedObject.transform.Rotate(new Vector3(1, 1, 1) * itemsRotationSpeed * Time.deltaTime); // objeyi x, y ve z eksenlerinde sonsuz olarak döndürmek için
        deneme = isShoot;
        //Debug.Log("isShoot:"+isShoot);
        timer += Time.deltaTime;//holderý uzaklaþtýrýrsak alttaki 1.5i düzelmeliyiz
        if (timer > 1f)
        {
            first.transform.localPosition = new Vector3(Random.Range(-20,-2), Random.Range(9, 15), Random.Range(0, 3)/*12.8f,9f,-11f*/);
            //Debug.Log(first.transform.position);
            int randomPrefab =  Random.Range(0, AllItems.Count);
            item = Instantiate(AllItems[randomPrefab], itemPoint.transform.position,Quaternion.Euler( new Vector3(AllItems[randomPrefab].transform.rotation.eulerAngles.x, AllItems[randomPrefab].transform.rotation.x, AllItems[randomPrefab].transform.rotation.x)));
            item.name = AllItems[randomPrefab].name;
            item.GetComponent<Rigidbody>().velocity = -1 * Vector3.left * 40f;
            Destroy(item,8f);
            //item.name = itemPrefabs[randomPrefab].name;

            timer = 0;
        }
        if (isShoot)
        {//vurabilir

            //Debug.Log("vurabilir: "+item.name+" "+whichRedObject.name);
            //Debug.Log("0:"+Input.GetMouseButtonDown(0) + "1:"+item.tag );
            if (Input.GetMouseButtonDown(0) && canPressSpaceLeft/*&& item.tag=="item"*/)
            {
                source.Play();
                canPressSpaceLeft = false; // Space tuþuna basýldýðýnda butona basma izni kapatýlýyor
                Invoke("EnableSpacePressLeft", 1f); // 1 saniye sonra butona basma izni açýlacak

                //Debug.Log("basti");
                var sequence = DOTween.Sequence();
                sequence.Append(item.transform.DOMove(first.transform.position,.3f));
                sequence.Append(item.transform.DOMove(redFinishPos.transform.position, .3f));
                //item.tag = "Untagged";
                item.GetComponent<TrailRenderer>().enabled = true;
                isShoot = false;
                isLeft = true;
                //Debug.Log(" :"+item.name +whichRedObject);
                if ((item.name == whichRedObject.name))
                {   //vuruþ baþarýlý
                    isFail = false;
                }
                else
                {
                    item.GetComponent<Rigidbody>().useGravity = true;
                    //item.GetComponent<Collider>().isTrigger = false;
                    isFail = true;
                }


            }
            if (Input.GetMouseButtonDown(1)&& canPressSpaceRight)
            {
                source.Play();
                print("sað");
                canPressSpaceRight= false; // Space tuþuna basýldýðýnda butona basma izni kapatýlýyor
                Invoke("EnableSpacePressRight", 1f); // 1 saniye sonra butona basma izni açýlacak

                var sequence = DOTween.Sequence();
                sequence.Append(item.transform.DOMove(first.transform.position, .3f));
                sequence.Append(item.transform.DOMove(greenFinishPos.transform.position, .3f));
                item.GetComponent<TrailRenderer>().enabled = true;
                isShoot = false;
                isLeft = false;
                if ((item.name == whichGreenObject.name))
                {   //vuruþ baþarýlý
                    isFail = false;
                }
                else
                {

                    item.GetComponent<Rigidbody>().useGravity = true;
                    //item.GetComponent<Collider>().isTrigger = false;
                    isFail = true;
                }
            }

          
        }
    }
    void EnableSpacePressLeft()
    {
        canPressSpaceLeft = true; // 1 saniye sonra butona basma izni açýlýyor
    }
    void EnableSpacePressRight()
    {
        canPressSpaceRight = true; // 1 saniye sonra butona basma izni açýlýyor
    }
}
