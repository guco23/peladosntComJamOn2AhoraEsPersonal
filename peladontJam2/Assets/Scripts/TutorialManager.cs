using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] private GameObject resoursesSystem_;

    [SerializeField] private GameObject spawnSystem_;

    [SerializeField] private GameObject iaSystem_;

    [SerializeField] private GameObject canvas_;

    [SerializeField] private GameObject tropa_;

    [SerializeField] private GameObject trincheras_;

    [SerializeField] private GameObject menuTrinchera_;

    [SerializeField] private GameObject textLevel_;

    [SerializeField] private GameObject mejoraBase_;

    [SerializeField] private GameObject [] texts_;

    private int currentText_;


    public void NextPartText()
    {
        if(currentText_ < texts_.Length) {

            texts_[currentText_].SetActive(true);

            currentText_++;

        }
        

    }

    public void CompleteCondition()
    {

        //mover camara
        if(currentText_ == 1)
        {

            

        }
        //info niebla
        else if(currentText_ == 2)
        {

            canvas_.SetActive(true);
            trincheras_.SetActive(false);
            menuTrinchera_.SetActive(false);

        }
        //colocar soldado
        else if(currentText_ == 3)
        {

            resoursesSystem_.SetActive(true);
            spawnSystem_.SetActive(true);
            
        }
        //colocar zafador
        else if(currentText_ == 4)
        {
            trincheras_.SetActive(true);

        }
        //meter soldados en trincheras
        else if(currentText_ == 5)
        {

        }
        //Soltar soldados en las trincheras
        else if(currentText_ == 6)
        {


        }
        //Mejorar base
        else if(currentText_ == 7)
        {

            mejoraBase_.SetActive(true);

        }

        NextPartText();

    }

    // Start is called before the first frame update
    void Start()
    {

        resoursesSystem_.SetActive(false);
        spawnSystem_.SetActive(false);
        iaSystem_.SetActive(false);
        canvas_.SetActive(false);
        textLevel_.SetActive(false);
        mejoraBase_.SetActive(false);

        for (int i = 0; i < texts_.Length; i++)
        {
            texts_[i].SetActive(false);
        }

        NextPartText();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(currentText_ == texts_.Length)
            {
                SceneManager.LoadScene("New Main Menu");
            }
            texts_[currentText_ - 1].SetActive(false);
            CompleteCondition();
        }


    }
}
