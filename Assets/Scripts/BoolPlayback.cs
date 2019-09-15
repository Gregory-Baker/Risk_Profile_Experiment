using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RecordAndRepeat;
using RecordAndRepeat.Examples;

public class BoolPlayback : MonoBehaviour
{
    public BoolRecorder.BoolData boolData = null;
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
        boolData = jsonFrame.ParseFromJson<BoolRecorder.BoolData>();
    }
}
