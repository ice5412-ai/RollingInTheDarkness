using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GravityController : MonoBehaviour
{
    [SerializeField] public GameObject _player = null;
    [SerializeField] public GameObject _focusViewPos = null;
    [SerializeField] public GameObject _cameraPivot = null;
    [SerializeField] public Camera _camera = null;
    [SerializeField] public float _smoothFollowSpeed = 0.125f;
    [SerializeField] public Transform _overallViewPos = null;
    [SerializeField] GameObject PureCrystal = null;
    [SerializeField] GameObject DestroyedParticle = null;
    [SerializeField] GameObject audioListener = null;

    [Range(1, 4)]
    [SerializeField] public int _rotationStage = 1;
    private float _tragetTransform = 0;
    public bool _TopView = false;
    private float LaunchCD = 1.5f;
    [SerializeField] Text StatusText = null;

    void Start()
    {
        if (StatusText == null)
        {
            StatusText = GameObject.Find("StatusText").GetComponent<Text>();
        }
        if (PureCrystal == null)
        {
            PureCrystal = GameObject.FindObjectOfType<WinCondition>().gameObject;
        }
        Cursor.visible = false;
        _tragetTransform = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (LaunchCD > 0)
        {
            LaunchCD -= Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (!PureCrystal.GetComponent<WinCondition>().Winning && _player.activeSelf)
                {
                    GameObject.FindObjectOfType<GravityController>()._TopView = false;
                    Instantiate(DestroyedParticle, _player.transform.position, Quaternion.identity);
                    PureCrystal.GetComponent<WinCondition>().textCountdown = 5;
                    PureCrystal.GetComponent<WinCondition>().switchingText = 8;
                    if (!PureCrystal.GetComponent<WinCondition>().hideInterface)
                    {
                        StatusText.enabled = true;
                    }
                    StatusText.color = Color.white;
                    StatusText.text = "Respawning! We are sending you back to the last check point...";
                    _player.SetActive(false);
                    PureCrystal.GetComponent<WinCondition>().ReloadSceneWithDelay(5f);
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _TopView = !_TopView;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                _rotationStage -= 1;
                if (_rotationStage <= 0)
                {
                    _rotationStage = 4;
                }
                _tragetTransform += 90;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _rotationStage += 1;
                if (_rotationStage >= 5)
                {
                    _rotationStage = 1;
                }
                _tragetTransform -= 90;
            }
            switch (_rotationStage)
            {
                case 1:
                    Physics.gravity = new Vector3(0f, -9.81f, -9.81f);
                    break;
                case 2:
                    Physics.gravity = new Vector3(9.81f, -9.81f, 0f);
                    break;
                case 3:
                    Physics.gravity = new Vector3(0f, -9.81f, 9.81f);
                    break;
                case 4:
                    Physics.gravity = new Vector3(-9.81f, -9.81f, 0f);
                    break;
            }
            if (_cameraPivot.transform.rotation.y != _tragetTransform)
            {
                _cameraPivot.transform.rotation = Quaternion.Slerp(_cameraPivot.transform.rotation, Quaternion.Euler(0f, _tragetTransform, 0f), 2f * Time.deltaTime);
            }
            if (_overallViewPos.transform.rotation.y != _tragetTransform)
            {
                _overallViewPos.transform.rotation = Quaternion.Slerp(_overallViewPos.transform.rotation, Quaternion.Euler(90f, _tragetTransform, 0f), 2f * Time.deltaTime);
            }
        }
    }
    private void FixedUpdate()
    {
        if (_player != null)
        {
            audioListener.transform.position = _player.transform.position + Vector3.up * 2;
            audioListener.transform.rotation = _player.transform.rotation;
            Vector3 smoothedPos = Vector3.Lerp(_cameraPivot.transform.position, _player.transform.position, _smoothFollowSpeed);
            _cameraPivot.transform.position = smoothedPos;
            if (!_TopView)
            {
                if (_camera.transform.position != _focusViewPos.transform.position || _camera.transform.rotation != _focusViewPos.transform.rotation)
                {
                    if (LaunchCD > 0)
                    {
                        _camera.transform.position = Vector3.Lerp(_camera.transform.position, _focusViewPos.transform.position, 3 * Time.deltaTime);
                        _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, _focusViewPos.transform.rotation, 1.5f * Time.deltaTime);
                    }
                    else
                    {
                        _camera.transform.position = Vector3.Lerp(_camera.transform.position, _focusViewPos.transform.position, 10 * Time.deltaTime);
                        _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, _focusViewPos.transform.rotation, 10 * Time.deltaTime);
                    }
                }
            }
            else
            {
                if (_camera.transform.position != _overallViewPos.position || _camera.transform.rotation != _overallViewPos.rotation)
                {
                    _camera.transform.position = Vector3.Lerp(_camera.transform.position, _overallViewPos.position, 10 * Time.deltaTime);
                    _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, _overallViewPos.rotation, 10 * Time.deltaTime);
                }
            }
        }
        else
        {
            LaunchCD += Time.deltaTime;
            _camera.transform.position += transform.up * Time.deltaTime * LaunchCD * 1f;
            if (!_TopView)
            {
                if (_camera.transform.position != _focusViewPos.transform.position || _camera.transform.rotation != _focusViewPos.transform.rotation)
                {
                    if (LaunchCD > 0)
                    {
                        _camera.transform.position = Vector3.Lerp(_camera.transform.position, _focusViewPos.transform.position, 3 * Time.deltaTime);
                        _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, _focusViewPos.transform.rotation, 1.5f * Time.deltaTime);
                    }
                    else
                    {
                        _camera.transform.position = Vector3.Lerp(_camera.transform.position, _focusViewPos.transform.position, 10 * Time.deltaTime);
                        _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, _focusViewPos.transform.rotation, 10 * Time.deltaTime);
                    }
                }
            }
        }
    }
}
