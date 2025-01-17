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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RecordAndRepeat.Examples
{
    public class HeadPlotter : MonoBehaviour
    {
        public Color colorDc = Color.green;
        public Color colorIc = Color.blue;
        public Color colorCol = Color.red;

        [System.Serializable]
        public class ColoredRecording
        {
            public Recording recording = null;
            public bool flip = false;
        }

        public List<ColoredRecording> recordings = new List<ColoredRecording>();

        [Header("Size")]
        public float radius = 0.5f;
        public float rayLength = 0.1f;

        [Range(0, 1)]
        public float connectionAlpha = 1f;

        void OnDrawGizmos()
        {
            //loop recordings
            foreach (ColoredRecording coloredRec in recordings)
            {
                Recording recording = coloredRec.recording;
                if (recording == null)
                {
                    continue;
                }


                //draw colored recording
                HeadData lastHeadData = null;
                foreach (DataFrame frame in recording.DataFrames)
                {

                    HeadData headData = frame.ParseFromJson<HeadData>();
                    if (coloredRec.flip)
                    {
                        headData.worldPos.z = -headData.worldPos.z;
                    }

                    if (headData.dc)
                    {
                        if (headData.inCol)
                        {
                            Gizmos.color = colorCol;
                        }
                        else
                        {
                            Gizmos.color = colorDc;
                        }
                    }
                    else
                    {
                        Gizmos.color = colorIc;
                    }
                    
                    headData.DebugDraw(radius, rayLength);

                    //draw connection between heads
                    if (lastHeadData != null)
                    {
                        SetGizmoAlpha(connectionAlpha);
                        Gizmos.DrawLine(lastHeadData.worldPos, headData.worldPos);
                    }
                    lastHeadData = headData;
                }
            }
        }

        void SetGizmoAlpha(float alpha)
        {
            Color drawColor = Gizmos.color;
            drawColor.a = alpha;
            Gizmos.color = drawColor;
        }
    }
}
