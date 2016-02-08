using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour {
    
	void Start () {
        var button = GetComponent<Button>();
        button.onClick.AddListener(delegate
       {
           GameManager.EndGame();
       });
	}
}
