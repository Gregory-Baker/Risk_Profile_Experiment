﻿/*
© Siemens AG, 2017-2019
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

// Adding Timestamp switching
// Shimadzu corp , 2019, Akira NODA (a-noda@shimadzu.co.jp / you.akira.noda@gmail.com)

using UnityEngine;
namespace RosSharp.RosBridgeClient
{
    public static class HeaderExtensions
    {
        private static Timer timer = null;
        private static Timer defaultTimer = null;
        public static Timer Timer { set { timer = value; } }
        static HeaderExtensions()
        {
            timer = defaultTimer;
        }

        public static void Update(this MessageTypes.Std.Header header)
        {
            if (timer == null)
            {
                GameObject obj = new GameObject("DefaultTimer(UnityEpoch)");
                timer=defaultTimer=obj.AddComponent<Timer>();
            }
            header.seq++;
            header.stamp = timer.Now();
        }
    }
}
