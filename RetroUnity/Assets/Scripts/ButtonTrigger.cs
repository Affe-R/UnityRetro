using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class ButtonTrigger : MonoBehaviour
{
    delegate void OnButtonEnter();
    OnButtonEnter onButtonEnter;
    delegate void OnButtonTrigger();
    OnButtonTrigger onButtonTrigger;
    delegate void OnButtonExit();
    OnButtonExit onButtonExit;
    BoxCollider2D buttonCollider;

    // Start is called before the first frame update
    void Awake()
    {
        buttonCollider = GetComponent<BoxCollider2D>();
        if(!buttonCollider.isTrigger)
        {
            buttonCollider.isTrigger = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            onButtonTrigger?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            onButtonTrigger?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onButtonExit?.Invoke();
        }
    }


}
