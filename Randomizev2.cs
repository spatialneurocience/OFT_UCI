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
    public int learnTrials = 1;


    void Start()
    {

    }

    void LevelPick(){
        // Check to see if Learning trials have been completed, if not, 
        // select the next learning trial in sequential order.

        if (learnTrialsCompleted)
        {
            // While levels completed do not reach max trials, randomly select without replacement
            // the next testing trial
            while (levels.Count != numlevel)
            {
                int randNum = UnityEngine.Random.Range(1, numlevel+1);
                //Debug.Log(randNum + " = generated");
                if (levels.Add(randNum))
                {
                    nextLevel = randNum + 4;
                    Debug.Log("Trial level: " + randNum + " picked");
                    break;
                }
            }
        } else
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
        // "k" = move onto next scene
        if (Input.GetKeyDown(KeyCode.K))
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


                
            }
    }

    }
