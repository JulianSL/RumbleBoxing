using GAF.Assets;
using GAF.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenuManager : MonoBehaviour {

    // Use this for initialization
    [SerializeField] private GameObject modelUnit;
    [SerializeField] private GameObject price;
    [SerializeField] private GameObject canvasLoadingScreen;
    [SerializeField] private GameObject UIOptions;
    [SerializeField] private Toggle music;
    [SerializeField] private List<GAFAnimationAsset> listOfCharacter = new List<GAFAnimationAsset>();
    [SerializeField] private Button btn_buy;
    [SerializeField] private Button btn_equip;
    [SerializeField] private Text txt_price;
    [SerializeField] private Text txt_characterName;
    [SerializeField] private Text txt_currentMedal;
    [SerializeField] private Slider sliderBarLoading;
    [SerializeField] private List<RuntimeAnimatorController> listOfAnimationControllerCharater = new List<RuntimeAnimatorController>();
    bool isPaused;

    enum showedMenu
    {
        skinMenu = 1,
        shopMenu = 2,
        rankMenu = 3
    }

    private int idShowedMenu;

    private int idModel;
	void Start () {
        isPaused = false;
        //GameplayDataManager.getInstance().reset();
        idModel = 1;
        showMenu((int)showedMenu.skinMenu);
        updateModel();
	}

    private void showMenu(int _idMenu)
    {
        idShowedMenu = _idMenu;
    }

    private void updateModel()
    {
        modelUnit.GetComponent<GAFMovieClip>().clear(false);
        modelUnit.GetComponent<GAFMovieClip>().asset = listOfCharacter[idModel - 1];
        modelUnit.GetComponent<GAFMovieClip>().initialize(listOfCharacter[idModel - 1]);
        modelUnit.GetComponent<GAFMovieClip>().settings.init(listOfCharacter[idModel - 1]);
        modelUnit.GetComponent<GAFMovieClip>().reload();
        modelUnit.GetComponent<GAFMovieClip>().setSequence("idle", true);
        txt_characterName.text = DatabaseCharacter.getInstance().getName(idModel);
        txt_price.text = DatabaseCharacter.getInstance().getPrice(idModel).ToString();
        txt_currentMedal.text = GameplayDataManager.getInstance().TotalMedals.ToString();
        if (GameplayDataManager.getInstance().isUnitUnlocked(idModel))
        {
            //modelUnit.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            btn_buy.gameObject.SetActive(false);
            price.SetActive(false);
            if (GameplayDataManager.getInstance().IdEquipedUnit == idModel)
            {
                btn_equip.gameObject.SetActive(false);
            }
            else
            {
                btn_equip.gameObject.SetActive(true);
            }
        }
        else
        {
            //modelUnit.GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f);
            price.SetActive(true);
            btn_equip.gameObject.SetActive(false);
            btn_buy.gameObject.SetActive(true);
        }
        //modelUnit.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("animation/Character" + idModel.ToString(), typeof(RuntimeAnimatorController));
        //modelUnit.GetComponent<Animator>().runtimeAnimatorController = listOfAnimationControllerCharater[idModel - 1];
    }

    public void prevModel()
    {
        idModel--;
        if(idModel < 1)
        {
            idModel = GameplayDataManager.getInstance().TotalUnits;
        }
        updateModel();
        Debug.Log(idModel);
    }

    public void nextModel()
    {
        idModel++;
        if (idModel > GameplayDataManager.getInstance().TotalUnits)
        {
            idModel = 1;
        }
        updateModel();
        Debug.Log(idModel);
    }

    public void unlockModel()
    {
        int price = DatabaseCharacter.getInstance().getPrice(idModel);
        int currentMedal = GameplayDataManager.getInstance().TotalMedals;
        if(currentMedal >= price)
        {
            GameplayDataManager.getInstance().TotalMedals -= price;
            GameplayDataManager.getInstance().unlockUnit(idModel);
        }
        updateModel();
        GameplayDataManager.getInstance().saveGame();
    }

    public void equipModel()
    {
        if (GameplayDataManager.getInstance().isUnitUnlocked(idModel))
        {
            GameplayDataManager.getInstance().IdEquipedUnit = idModel;
        }
        updateModel();
        GameplayDataManager.getInstance().saveGame();
    }

    public void StartGame()
    {
        canvasLoadingScreen.SetActive(true);
        StartCoroutine(loadAsync());
    }

    IEnumerator loadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            Debug.Log("loading" + operation.progress);
            sliderBarLoading.value = (float)Mathf.Clamp01(operation.progress / .9f);
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    public void showOptions()
    {
        if (AudioManager.instance.isMute())
        {
            music.isOn = true;
            AudioManager.instance.mute();
        }
        else
        {
            music.isOn = false;
        }
        UIOptions.SetActive(true);
    }

    public void hideOptions()
    {
        UIOptions.SetActive(false);
    }

    public void muteMusic()
    {
        AudioManager.instance.mute();
    }

    public void clearsaveData()
    {
        GameplayDataManager.getInstance().clearSaveData();
        updateModel();
    }

    private void Update()
    {
       
    }

    public void quitGame()
    {
        Application.Quit();
    }

    
}
