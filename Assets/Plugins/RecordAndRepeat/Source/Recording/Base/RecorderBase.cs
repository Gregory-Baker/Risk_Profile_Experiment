﻿// MIT License

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

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RecordAndRepeat
{
    [ExecuteInEditMode]
    public abstract class RecorderBase : MonoBehaviour
    {
        //folder to store recordings
        protected static string recordingsPath = "DataRecordings";
        [HideInInspector]
        public string recordingName = "";

        //interface via inspector
        [HideInInspector]
        public bool disableIfNotPlaying = true;
        [HideInInspector]
        public bool doRecord = false;
        [HideInInspector]
        public bool doSave = false;
        [HideInInspector]
        public bool doCancel = false;

        //private members
        private float startTimeSec;
        private float pauseStartTimeSec;
        private bool isRecordingStarted = false;
        private bool isPaused = false;

        [SerializeField]
        [HideInInspector]
        private RecordingBase recording = null;

        [SerializeField]
        [HideInInspector]
        private string responseText;

        //properties
        public bool IsRecording { get { return isRecordingStarted && !isPaused; } }
        public bool IsPaused { get { return isPaused; } }
        public bool IsRecordingStarted { get { return isRecordingStarted; } }
        public string DestinationFolder = String.Format("Assets/{0}", recordingsPath);
        //{
        //    get { return String.Format("Assets/{0}", recordingsPath); }
        //}
        public string DefaultRecordingName { get; set; } = "New Recording";

        protected abstract RecordingBase CreateInstance();

        public void InitRecording()
        {
            if (recordingName == "")
            {
                recordingName = DefaultRecordingName;
            }
        }

        protected void Start()
        {
            doSave = false;
            doRecord = false;
        }

        protected void Update()
        {
            if (!isRecordingStarted && doRecord)
            {
                StartRecording();
            }
            else if (IsRecording && !doRecord)
            {
                PauseRecording();
            }
            else if (isRecordingStarted && isPaused && doRecord)
            {
                ContinueRecording();
            }

            if (doSave)
            {
                SaveRecording();
            }
            else if (doCancel)
            {
                CancelRecording();
            }
        }

        public void StartRecording()
        {
            recording = CreateInstance();

            startTimeSec = Time.realtimeSinceStartup;
            isRecordingStarted = true;
            doRecord = true;
            isPaused = false;
        }

        public void PauseRecording()
        {
            // Debug.Log("PauseRecording");
            isPaused = true;
            pauseStartTimeSec = Time.realtimeSinceStartup;
        }

        public void ContinueRecording()
        {
            float pauseDuration = Time.realtimeSinceStartup - pauseStartTimeSec;
            startTimeSec += pauseDuration;
            isPaused = false;
            // Debug.Log(String.Format("ContinueRecording after {0}",pauseDuration));
        }

        public void CancelRecording()
        {
            ResetRecorder();

            responseText = "Recording canceled!";
        }

        public void SaveRecording()
        {
#if UNITY_EDITOR
            if (recording == null || recording.duration <= 0)
            {
                responseText = "Nothing recorded yet, can't save Recording.";
                ResetRecorder();
                return;
            }

            string path = DestinationFolder;
            //if (!AssetDatabase.IsValidFolder(path))
            //{
            //    AssetDatabase.CreateFolder("Assets", recordingsPath);
            //}

            recordingName = recordingName.Trim() == "" ? DefaultRecordingName : recordingName;
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/" + recordingName + ".asset");

            AssetDatabase.CreateAsset(recording, assetPathAndName);
            responseText = String.Format("Recording stored under {0}.", assetPathAndName);
            Debug.Log(responseText);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            ResetRecorder();
#endif
        }

        private void ResetRecorder()
        {
            isPaused = isRecordingStarted = false;
            doCancel = doSave = doRecord = false;
            recording = null;
        }

        protected void RecordData(IDataFrame frame)
        {
            if (!IsRecording)
            {
                return;
            }

            frame.Time = Time.realtimeSinceStartup - startTimeSec;

            recording.duration = frame.Time; //always as long as the last data frame
            recording.Add(frame);
        }
    }
}

