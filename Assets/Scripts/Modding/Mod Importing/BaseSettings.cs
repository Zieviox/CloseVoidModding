using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseSettings : MonoBehaviour
{
    public string charName;                 // The actual character name
    public GameObject modCharacter;         // The character prefab
    Animator animator;                      // The animator component of this character

    public GameObject charController;
    Animator contAnim;                      // the Animator component of the Character Controller


    public float animatorDeactivation = 0.1f;
    public float animatorActivation = 0.1f;
    

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("ModCharacter")){
            gameObject.tag = "CurrentCharacter";
            StartCoroutine(ChangeScene(0.1f));            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeScene(float seconds)
    {
        // Find and Initialize the variables of the character controller
        CharControllerSetup();

        //Get the main Scene
        Scene mainScene = SceneManager.GetSceneAt(0);

        //Move to the main scene
        SceneManager.MoveGameObjectToScene(gameObject, mainScene);

        //Set the Character Controller as the parent
        gameObject.transform.SetParent(charController.transform, false);


        yield return null;


        /*
        while (true)
        {
            //code
            modName = modCharacter.gameObject.GetComponent<BaseSettings>().charName;
            yield return new WaitForSeconds(seconds);
        }
        */
    }


    void CharControllerSetup()
    {
        charController = GameObject.FindWithTag("CharacterController");

        contAnim = charController.GetComponent<Animator>();
        animator = gameObject.GetComponent<Animator>();

        contAnim.avatar = animator.avatar;
        contAnim.Rebind();


        contAnim.enabled = false;

        Invoke(nameof(animatorDisable), animatorDeactivation);
        Invoke(nameof(animatorEnable), animatorActivation);
    }


    void animatorDisable()
    {
        contAnim.enabled = false;
        contAnim.Rebind();
    }

    void animatorEnable()
    {
        contAnim.enabled = true;
    }
}
