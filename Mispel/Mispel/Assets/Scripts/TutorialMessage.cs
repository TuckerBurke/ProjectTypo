using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMessage : MonoBehaviour
{
    [SerializeField] private GameObject controlMessage;

    private bool displayMessage;

    // Start is called before the first frame update
    void Start()
    {
        controlMessage.SetActive(false);
        controlMessage.GetComponent<CanvasGroup>().alpha = 0;

    }

    // Update is called once per frame
    void Update()
    {
        controlMessage.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position + new Vector3(0, 0, 0));

        if(displayMessage)
        {
            controlMessage.SetActive(true);
            controlMessage.GetComponent<CanvasGroup>().alpha += Time.deltaTime*1.2f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.root.name == "Player")
        {
            displayMessage = true;
        }
    }
}
