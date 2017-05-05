using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomGenerator : MonoBehaviour {

    public string electronsPerShell = "0";
    public string atom = "ATOMNAME";
    public GameObject protonPrefab;
    public GameObject electronPrefab;
    private GameObject nucleus;

    public Vector3 position = new Vector3(0,0,0);
    public float electronSpeed = 0.05f;
    private Vector3 scale = new Vector3(1, 1, 1);
    private Color color =new Vector4(0, 255, 244, 255);

    private int[] electronList = { 0 };// Start with length 1 electrons 0
    private int[] electronShellLimits = { 2, 8, 18, 32, 32, 18, 8 };
    private int totalElectrons = 0;
    private float radius;

    // Use this for initialization
    void Start () {
        string[] stringList = electronsPerShell.Split(',');
        electronList = new int[stringList.Length];

        //Now that we have instansiated the values we can begin parsing the data
        if (stringList.Length > 1)
        {
            for (int i = 0; i < stringList.Length; i++)
            {
                electronList[i] = int.Parse(stringList[i]);
                totalElectrons = totalElectrons + electronList[i];
            }
        }
        else//For dealing with single shell atoms
        {
            electronList[0] = int.Parse(stringList[0]);
            totalElectrons = electronList[0];
        }
        createNucleus();
        createShell();
    }
	
	// Update is called once per frame
	void Update () {

    }

    void createShell()
    {
        //radius is the distance from the nucleus
        radius = scale.x + ((scale.x / 2) * electronList.Length);
        //needs to know number shell it is and electrons that it will have.

        for(int shell = 0; shell < electronList.Length; shell++)
        {
            //we need to seperate the electrons so they are as far from each other as possible
            //Each ring will be 1.5 the nucleus radius away from the centre and then .5 from each other
            for(int electron = 0; electron < electronList[shell]; electron++)
            {
                //Now we create electron
                createElectron(radius, electron, (shell+1));
                
            }

        }
    }

    void createNucleus()
    {
        //This instansiates a nucleus and scales it based on the number of electrons it has.
        scale = new Vector3(totalElectrons, totalElectrons, totalElectrons);

        nucleus = Instantiate<GameObject>(protonPrefab);
        nucleus.transform.position = position;//Moving to predetermined position
        nucleus.transform.localScale = scale;
    }

    void createElectron(float rad, int num, int shell)
    {

        Renderer rend = electronPrefab.GetComponentInChildren<Renderer>();
        if (shell > electronShellLimits.Length || electronShellLimits[shell] < num)
        {
            //This electron should not exist! 
            //All electrons that should not be there are coloured to show this
            rend.material.color = Color.yellow;

            print("This is not a true Atom structure");
        }
        else
        {
            rend.material.color = color;
        }
        Vector3 startPoint = new Vector3((shell*rad), 0, 0);//This is the distance we start at.

        GameObject electron = Instantiate<GameObject>(electronPrefab);
        electron.transform.position = startPoint;
        

        //Now we update the values for boid
        Boid boid = electron.GetComponent<Boid>();
        boid.speed = electronSpeed * (rad*shell/2);//speed is based on distance to centre
        boid.source = position;//We tell the boid where our source object is
        boid.nucleus = nucleus;
        boid.instanciated = true;

        //Lets make a way of checking whether these electrons are allowed exist

    }
}
