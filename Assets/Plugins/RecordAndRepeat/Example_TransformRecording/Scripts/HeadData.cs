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

using UnityEngine;

namespace RecordAndRepeat.Examples
{
    [System.Serializable]
    public class HeadData
    {
        public Vector3 worldPos;
        public Vector3 forward;
        public bool dc = false;
        public bool inCol = false;

        public HeadData() { }
        public HeadData(Transform t, bool inCollision, bool dcOn = false)
        {
            worldPos = t.position;
            forward = t.forward;
            dc = dcOn;
            inCol = inCollision;
        }

        public void DebugDraw(float radius, float rayLength)
        {
            Gizmos.DrawWireSphere(worldPos, radius);

            Vector3 from = worldPos;
            Vector3 to = from + forward * rayLength;
            Gizmos.DrawLine(from, to);
        }
    }
}