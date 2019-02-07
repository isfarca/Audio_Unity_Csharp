using System.Collections.Generic;
using UnityEngine;

public class FmodEventParameterController : MonoBehaviour
{
    // Value types
    [SerializeField, FMODUnity.EventRef] private string eventReference;
    private string parameterName;
    private float parameterValue;

    // Reference types
    private FMOD.Studio.EventInstance eventInstance;
    private Dictionary<string, FMOD.Studio.ParameterInstance> parameterDictionary;

    /// <summary>
    /// Initialize variables.
    /// </summary>
    private void Awake()
    {
        // Produce a new dictionary.
        parameterDictionary = new Dictionary<string, FMOD.Studio.ParameterInstance>();
    }

    // Use this for initialization
    void Start()
    {
        // Create a instance for a FMOD event.
        eventInstance = FMODUnity.RuntimeManager.CreateInstance(eventReference);

        LoadParameters(eventInstance);

        // Start the FMOD event instance.
        eventInstance.start();
	}
	
	// Update is called once per frame
	void Update()
    {
        // Modify audio.
        if (Input.GetKeyDown(KeyCode.Return))
            SetParameter(parameterName, parameterValue);
    }

    /// <summary>
    /// Load modified parameters.
    /// </summary>
    /// <param name="eventInstance">Instance from the current FMOD event.</param>
    private void LoadParameters(FMOD.Studio.EventInstance eventInstance)
    {
        // Declare variables
        int paramCount;
        FMOD.Studio.ParameterInstance parameterInstance;
        FMOD.Studio.PARAMETER_DESCRIPTION parameterDescription;

        eventInstance.getParameterCount(out paramCount);

        // Add parameters in the dictionary.
        for (int i = 0; i < paramCount; i++)
        {
            eventInstance.getParameterByIndex(i, out parameterInstance);
            parameterInstance.getDescription(out parameterDescription);

            parameterDictionary.Add(parameterDescription.name, parameterInstance);

            Debug.LogFormat("Param: {0} | Type: {1} | Default value {2}", parameterDescription.name, parameterDescription.type.ToString(), parameterDescription.defaultvalue);
        }
    }

    /// <summary>
    /// Set modified parameters in the FMOD event instance.  
    /// </summary>
    /// <param name="parameterName">Name from FMOD parameter.</param>
    /// <param name="parameterValue">Value from FMOD parameter.</param>
    public void SetParameter(string parameterName, float parameterValue)
    {
        // Declare variables
        FMOD.Studio.ParameterInstance parameterInstance;

        // Setting value to FMOD event instance.
        if (parameterDictionary.TryGetValue(parameterName, out parameterInstance))
            parameterInstance.setValue(parameterValue);
        else
            Debug.LogFormat("Parameter {0} not found!", parameterName);
    }
}
