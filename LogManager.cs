using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using System.IO;


public class LogManager : MonoBehaviour
{
    string FILE_NAME = "test";
    string OBJ_FILE_NAME = "test";
    private GameObject chair, sball, bucket, cone, object31, object32;
    public Vector3 playerPos;
    public Quaternion playerRot;
    private FirstPersonController fps;
    private static bool created = false;
    public GameObject[] targetsWithTag;
    public string target;
    private GameObject targetObj;
    public Vector3 playerStart;


    // Start is called before the first frame update
    void Start()
    {
        // Call the CollectPositionData() Method Once every 1/10th of a second (10Hz)
        InvokeRepeating("CollectPositionData", 1.0f, 0.1f);

        
    }

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }

    void CollectPositionData()
    {

        // Every 10 Hz after start, grab Variables: 
        // pos_x, pos_z, rot_x, rot_y, rot_z, rot_w, time, target_obj, trial_level, delta_target, delta_start

        // Position variables X and Z. Y is not needed as height does not change throughout experiment.
        float pos_x = playerPos.x, pos_z = playerPos.z;
        // Rotation around [variable] axis. X, Y, Z is a vector. W is a scalar of rotation around that vector.
        float rot_x = playerRot.x, rot_y = playerRot.y, rot_z = playerRot.z, rot_w = playerRot.w;
        // Current Trial Level: if new trial level, update variable as well as start position **** PROBLEM BUG: SPAWNPOShg


        

        // Distance from player to target object, and distance from player spawn to current player location
        targetObj = GameObject.Find(target);


        float deltaTarget = Vector3.Distance(targetObj.transform.position, fps.transform.position);
        float deltaStart = Vector3.Distance(GameObject.FindWithTag("spawn").transform.position, fps.transform.position);


        // Initiate writing [particpant #]_position.txt, if no data is present, write a header line containing variable names
        StreamWriter sw = File.AppendText(FILE_NAME);
        if (new FileInfo(FILE_NAME).Length == 0)
        {
            sw.WriteLine("pos_x, pos_z, rot_x, rot_y, rot_z, rot_w, time, target_obj, trial_level, delta_target, delta_start");
        }
        
        sw.WriteLine(pos_x + "," + pos_z + "," + rot_x + "," + rot_y + "," + rot_z + "," + rot_w + "," + Time.time
            + "," + target + "," + SceneManager.GetActiveScene().name + "," + deltaTarget + "," + deltaStart);
        sw.Close();
    }

    void CollectResponseData()
    { 
        StreamWriter writedist = File.AppendText(OBJ_FILE_NAME);
        if (new FileInfo(OBJ_FILE_NAME).Length == 0)
        {
            writedist.WriteLine("target_obj, trial_level, start_x, start_z, end_x, end_z, end_rot_x, end_rot_y, end_rot_z, " +
                "end_rot_w, start_delta_target, end_delta_target, run_time, completion_time");
        }
        // Upon response event from participant, record data variables to #_responses:
        //      target_obj, trial_level, start_x, start_z, end_x, end_z, end_rot_x, end_rot_y, end_rot_z, end_rot_w, 
        //      start_delta_target, end_delta_target, run_time, completion_time

        writedist.WriteLine(target + "," + SceneManager.GetActiveScene().name + "," + 
            GameObject.FindWithTag("spawn").transform.position.x + "," +
            GameObject.FindWithTag("spawn").transform.position.z + "," + 
            fps.transform.position.x + "," + fps.transform.position.z + "," +
            fps.transform.rotation.x + "," + fps.transform.rotation.y + "," + 
            fps.transform.rotation.z + "," + fps.transform.rotation.w + "," +
            Vector3.Distance(GameObject.FindWithTag("spawn").transform.position, fps.transform.position) + "," +
            Vector3.Distance(GameObject.FindWithTag("target").transform.position, fps.transform.position) + "," +
            Time.time + "," + (Time.time - Randomizev2.startTime));
        writedist.Close();

    }

    // Update is called once per frame
    void Update()
    {

        fps = FindObjectOfType<FirstPersonController>();
        
        // With each update, obtain necessary data

        playerPos = fps.transform.position;
        playerRot = fps.transform.rotation;

        GameObject[] targetsWithTag = GameObject.FindGameObjectsWithTag("target");
        foreach (GameObject targ in targetsWithTag)
        {
            target = targ.name;
        }

        // Upon initial trial load, set file name to participant number entered on launch
        if (FILE_NAME == "test" && OBJ_FILE_NAME == "test")
        {
            FILE_NAME = ParticipantLog.user + "_position.csv";
            OBJ_FILE_NAME = ParticipantLog.user + "_objdistance.csv";
            Debug.Log("FN: " + FILE_NAME);
            
        }


        //if key is pressed and the folding chair exists in the scene
        if (Input.GetKeyDown("1") && GameObject.Find("chFolding.A_LOD0") != null)
        {
            chair = GameObject.Find("chFolding.A_LOD0");
            fps.m_WalkSpeed = 0f;
            float distance = Vector3.Distance(chair.transform.position, fps.transform.position);
            MeshRenderer m = chair.GetComponent<MeshRenderer>();
            m.enabled = true;
            CollectResponseData();
        }

        //if key is pressed and the Soccer Ball exists in the scene
        if (Input.GetKeyDown("1") && GameObject.Find("Soccer Ball") != null)
        {
            sball = GameObject.Find("Soccer Ball");
            fps.m_WalkSpeed = 0f;
            float distance = Vector3.Distance(sball.transform.position, fps.transform.position);
            MeshRenderer m = sball.GetComponent<MeshRenderer>();
            m.enabled = true;
            CollectResponseData();
           
        }

        //if key is pressed and the Bucket exists in the scene
        if (Input.GetKeyDown("1") && GameObject.Find("Bucket_clean") != null)
        {
            bucket = GameObject.Find("Bucket_clean");
            object31 = GameObject.Find("bucket_low");
            object32 = GameObject.Find("rings_low");
            fps.m_WalkSpeed = 0f;
            float distance = Vector3.Distance(bucket.transform.position, fps.transform.position);
            
            MeshRenderer n = object31.GetComponent<MeshRenderer>();
            MeshRenderer o = object32.GetComponent<MeshRenderer>();
          
            n.enabled = true;
            o.enabled = true;
            CollectResponseData();

        }

        //if key is pressed and the Cone exists in the scene
        if (Input.GetKeyDown("1") && GameObject.Find("cone_clean") != null)
        {
            cone = GameObject.Find("cone_clean");
            fps.m_WalkSpeed = 0f;
            float distance = Vector3.Distance(cone.transform.position, fps.transform.position);
            MeshRenderer m = cone.GetComponent<MeshRenderer>();
            m.enabled = true;
            CollectResponseData();

        }

    }
}

