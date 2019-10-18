using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using UnityStandardAssets.Characters.FirstPerson;



public class Randomizev2 : MonoBehaviour
{
    // Creating a HashSet of odd numbers 
    HashSet<int> levels = new HashSet<int>();
    private int numlevel = 24;
    private static bool created = false;
    public static int nextLevel = 0;
    string FILE_NAME = "position.txt";
    string OBJ_FILE_NAME = "objdistance.txt";
    public static Vector3 playerStart;
    public static int currentLevel;
    public static float startTime;
    public bool learnTrialsCompleted = false;
    public bool practiceTrialsCompleted = false;
    public int learnTrials = 1;
    public int practiceTrials = 1;
    public static bool OFT_COMPLETE = false;


    void Start()
    {

    }

    void LevelPick(){
        // Check to see if Learning trials have been completed, if not, 
        // select the next learning trial in sequential order.

        if (learnTrialsCompleted && practiceTrialsCompleted)
        {
            ParticipantLog.trialPhase = 3;
            // While levels completed do not reach max trials, randomly select without replacement
            // the next testing trial
            while (levels.Count != numlevel)
            {
                int randNum = UnityEngine.Random.Range(1, numlevel+1);
                //Debug.Log(randNum + " = generated");
                if (levels.Add(randNum))
                {
                    nextLevel = randNum + 8;
                    Debug.Log("Trial level: " + randNum + " picked");
                    Debug.Log(levels.Count);
                    if (levels.Count == numlevel)
                    {
                        OFT_COMPLETE = true;
                    }
                    break;
                }
            }
        } 
        else if (learnTrialsCompleted && !practiceTrialsCompleted)
        {
            nextLevel = practiceTrials + 4;
            Debug.Log("Practice Level: " + (nextLevel - 4) + " picked");
            practiceTrials += 1;

            if (practiceTrials == 5)
            {
                practiceTrialsCompleted = true;
            }
        }
        else
        {
            // Sequentially go through each of the four learning trials before beginning 
            // the testing phase
            nextLevel = learnTrials;
            Debug.Log("Learn level: " + nextLevel + " picked");
            learnTrials += 1;

            if (learnTrials == 5)
            {
                learnTrialsCompleted = true;
            }
        }
        
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

    // Update is called once per frame
    void Update()
    {

    }

    public void NextScene()
    {
        LevelPick();
        SceneManager.LoadScene(nextLevel);
        StreamWriter writelevel = File.AppendText(FILE_NAME);
        writelevel.WriteLine("level " + nextLevel + " loaded");
        writelevel.Close();
        StreamWriter writelevelobj = File.AppendText(OBJ_FILE_NAME);
        writelevelobj.WriteLine("level " + nextLevel + " loaded");
        writelevelobj.Close();
        currentLevel = nextLevel;
        startTime = Time.time;
        if (ParticipantLog.trialPhase != 3)
        {
            ParticipantLog.trialPhase = 1;
        }

               
        
    }

}
