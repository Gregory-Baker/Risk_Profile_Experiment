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
using RecordAndRepeat;
using RecordAndRepeat.Examples;

[ExecuteInEditMode]
[RequireComponent(typeof(DataListener))]
public class ObjectPlayback : MonoBehaviour
{
    public Transform playbackTarget;

    [Header("DebugDraw")]
    public Color color;
    public float radius = 0.5f;
    public float rayLength = 0.1f;

    private HeadData headData = null;
    private DataListener dataListener;

    void Awake()
    {
        dataListener = GetComponent<DataListener>();
    }

    void OnEnable()
    {
        dataListener.OnDataFrameReceived += ProcessData;
    }

    void OnDisable()
    {
        dataListener.OnDataFrameReceived -= ProcessData;
    }

    public void ProcessData(IDataFrame frame)
    {
        DataFrame jsonFrame = frame as DataFrame;
        headData = jsonFrame.ParseFromJson<HeadData>();

        if (playbackTarget != null)
        {
            playbackTarget.position = headData.worldPos;
            playbackTarget.rotation = Quaternion.LookRotation(headData.forward);
        }
    }

    void OnDrawGizmos()
    {
        if (headData == null)
        {
            return;
        }

        Gizmos.color = color;
        headData.DebugDraw(radius, rayLength);
    }
}
