using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feathers : MonoBehaviour
{
    public int jumps;
    public int numOfFeathers;

    public Image[] feathers;
    public Sprite newFeather;
    public Sprite usedFeather;

    void Start() {
        numOfFeathers = GetComponent<stolen>().num_of_jumps;
        jumps = numOfFeathers;
    }

    void Update() {

        jumps = GetComponent<stolen>().jumpNum;

        for (int i = 0; i < feathers.Length; i++) {
            if (i < jumps) {
                feathers[i].sprite = newFeather;
            } else {
                feathers[i].sprite = usedFeather;
            }

            if (i < numOfFeathers) {
                feathers[i].enabled = true;
            } else {
                feathers[i].enabled = false;
            }
        }
    }
}
