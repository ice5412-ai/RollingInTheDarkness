using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class WinCondition : MonoBehaviour
{
    [SerializeField] GameObject Player = null;
    [SerializeField] FollowLight followLight = null;
    [SerializeField] GravityController gravityController = null;
    public GameObject SpawnPoint { get; set; }
    [SerializeField] ParticleSystem _ParticleSystem = null;
    [SerializeField] MeshRenderer _MeshRenderer = null;
    [SerializeField] Collider _Collider = null;
    [SerializeField] GameObject _onDestroyParticle = null;
    [SerializeField] Text StatusText = null;
    [SerializeField] GameObject MainLight = null;
    Light mainLight;
    [SerializeField] ForwardTimer forwardTimer = null;
    private float _Scale = .5f;
    public int remainActivePlate { get; set; } = 0;
    private float cooldown = 5;
    public int switchingText { get; set; } = 0;
    public float textCountdown = 6;
    public bool Winning { get; set; } = false;
    [SerializeField] PostProcessVolume volume = null;
    private ColorGrading colorGrading;
    private Coroutine spawn = null;
    private AudioSource audioSource;
    private Light Childlight;
    public bool hideInterface;
    public Text[] InterfaceToHide;
    public Text DestroyedCountText;
    public int destroyedCount { get; set; }
    private void Start()
    {
        destroyedCount = 0;
        DestroyedCountText.text = destroyedCount.ToString();
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = false;
        Childlight = GetComponentInChildren<Light>();
        Childlight.enabled = false;
        SpawnPoint = new GameObject();
        SpawnPoint.transform.position = Player.transform.position;
        Physics.gravity = new Vector3(0, -9.8f, 0);
        volume = GameObject.FindObjectOfType<PostProcessVolume>();
        volume.profile.TryGetSettings(out colorGrading);
        colorGrading.saturation.overrideState = true;
        colorGrading.saturation.value = -100;
        _MeshRenderer.enabled = false;
        _Collider.enabled = false;
        StatusText.text = "Hello...(Again?)";
        StatusText.color = Color.white;
        StatusText.enabled = true;
        spawn = StartCoroutine(Spawn());
        mainLight = MainLight.GetComponent<Light>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (switchingText <= 9)
            {
                cooldown = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            hideInterface = !hideInterface;
            foreach (Text hif in InterfaceToHide)
            {
                hif.enabled = !hideInterface;
            }
        }

        switch (switchingText)
        {
            case 0:
                {
                    cooldown -= Time.deltaTime;
                    if (cooldown < 0)
                    {
                        cooldown = 7;
                        StatusText.text = "Press A/S to Rotate the gravity clockwise/counter-clockwise.";
                        switchingText = 1;
                    }
                    break;
                }
            case 1:
                {
                    cooldown -= Time.deltaTime;
                    if (cooldown < 0)
                    {
                        cooldown = 5;
                        StatusText.text = "Press Spacebar to toggle view.";
                        switchingText = 2;
                    }
                    break;
                }
            case 2:
                {
                    cooldown -= Time.deltaTime;
                    if (cooldown < 0)
                    {
                        cooldown = 7;
                        StatusText.text = "On the top view you will see there are many crytals around the map.";
                        switchingText = 3;
                    }
                    break;
                }
            case 3:
                {
                    cooldown -= Time.deltaTime;
                    if (cooldown < 0)
                    {
                        cooldown = 8;
                        StatusText.text = "Your objective is to collect them all and then return to the center to collect the pure crystal.";
                        switchingText = 4;
                    }
                    break;
                }
            case 4:
                {
                    cooldown -= Time.deltaTime;
                    if (cooldown < 0)
                    {
                        cooldown = 7;
                        StatusText.text = "Caution, If you touch the blackhole you will die and lose.";
                        switchingText = 5;
                    }
                    break;
                }
            case 5:
                {
                    cooldown -= Time.deltaTime;
                    if (cooldown < 0)
                    {
                        cooldown = 6;
                        StatusText.text = "You can press R to Respawn at last checkpoint immediately.";
                        switchingText = 6;
                    }
                    break;
                }
            case 6:
                {
                    cooldown -= Time.deltaTime;
                    if (cooldown < 0)
                    {
                        cooldown = 5;
                        StatusText.text = "That's basic. Now go and have fun.";
                        switchingText = 7;
                    }
                    break;
                }
            case 7:
                {
                    cooldown -= Time.deltaTime;
                    if (cooldown < 0)
                    {
                        switchingText = 8;
                        StatusText.enabled = false;
                    }
                    break;
                }
            case 8:
                {
                    DeactiveText(textCountdown);
                    break;
                }
            case 9: { break; }
        }

        if (remainActivePlate <= 0)
        {
            mainLight.intensity = 1f;
            mainLight.range = 100f;
            _ParticleSystem.Play();
            _MeshRenderer.enabled = true;
            _Collider.enabled = true;
            audioSource.enabled = true;
            Childlight.enabled = true;
        }
        if (Winning)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Invoke("ReloadEntireScene", 1);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Winning = true;
            MainLight.GetComponent<Light>().intensity = 2f;
            _ParticleSystem.Stop();
            Instantiate(_onDestroyParticle, transform.position, Quaternion.identity);
            if (!hideInterface)
            {
                StatusText.enabled = true;
            }
            StatusText.color = Color.white;
            StatusText.text = "Congratulations, You win!\nPress Enter to play again!";
            forwardTimer.IsTimerUpdate = false;
            StartCoroutine(EndPlate());
        }
    }
    private IEnumerator EndPlate()
    {
        while (true)
        {
            yield return Time.deltaTime;
            if (_Scale > 0)
            {
                _Scale -= Time.deltaTime;
                if (_Scale <= 0)
                {
                    _Scale = 0;
                }
                this.gameObject.transform.localScale = new Vector3(_Scale, _Scale, _Scale);
            }
            else
            {
                _Scale = 0;
                if (GetComponent<ParticleSystem>().isStopped)
                {
                    Destroy(gameObject, 6);
                }
            }
        }
    }
    public void ReloadSceneWithDelay(float delay)
    {
        destroyedCount++;
        DestroyedCountText.text = destroyedCount.ToString();
        Invoke("ReloadScene", delay);
    }
    public void ReloadScene()
    {
        Player.SetActive(true);
        Player.transform.position = SpawnPoint.transform.position;
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gravityController._player = Player;
        followLight._Player = Player;
    }
    public void ReloadEntireScene()
    {
        SceneManager.LoadScene("Stage1");
    }
    public void DeactiveText(float time)
    {
        textCountdown = time;
        textCountdown -= Time.deltaTime;
        if (textCountdown <= 0)
        {
            if (!hideInterface)
            {
                StatusText.enabled = false;
            }
            switchingText = 9;
        }
    }
    public IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (colorGrading.saturation.value < 0)
            {
                colorGrading.saturation.value += Time.deltaTime * 4000f;
            }
            else
            {
                colorGrading.saturation.value = 0;
                StopCoroutine(spawn);
            }
        }
    }
}
