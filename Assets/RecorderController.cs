using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecorderController : MonoBehaviour
{
    public bool record;
    public bool playback;

    public Status status;

    public ObjectRecorder[] recorderScripts;
    public ObjectRecorder targetRecorder;

    public ObjectPlayback[] playbackScripts;
    public GameObject playbackCamera;


    // Start is called before the first frame update
    void Start()
    {
        if (!status.tutorial)
        {
            if (record)
            {
                foreach (ObjectRecorder recorder in recorderScripts)
                {
                    recorder.enabled = true;
                }

                foreach (ObjectPlayback playback in playbackScripts)
                {
                    playback.enabled = false;
                }
            }

            else if (playback)
            {
                status.directControl = true;

                foreach (ObjectRecorder recorder in recorderScripts)
                {
                    recorder.enabled = false;
                }

                foreach (ObjectPlayback playback in playbackScripts)
                {
                    playback.enabled = true;
                }
                playbackCamera.SetActive(true);
            }
        }

        else
        {
            foreach (ObjectPlayback playback in playbackScripts)
            {
                playback.enabled = false;
            }

            foreach (ObjectPlayback playback in playbackScripts)
            {
                playback.enabled = true;
            }
        }

        if (!status.changeControlPermitted && status.directControl)
        {
            targetRecorder.enabled = false;
        }

    }
    
}
