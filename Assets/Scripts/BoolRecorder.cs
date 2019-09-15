using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RecordAndRepeat;
using RecordAndRepeat.Examples;


public class BoolRecorder : MonoBehaviour
{
    private Recorder recorder;
    bool recordingStarted = false;
    bool recordingSaved = false;
    public Timer timer;
    public Status status;

    public class BoolData
    {
        public bool boolean;

        public BoolData() { }
        public BoolData(bool input)
        {
            boolean = input;
        }
    }

    void Awake()
    {
        recorder = GetComponent<Recorder>();
    }

    void Update()
    {
        if (!recorder.IsRecording && timer.timerOn)
        {
            if (!recordingStarted)
            {
                recorder.DestinationFolder = status.folderLocalPath;
                recorder.recordingName = status.trialName + "_DirectControlOn";
                recorder.StartRecording();
                recordingStarted = true;
            }
        }

        BoolData boolData = new BoolData(status.directControl);
        recorder.RecordAsJson(boolData);

        if (timer.trialFinished & !recordingSaved)
        {
            recorder.SaveRecording();
            recordingSaved = true;
        }

    }

    private void OnApplicationQuit()
    {
        if (!recordingSaved)
        {
            recorder.SaveRecording();
        }
    }
}
