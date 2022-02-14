using UnityEngine;
using TestAnimares;
using System.Collections;

public class PlanetManager : MonoBehaviour
{
    public static PlanetManager instance;

    [SerializeField] private ObjectSelector[] objectSelectors;

    private int sunParam = Animator.StringToHash("sun");
    private int earthParam = Animator.StringToHash("earth");
    private int moonParam = Animator.StringToHash("moon");


    private Animator animator;

    //Circumstantial variable
    private bool planetClicked;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    private void Start()
    {
        animator = animator == null ? GetComponent<Animator>():animator;
        SceneLoader.instance.EventLoadNewScene += OnLoadNewScene;

        foreach (var objectSelector in objectSelectors)
        {
            objectSelector.EventPlanetSelected += OnPlanetSelected;
        }

        if (instance != null)
        {

            // Initilize State
            animator.SetBool(sunParam, true);
            animator.SetBool(earthParam, true);
            animator.SetBool(moonParam, true);
        }
    }

    private void OnDestroy()
    {
        SceneLoader.instance.EventLoadNewScene -= OnLoadNewScene;

        foreach (var objectSelector in objectSelectors)
        {
            objectSelector.EventPlanetSelected -= OnPlanetSelected;
        }
    }

    private void OnLoadNewScene(int scene)
    {
        
        if (scene == 1)
        {
            animator.SetBool(sunParam, true);
            animator.SetBool(earthParam, true);
            animator.SetBool(moonParam, true);
        }

        if (scene == 2 && planetClicked)
        {
            planetClicked = false;
            return;
        }
        else if (scene == 2 && !planetClicked)
        {
            animator.SetBool(sunParam, false);
            animator.SetBool(earthParam, false);
            animator.SetBool(moonParam, false);
        }

        if (scene == 0)
        {
            animator.SetBool(sunParam, false);
            animator.SetBool(earthParam, false);
            animator.SetBool(moonParam, false);
        }

        ResettingPosition(scene);
    }


    private void OnPlanetSelected(Transform planetTransform)
    {
        planetClicked = true;
        ObjectSelector objectSelector;
        objectSelector = planetTransform.GetComponent<ObjectSelector>();
        SystemID planet = objectSelector.SystemPlanet;
        SceneLoader.instance.LoadNewScene(2);
        switch (planet)
        {
            case SystemID.SUN:
                animator.SetBool(sunParam, true);
                animator.SetBool(earthParam, false);
                animator.SetBool(moonParam, false);
                break;
            case SystemID.EARTH:
                animator.SetBool(sunParam, false);
                animator.SetBool(earthParam, true);
                animator.SetBool(moonParam, false);
                break;
            case SystemID.MOON:
                animator.SetBool(sunParam, false);
                animator.SetBool(earthParam, false);
                animator.SetBool(moonParam, true);
                break;
            default:
                break;
        }

        SettingCentralPosition();
    }

    private void SettingCentralPosition()
    {
        foreach (var objectSelector in objectSelectors)
        {
            objectSelector.enabled = false;
            StartCoroutine(InterpolateMovement(objectSelector, Vector3.zero, 1.5f));
            objectSelector.transform.rotation = Quaternion.identity;
        }
    }

    private void ResettingPosition(int scene)
    {
        foreach (var objectSelector in objectSelectors)
        {
            if (scene < 2)
            { 
                StartCoroutine(InterpolateMovement(objectSelector, objectSelector.InitialPosition, 2f));
                objectSelector.transform.rotation = Quaternion.identity;
                objectSelector.enabled = true;
            }
        }
        
    }

    private IEnumerator InterpolateMovement(ObjectSelector currentObject, Vector3 finalVector, float time)
    {
        float t = 0;

        while (t < time )
        {
            currentObject.transform.position = Vector3.Lerp(currentObject.transform.position, finalVector, t/time);
            t += Time.deltaTime;
            yield return null;
        }

        currentObject.transform.position = finalVector;
    }

}
