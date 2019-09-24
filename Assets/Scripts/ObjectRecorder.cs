// MIT License

// Copyright (c) 2018 Felix Lange 

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RecordAndRepeat;
using RecordAndRepeat.Examples;


[RequireComponent(typeof(Recorder))]
//[ExecuteInEditMode]
public class ObjectRecorder : MonoBehaviour
{
    public Transform objectTransform;
    private Recorder recorder;
    string participantID;
    public string objectName;
    bool recordingStarted = false;
    bool recordingSaved = false;
    public Timer timer;
    public Status status;
    public CollisionAvoidance collisionAvoidance;
    
    void Awake()
    {
        recorder = GetComponent<Recorder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!recorder.IsRecording && timer.timerOn)
        {
            if (!recordingStarted)
            {
                recorder.DestinationFolder = status.folderLocalPath;
                recorder.recordingName = status.trialName + "_" + objectName;
                recorder.StartRecording();
                recordingStarted = true;
            }
        }

        if (objectTransform == null)
        {
            Debug.LogWarning("No transform target set!");
            return;
        }

        HeadData headData = new HeadData(objectTransform, collisionAvoidance.inCollision, status.directControl);
        recorder.RecordAsJson(headData);

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
