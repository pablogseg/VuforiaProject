using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class catapultaLogic : MonoBehaviour {
    public float minDistance = 0f;
    public GameObject enabledCatapult;
    public GameObject disabledCatapult;
    public GameObject target;
    public GameObject placeholderBall;
    public Transform shotOrigin;
    public Transform hinge;
    public GameObject projectile;
    public Text shotsRemaining_text;
    public GameObject tooClose_Image;
    public GameObject gameOverMenu;

    public static catapultaLogic getInstance;

    public float powerScale;
    public float maxPower;

    GameObject proj;
    bool charging = false;
    bool release = false;
    bool canShoot = false;
    bool gameOver = false;
    float power = 0f;
    float finalPower = 0f;
    [SerializeField]
    float coolDown = 2.5f;
    int shotsRemaining = 5;
    int ballsRemaining;

    // Use this for initialization
    void Start() {
        getInstance = this;
        ballsRemaining = shotsRemaining;
        canShoot = true;
        gameOverMenu.SetActive(false);
        shotsRemaining_text.text = shotsRemaining.ToString();
        gameOver = false;
    }
    private void OnEnable()
    {
        charging = false;
        release = false;
        canShoot = true;
        power = 0f;
        finalPower = 0f;
        hinge.transform.localEulerAngles = new Vector3(22, 0, 0);
        placeholderBall.SetActive(true);
    }

    private void OnDisable()
    {
        OnEnable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.transform.position, transform.position) >= minDistance) //SI ESTAN A LA DISTANCIA MINIMA
        {
            enabledCatapult.SetActive(true);//Activate Good Catapult
            disabledCatapult.SetActive(false);//Deactivate Bad Catapult
            tooClose_Image.SetActive(false);//No warning UI error
        }
        else //NO ESTAN A LA DISTANCIA MINIMA
        {
            enabledCatapult.SetActive(false);//Deactivate Good Catapult
            OnEnable();
            disabledCatapult.SetActive(true);//Activate Bad Catapult
            if (!gameOver)
                tooClose_Image.SetActive(true); //UI Enabled --> Too close! Message

        }

        if (enabledCatapult.activeSelf)
        {
            if (Input.GetMouseButtonDown(0) && canShoot && !gameOver)
            {
                charging = true;
                power = 0f;
                release = false;
            }

            if (charging)
            {
                if (Input.GetMouseButton(0)) //Charging shot
                {
                    //charging up
                    power += Time.deltaTime * powerScale;
                    hinge.transform.localEulerAngles = new Vector3(Mathf.Lerp(22, 60, (power / maxPower)), 0f, 0f);

                }
                else if (!Input.GetMouseButton(0) || power > maxPower) //Shot fired
                {
                    release = true;
                    charging = false;
                    if (--shotsRemaining <= 0) {
                        canShoot = false;
                    }
                    else
                    {
                        StartCoroutine(CoolDownShoot());
                        finalPower = power;
                    }
                    shotsRemaining_text.text = shotsRemaining.ToString();

                }
            }

            if (release)
            {
                power -= Time.deltaTime * powerScale * maxPower;
                hinge.transform.localEulerAngles = hinge.transform.localEulerAngles = new Vector3(Mathf.Lerp(22, 60, (power / maxPower)), 0f, 0f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (enabledCatapult.activeSelf)
        {
            if (release)
            {
                if (hinge.localEulerAngles.x <= 22.5f)
                {
                    placeholderBall.SetActive(false);
                    proj = Instantiate(projectile);
                    proj.transform.position = shotOrigin.position;
                    proj.transform.rotation = shotOrigin.transform.rotation;
                    release = false;
                    proj.GetComponent<Rigidbody>().AddRelativeForce(finalPower * new Vector3(0, 0f, 1f), ForceMode.VelocityChange);
                }
            }
        }

    }

    IEnumerator CoolDownShoot()
    {
        canShoot = false;
        float t = coolDown;
        while (t > 0)
        {
            t -= Time.deltaTime;
            yield return null;
        }
        canShoot = true;
        placeholderBall.SetActive(true);
    }

    public void GameOver ()
    {
        ballsRemaining--;
        if(shotsRemaining <= 0 && ballsRemaining <= 0)
        {
            gameOverMenu.SetActive(true);
            tooClose_Image.SetActive(false);
            gameOver = true;
        }
    }
}
