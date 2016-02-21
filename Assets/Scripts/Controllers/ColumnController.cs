using UnityEngine;
using System.Collections;

public class ColumnController : MonoBehaviour
{
	public enum ColumnType{
		Top, Bottom
	}

    public ColumnType type = ColumnType.Top;

//     public float disableDelay = 4f;

//     private void OnEnable() {
//     	StartCoroutine( DisableAfterDelay() );
//     }

//     private IEnumerator DisableAfterDelay() {
//     	yield return new WaitForSeconds(disableDelay);
//     	gameObject.SetActive(false);
//     }
}