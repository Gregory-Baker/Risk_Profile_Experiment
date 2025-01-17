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
    [RequireComponent(typeof(DataListener))]
    public class MouseDrawer : MonoBehaviour
    {
        private MouseData mouseData = new MouseData();

        //binding via UnityEvent (DataFrameEvent of DataListener)
        public void ProcessData(IDataFrame frame)
        {
            DataFrame jsonFrame = frame as DataFrame;
            mouseData = jsonFrame.ParseFromJson<MouseData>();
        }

        void OnDrawGizmos()
        {
            float radius;
            if (mouseData.pressed)
            {
                radius = 1.2f;
                Gizmos.color = Color.green;
            }
            else
            {
                radius = 1;
                Gizmos.color = Color.grey;
            }

            Gizmos.DrawWireSphere(mouseData.worldPos, radius);
        }
    }
}

