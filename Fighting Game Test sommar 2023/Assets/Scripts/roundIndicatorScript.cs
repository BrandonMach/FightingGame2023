using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roundIndicatorScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Color[] _indicateColor;
    public Dictionary<string, Color> koType;

    void Start()
    {
        koType = new Dictionary<string, Color>();
        koType.Add("NormalKO", _indicateColor[0]);
        koType.Add("PerfectKO", _indicateColor[1]);
        koType.Add("TimeKO", _indicateColor[2]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
